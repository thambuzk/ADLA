using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MicrosoftIT.CustomExtractorADLALibrary.Model
{
    public static class FileTemplateLoad
    {
        private static List<FileTemplate> FileTemplates;
        private static List<Table> Tables;

        static FileTemplateLoad()
        {
            LoadFiles();
        }

        private static void LoadFiles()
        {
            LoadFileTemplates();
            LoadTables();
        }
        private static T Deserialize<T>(string filepath)
        {
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                T obj = Activator.CreateInstance<T>();
                MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
                return obj;
            }
        }

        private static void LoadFileTemplates()
        {
            if (FileTemplates == null)
                FileTemplates = Deserialize<List<FileTemplate>>(@"Format.json");
        }

        private static void LoadTables()
        {
            if (Tables == null)
                Tables = Deserialize<List<Table>>(@"Tables.json");
        }

        public static FileTemplate GetFileTemplate(string FileIdentifier)
        {
            FileTemplate f = FileTemplates.Where(f1 => f1.FileIdentifier == FileIdentifier).First();
            Table t = Tables.Where(t1 => t1.TableName == f.TableName).First();

            //loop thru each of the column in table. 
            //if the column is not in the template add to the template
            //if the column exists append with the datatype.
            foreach (TableColumn tc in t.TableColumns)
            {
                if (!f.FileColumns.Exists(fc => fc.Field == tc.ColumnName))
                {
                    FileColumn fc = new FileColumn(tc.ColumnName, 0, -1, -1, tc.DataType);
                    f.FileColumns.Add(fc);
                }
                else
                {
                    FileColumn ftc = f.FileColumns.Where(fc => fc.Field == tc.ColumnName).First();
                    ftc.DataType = tc.DataType;
                }
            }
            return f;
        }

    }
}

