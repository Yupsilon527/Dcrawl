using UnityEngine;
using UnityEditor;

public class Resource
{
    bool canNegative;
    string debugName;
    float[] values;
    bool hasHardLimit = false;

    public Resource( float limit,string name,bool negative, bool limited)
    {
        debugName = name;
        hasHardLimit = limited;
        values = new float[] { limit, limit };
        canNegative = negative;
        Debug.Log("["+ debugName+"] Initialized");
    }
    public float GetValue()
    {
        return values[0];
    }
    public float GetPercentage()
    {
        float value = values[0] / values[1];
        if (float.IsNaN(value))
            return 0;
        return value;
    }

    public float GetLimit()
    {
        return values[1];
    }
    public float GetDifference()
    {
        return values[1] - values[0];
    }

    public void GiveValue(float value)
    {
        Debug.Log("[" + debugName + "] Give " + value);
        if (value != 0)
        {
            SetValue(values[0] + value);
        }
    }

    public void SetValue(float value)
    {
        float oldlife = values[0];
        if (hasHardLimit)
            values[0] = Mathf.Min(value, values[1]);
        else 
            values[0] = value;

        if (!canNegative)
        {
            values[0] = Mathf.Max(0, values[0]);
        }
        Debug.Log("[" + debugName + "] Change " + oldlife + " to " + values[0]);
    }

    public void SetPercentage(float value)
    {
        SetValue(value * GetLimit());
    }

    public enum LimitRule
    {
        leave_value,
        heal_difference,
        percent_value,
        fullheal_value,
        empty_value
    }

    public void SetLimit(float value, LimitRule rule)
    {
       // display?.SetMaximum(values[1]);
       switch (rule)
        {
            case LimitRule.leave_value:
                values[1] = value;
                SetValue(values[0]);
                break;
            case LimitRule.heal_difference:
                float difference = value - values[1];
                values[1] = value;
                SetValue(values[0] + difference);
                break;
            case LimitRule.percent_value:
                float percent = GetPercentage();
                values[1] = value;
                SetPercentage(percent);
                break;
            case LimitRule.fullheal_value:
                values[1] = value;
                SetPercentage(1);
                break;
            case LimitRule.empty_value:
                values[1] = value;
                SetPercentage(0);
                break;
        }

        Debug.Log("[" + debugName + "] Set Max to " + value );
    }

    public bool ChargeValue(float value)
    {
        Debug.Log("[" + debugName + "] Charge " + value );
        if (value == 0)
        {
            return true;
        }
        if (GetValue() < value)
        {
            return false;
        }
        GiveValue(-value);
        return true;
    }

    public void SubstractValue(float value)
    {
        Debug.Log("[" + debugName + "] Substract " + value );
        GiveValue(-Mathf.Min(GetValue(),value));
    }
}