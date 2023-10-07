using System.Collections.Generic;
using System.IO;

public class CsvReader
{
    private char fieldDelimiter;

    public CsvReader(char fieldDelimiter = ',')
    {
        this.fieldDelimiter = fieldDelimiter;
    }
    public List<List<string>> ReadCsv(string csvText)
    {
        List<List<string>> csvData = new List<List<string>>();

        string[] lines = csvText.Split('\n');

        foreach (string line in lines)
        {
            List<string> fields = ParseCsvLine(line);
            csvData.Add(fields);
        }
        return csvData;
    }
    public List<List<string>> ReadCsvFromPath(string filePath)
    {
        List<List<string>> csvData = new List<List<string>>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<string> fields = ParseCsvLine(line);
                csvData.Add(fields);
            }
        }

        return csvData;
    }

    public List<List<string>> ReadCsvFromPath(string filePath, bool isText)
    {
        List<List<string>> csvData = new List<List<string>>();

        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                List<string> fields = ParseCsvLine(line);
                csvData.Add(fields);
            }
        }

        return csvData;
    }

    private List<string> ParseCsvLine(string line)
    {
        List<string> fields = new List<string>();

        string field = "";
        bool insideQuotes = false;

        foreach (char c in line)
        {
            if (c == '"')
            {
                insideQuotes = !insideQuotes;
            }
            else if (c == fieldDelimiter && !insideQuotes)
            {
                fields.Add(field);
                field = "";
            }
            else
            {
                field += c;
            }
        }

        fields.Add(field);

        return fields;
    }
}