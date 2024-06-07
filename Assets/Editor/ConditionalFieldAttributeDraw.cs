using System;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(conditional, property);

        bool wasEnabled = GUI.enabled;
        GUI.enabled = enabled;

        if (enabled)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }

        GUI.enabled = wasEnabled;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute conditional = (ConditionalFieldAttribute)attribute;
        bool enabled = GetConditionalHideAttributeResult(conditional, property);

        return enabled ? EditorGUI.GetPropertyHeight(property, label) : -EditorGUIUtility.standardVerticalSpacing;
    }

    private bool GetConditionalHideAttributeResult(ConditionalFieldAttribute conditional, SerializedProperty property)
    {
        string propertyPath = property.propertyPath;
        string conditionPath = propertyPath.Replace(property.name, conditional.ConditionPath);
        SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

        if (sourcePropertyValue == null)
        {
            Debug.LogWarning("No matching SourcePropertyValue found in object: " + conditional.ConditionPath);
            return true;
        }

        return EvaluateCondition(sourcePropertyValue, conditional.CompareValue, conditional.Operator);
    }

    private bool EvaluateCondition(SerializedProperty sourceProperty, object compareValue, string comparisonOperator)
    {
        switch (sourceProperty.propertyType)
        {
            case SerializedPropertyType.Enum:
                int enumValue = sourceProperty.enumValueIndex;
                int compareEnumValue = (int)compareValue;
                return CompareValues(enumValue, compareEnumValue, comparisonOperator);
            case SerializedPropertyType.Integer:
                int intValue = sourceProperty.intValue;
                int compareIntValue = (int)compareValue;
                return CompareValues(intValue, compareIntValue, comparisonOperator);
            case SerializedPropertyType.Boolean:
                bool boolValue = sourceProperty.boolValue;
                bool compareBoolValue = (bool)compareValue;
                return CompareValues(boolValue, compareBoolValue, comparisonOperator);
            default:
                Debug.LogWarning("Unsupported property type for comparison: " + sourceProperty.propertyType);
                return true;
        }
    }

    private bool CompareValues<T>(T sourceValue, T compareValue, string comparisonOperator) where T : IComparable<T>
    {
        switch (comparisonOperator)
        {
            case "==":
                return sourceValue.CompareTo(compareValue) == 0;
            case "!=":
                return sourceValue.CompareTo(compareValue) != 0;
            case ">":
                return sourceValue.CompareTo(compareValue) > 0;
            case ">=":
                return sourceValue.CompareTo(compareValue) >= 0;
            case "<":
                return sourceValue.CompareTo(compareValue) < 0;
            case "<=":
                return sourceValue.CompareTo(compareValue) <= 0;
            default:
                Debug.LogWarning("Unsupported comparison operator: " + comparisonOperator);
                return true;
        }
    }
}