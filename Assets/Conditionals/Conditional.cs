using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public abstract class Conditional : SerializedMonoBehaviour, IConditional 
{
    protected enum ConditionType { Exactly, MoreThan, FewerThan, NotLessThan, NotMoreThan, Not }
    public string conditionalText;
    public abstract bool Test(Object context);
}

public interface IConditional { }
public interface ICriteria { }

// From DoctorPangloss on Unity Discord:

//interface ICondition
//{
//    bool Evaluate(IContext context);
//}

//class ComparisonCondition : ICondition
//{
//    ArithmeticOperator operator;

//    IValue leftOperand;
//    IValue rightOperand;
//    bool Evaluate(IContext context)
//    {
//        var left = leftOperand?.GetFloatValue(context) ?? context.leftOperand.GetFloatValue(context);
//        var right = rightOperand?.GetFloatValue(context) ?? context.rightOperand.GetFloatValue(context);
//        switch (operator) {
//      case ArithmeticOperator.GreaterThan:
//        return left > right;
//            ...
//    }
//    }
//}

//class ComparisonCondition : ICondition
//{
//    ArithmeticOperator operator;

//    IValue leftOperand;
//    IValue rightOperand;
//    bool Evaluate(IContext context)
//    {
//        var left = leftOperand?.GetFloatValue(context)
//         ?? context.currentValue.GetFloatValue(context);
//        var right = rightOperand?.GetFloatValue(context)
//         ?? context.currentValue.GetFloatValue(context);
//        switch (operator) {
//      case ArithmeticOperator.GreaterThan:
//        return left > right;
//            ...
//    }
//    }
//}

//var greaterThanFive = new ComparisonCondition()
//{
//    rightOperand = new ConstValue(5),
//   operator = ArithemticOperator.GreaterThan
//};

//...

//    class CustomIsPlayable : ICondition
//{
//    bool Evaluate(IContext context)
//    {
//        var currentAction = context.currentAction;
//        if (currentAction == null)
//        {
//            return false;
//        }
//        return currentAction.target.region == Regions.NORTH_AMERICA
//           || currentAction.type == ActionTypes.DIPLOMATIC
//           || context.GetScoreFor(Players.OPPONENT, currentAction.target.territory) >= 1;
//    }
//}

//bool CanPlay(IContext context, Action currentAction)
//{
//    EnchantmentEnum[] enchantments = context.GetAllActionEnchantments();
//    // check cost, do a bunch of stuff
//    return enchantments.All(e => {
//        switch (e)
//        {
//            case EnchantmentEnum.THE_IMPERIALIST:
//                return !context.GetPlayerHasEnchantment(Players.FRIENDLY, EnchantmentEnum.SPENT_THE_IMPERALIST) &&
//                 (currentAction.target.region == Regions.NORTH_AMERICA
//                 || currentAction.type == ActionTypes.DIPLOMATIC
//                 || context.GetScoreFor(Players.OPPONENT, currentAction.target.territory)) >= 1...
//  }
//    });
//}

//var theImperalist = new Card()
//{
//    title = "The Imperalist",
//    description = "At the start of every turn, gain a free action in North America; or a free diplomatic action; or in any territory where your opponent has at least 1 point",
//    cost = 1,
//    friendlyActionEnchantments = new[] { EnchantmentEnum.THE_IMPERALIST }
//};

//which can be encoded directly in a scriptable object
//so when something is complicated, don't make a metaprogramming thing. just declare an enum, and check for it in the right places @ManzellBeezy
//in this implementation The Imperialist would need to put another enchantment into play indicating it was spent this turn 
//alternatively you can create an enchantment token tracker if this is a common thing you do in your game
//ManzellBeezy — Today at 11:25 AM
//I think I can un-genericize the code and move on from that approach, but shifting everything into scriptable objects with enums is probably too big of an overhaul for the scope of the project right now. 
//doctorpangloss — Today at 11:25 AM
//it can be whatever you need it to be
//not a scriptable object
//but more like, don't inject code
//coordinate around an enum
//that corresponds to the effect
//this thing that you are doing, this particular card, it's really hard to do
//there's a lot that would go into its implementation and you are not going to make something bug free via meta programming
//it's a really complicated card lol