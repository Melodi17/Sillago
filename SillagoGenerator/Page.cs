namespace SillagoGenerator;

using System.Collections;
using System.Text;

public class Page : IEnumerable<Row>
{
    public readonly string Name;
    private readonly string _url;
    private readonly List<Row> _rows = new();

    public Page(string name, string url)
    {
        this.Name = name;
        this._url = url;
        this.Load();
    }

    public Row this[int index]
    {
        get
        {
            if (index < 0 || index >= this._rows.Count)
                throw new IndexOutOfRangeException($"Row index {index} is out of range.");
            return this._rows[index];
        }
    }
    
    public IEnumerator<Row> GetEnumerator() => this._rows.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    
    private void Load()
    {
        using HttpClient client = new();
        string csvData = client.GetStringAsync(this._url).Result;

        using StringReader reader = new(csvData);
        string? headerLine = reader.ReadLine();
        if (headerLine == null)
            throw new Exception("CSV data is empty.");

        string[] headers = this.ParseCsvLine(headerLine).ToArray();

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] values = this.ParseCsvLine(line).ToArray();
            Dictionary<string, string> row = new();

            for (int i = 0; i < headers.Length; i++)
            {
                string header = headers[i];
                string value = i < values.Length ? values[i] : string.Empty;
                row[header] = value;
            }

            this._rows.Add(new Row(row));
        }
    }

    private IEnumerable<string> ParseCsvLine(string line)
    {
        bool inQuotes = false;
        StringBuilder valueBuilder = new();

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (c == '\"')
            {
                if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                {
                    valueBuilder.Append('\"');
                    i++;
                }
                else
                {
                    inQuotes = !inQuotes;
                }
            }
            else if (c == ',' && !inQuotes)
            {
                yield return valueBuilder.ToString();
                valueBuilder.Clear();
            }
            else
            {
                valueBuilder.Append(c);
            }
        }

        yield return valueBuilder.ToString();
    }
}

public class Row
{
    private readonly Dictionary<string, string> _data;

    public Row(Dictionary<string, string> data)
    {
        this._data = data;
    }

    public string this[string columnName]
    {
        get
        {
            if (this._data.TryGetValue(columnName, out string? value))
                return value;
            throw new KeyNotFoundException($"Column '{columnName}' not found in row.");
        }
    }
    
    public IEnumerable<string> Columns => this._data.Keys;
    public IEnumerable<string> Values => this._data.Values;
}