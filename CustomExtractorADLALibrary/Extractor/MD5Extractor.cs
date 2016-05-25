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
    [SqlUserDefinedExtractor(AtomicFileProcessing = true)]
    public class MD5Extractor : IExtractor
    {
        private string md5Hashsource;
        private string fileName;

        public MD5Extractor()
        {
        }

        public MD5Extractor(string filename, string md5hashsource)
        {
            this.fileName = filename;
            this.md5Hashsource = md5hashsource;
        }

        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            string md5Hash;
            using (var md5 = MD5.Create())
            {
                md5Hash = BitConverter.ToString(md5.ComputeHash(input.BaseStream)).Replace("-", "");
                if (!String.IsNullOrEmpty(md5Hashsource))
                {
                    if (!String.Equals(md5Hash, md5Hashsource))
                    {
                        throw new Exception(this.fileName + " : MD5 Hash does not match with source");
                    }
                }
            }
            output.Set<string>(0, md5Hash);
            yield return output.AsReadOnly();

        }
    }
}
