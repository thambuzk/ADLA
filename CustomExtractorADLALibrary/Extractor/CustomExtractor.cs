using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftIT.CustomExtractorADLALibrary.Model
{
    
    public class FixedPositionExtractor : IExtractor
    {
        private string fileidentifier;
        private FileTemplate fileTemplate;

        public FixedPositionExtractor()
        {
        }

        public FixedPositionExtractor(string fileidentifier)
        {
            this.fileidentifier = fileidentifier;
            fileTemplate = FileTemplateLoad.GetFileTemplate(fileidentifier);
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            char delimiter = ',';
            if (fileTemplate.FileType == "Delimited")
            {
                delimiter = (char)fileTemplate.FileDelimiterASCIIValue;
            }
            // Read the input stream
            using (StreamReader reader = new StreamReader(input.BaseStream))
            {
                // Rows
                while (reader.Peek() >= 0)
                {
                    try
                    {
                        string line = reader.ReadLine();
                        if (fileTemplate.FileType == "FixedPosition")
                        {
                            for (int i = 0; i < fileTemplate.FileColumns.Count; i++)
                            {
                                if (fileTemplate.FileColumns[i].Position == -1)
                                {
                                    output.Set<string>(fileTemplate.FileColumns[i].Field, "");
                                }
                                else
                                {
                                    output.Set<string>(fileTemplate.FileColumns[i].Field, line.Substring(fileTemplate.FileColumns[i].Position, fileTemplate.FileColumns[i].Length));
                                }
                            }
                        }
                        else if (fileTemplate.FileType == "Delimited")
                        {
                            string[] lineitems = line.Split(delimiter);
                            for (int i = 0; i < fileTemplate.FileColumns.Count; i++)
                            {
                                if (fileTemplate.FileColumns[i].Position == -1)
                                {
                                    output.Set<string>(fileTemplate.FileColumns[i].Field, "");
                                }
                                else
                                {
                                    output.Set<string>(fileTemplate.FileColumns[i].Field, lineitems[fileTemplate.FileColumns[i].Position-1]);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    yield return output.AsReadOnly();
                }
            }

        }
    }
}
