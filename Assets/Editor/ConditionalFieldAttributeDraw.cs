using UnityEditor;
using UnityEngine;

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

        if (enabled)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return -EditorGUIUtility.standardVerticalSpacing;
        }
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
            case SerializedPropertyType.Boolean:
                bool sourceBool = sourceProperty.boolValue;
                bool compareBool = compareValue == null ? true : (bool)compareValue;
                return CompareValues(sourceBool, compareBool, comparisonOperator);
            case SerializedPropertyType.Integer:
                int sourceInt = sourceProperty.intValue;
                int compareInt = compareValue == null ? 0 : (int)compareValue;
                return CompareValues(sourceInt, compareInt, comparisonOperator);
            case SerializedPropertyType.Float:
                float sourceFloat = sourceProperty.floatValue;
                float compareFloat = compareValue == null ? 0.0f : (float)compareValue;
                return CompareValues(sourceFloat, compareFloat, comparisonOperator);
            case SerializedPropertyType.String:
                string sourceString = sourceProperty.stringValue;
                string compareString = compareValue == null ? string.Empty : (string)compareValue;
                return CompareValues(sourceString, compareString, comparisonOperator);
            default:
                Debug.LogWarning("Unsupported property type for comparison: " + sourceProperty.propertyType);
                return true;
        }
    }

    private bool CompareValues<T>(T sourceValue, T compareValue, string comparisonOperator) where T : System.IComparable<T>
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