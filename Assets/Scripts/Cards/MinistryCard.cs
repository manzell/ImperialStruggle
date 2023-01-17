namespace ImperialStruggle
{
    public class MinistryCard : ICard, ISelectable
    {
        public enum Keyword { Style, Governance, Mercantilism, Scholarship, Finance }
        public enum MinistryCardStatus { Reserved, Selected, Revealed, Exhausted }

        public System.Action UISelectionEvent { get; set; }
        public System.Action UIDeselectEvent { get; set; }

        public MinistryCardStatus ministryCardStatus { get; private set; }
        public MinistryCardData data { get; private set; }

        public string Name => data.name;

        public MinistryCard(MinistryCardData data) => this.data = data;

        public void SetMinistryCardStatus(MinistryCardStatus status) => ministryCardStatus = status;
    }
}