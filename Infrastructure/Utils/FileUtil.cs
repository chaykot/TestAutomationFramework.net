using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace Infrastructure.Utils
{
    public static class FileUtil
    {
        public static void WriteToCsv(this List<string> records,
            string fileName,
            string header = null,
            bool addEmptyLines = false)
        {
            using var writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}"));
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            if (header != null)
            {
                WriteRecord(csv, header);
            }
            records.ForEach(r =>
            {
                WriteRecord(csv, r);
                if (addEmptyLines)
                {
                    WriteRecord(csv, string.Empty);
                }
            });
        }

        public static List<string> ReadFromCsv(string fileName, bool hasHeader = false)
        {
            List<string> result = new();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = hasHeader
            };
            using var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}"));
            using var csv = new CsvReader(reader, config);
            if (config.HasHeaderRecord)
            {
                csv.Read();
                csv.ReadHeader();
            }
            while (csv.Read())
            {
                for (int i = 0; csv.TryGetField<string>(i, out var value); i++)
                {
                    result.Add(value);
                }
            }
            return result;
        }

        public static int GetNumberOfCsvRows(this string fileName)
        {
            using var reader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), $"{fileName}"));
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<dynamic>().Count();
        }

        private static void WriteRecord(IWriter writer, string record)
        {
            writer.WriteField(record);
            writer.NextRecord();
        }
    }
}