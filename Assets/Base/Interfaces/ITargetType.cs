using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetType<T>
{
    public T target { get; }
}