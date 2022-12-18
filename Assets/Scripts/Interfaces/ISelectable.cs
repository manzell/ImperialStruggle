using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public System.Action UISelectionEvent { get; set; }
    public System.Action UIDeselectEvent { get; set; }
    public string Name { get; }
}
