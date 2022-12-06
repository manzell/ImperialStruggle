using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActionTarget<T>
{
    public T target { get; }
}