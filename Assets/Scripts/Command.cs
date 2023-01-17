namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Command
    {
        public abstract void Do(IAction action);
        public virtual void Undo() { }
    }
}
