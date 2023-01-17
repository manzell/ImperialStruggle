namespace ImperialStruggle
{
    public class Squadron : ISelectable
    {
        public Faction flag;
        public NavalSpace space;

        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public string Name => space == null ? $"{flag.Name} Squadron (Port)" : $"{flag.Name} Squadron in {space.Name}"; 
    }
}