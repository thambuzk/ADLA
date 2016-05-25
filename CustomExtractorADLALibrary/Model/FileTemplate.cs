using System.Collections.Generic;

namespace MicrosoftIT.CustomExtractorADLALibrary.Model
{
    public enum FileType
    {
        FixedPosition,Delimited
    }
    public class FileTemplate
    {
        public string FileIdentifier { get; set; }
        public string TableName { get; set; }
        public string FileType { get; set; }
        public int FileDelimiterASCIIValue { get; set; }
        public List<FileColumn> FileColumns { get; set; }

    }

}

