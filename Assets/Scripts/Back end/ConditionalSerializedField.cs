using UnityEngine;

public class ConditionalFieldAttribute : PropertyAttribute
{
    public string ConditionPath { get; }
    public object CompareValue { get; }
    public string Operator { get; }

    public ConditionalFieldAttribute(string conditionPath, object compareValue = null, string comparisonOperator = "==")
    {
        this.ConditionPath = conditionPath;
        this.CompareValue = compareValue;
        this.Operator = comparisonOperator;
    }
}