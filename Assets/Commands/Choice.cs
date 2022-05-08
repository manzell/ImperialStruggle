using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 

public class Choice : Command
{
    public static UnityEvent<Choice> choiceEvent = new UnityEvent<Choice>();
    [SerializeField] List<Command> choices = new List<Command>();
    [SerializeField] int numChoices = 1;

    public override void Do(BaseAction action)
    {
        throw new System.NotImplementedException();
    }
}
