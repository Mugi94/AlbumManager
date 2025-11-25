using BusinessObjects.Enum;

namespace BusinessObjects.DataTransferObject
{
    public class RecordSummary
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public TypeRecord Type { get; set; }
    }
}