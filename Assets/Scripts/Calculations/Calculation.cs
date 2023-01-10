using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public abstract class Calculation<T>
{
    public T value
    {
        get
        {
            if (!calculated || calculationType == CalculationType.Dynamic)
                _value = Calculate();

            return _value;
        }
        set
        {
            _value = value;
        }
    }
    public enum CalculationType { Once, Dynamic }
    public CalculationType calculationType;
    public string calculationText;

    private T _value = default(T);
    [SerializeField] protected bool calculated = false; // Calculate() Functions MUST set the calculated flag

    public abstract T Calculate();
}
