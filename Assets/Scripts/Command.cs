namespace ImperialStruggle
{
    [System.Serializable]
    public abstract class Command
    {
        public abstract void Do(GameAction action);
        public virtual void Undo() { }
    }
}
