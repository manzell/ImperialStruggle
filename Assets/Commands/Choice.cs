using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Choice : Command
{
    public static UnityEvent<Choice> choiceEvent = new UnityEvent<Choice>();
    [SerializeField] int numChoices = 1; 

    public override void Do(Game.Faction faction)
    {
        HashSet<GameObject> options = new HashSet<GameObject>();

        foreach (Command command in GetComponentsInChildren<Command>())
            options.Add(command.gameObject); 

        // Open up a Selector Window

        base.Do(faction);
        choiceEvent.Invoke(this); 
    }
}
