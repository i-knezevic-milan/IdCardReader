namespace IdCardReaderApi.Models
{
    public class IdCardData
    {
        public DocumentData DocumentData { get; set; }
        public FixedPersonalData FixedPersonalData { get; set; }
        public VariablePersonalData VariablePersonalData { get; set; }
        public Portrait Portrait { get; set; }
    }
}
