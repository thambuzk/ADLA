namespace MicrosoftIT.CustomExtractorADLALibrary.Model
{
    public class TableColumn
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int IsRequired { get; set; }
        public int Position { get; set; }
        public int Length { get; set; }
    }
}
