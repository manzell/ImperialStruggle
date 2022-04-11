using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Calculation<T> : SerializedMonoBehaviour
{
    public enum CalculationType { Once, Dynamic }
    public CalculationType calculationType;
    public string calculationText;

    [ShowInInspector] public T value
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
    private T _value = default(T);
    [SerializeField] protected bool calculated = false; // Calculate() Functions MUST set the calculated flag

    public virtual T Calculate() => default(T);
}
