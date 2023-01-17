
namespace ImperialStruggle
{
    // A Calculating returns some values based on the given Game Context (a PlayerAction)
    // Calculations generally speaking must not be collapsed until the Do() function of the PlayerAction
    [System.Serializable]
    public abstract class Calculation<T>
    {
        protected bool calculated = false;

        protected abstract T Calc(IAction context);

        public T Calculate(IAction context = null)
        {
            calculated = true;
            return Calc(context);
        }
    }
}