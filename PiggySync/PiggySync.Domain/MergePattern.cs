namespace PiggySync.Domain
{
    public class MergePattern
    {
        public string AggregateStartTag { get; set; }
        public string AggregateStopTag { get; set; }
        public string[] TagOpenString { get; set; }

    }
}