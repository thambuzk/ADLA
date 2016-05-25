using System.Runtime.Serialization;

namespace MicrosoftIT.CustomExtractorADLALibrary.Model
{
    [DataContract]
    public class FileColumn
    {
        public FileColumn(string field, int isrequired, int position, int length, string datatype)
        {
            this.Field = field;
            this.IsRequired = isrequired;
            this.Position = position;
            this.Length = length;
            this.DataType = datatype;
        }
        [DataMember]
        public string Field { get; set; }
        [DataMember]
        public int IsRequired { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public int Length { get; set; }
        public string DataType { get; set; }
    }

}
