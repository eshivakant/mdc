namespace MDC.ContributionService.Common
{
    public class RequestField
    {
        public FieldType Type { get; set; }
        public string FieldName { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return $"{FieldName} ({Type.ToString()}) {Value}";
        }
    }
}