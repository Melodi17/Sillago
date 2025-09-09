namespace SillagoGenerator;

using System.Collections;

public class Sheet : IEnumerable<Page>
{
    private readonly string _url;
    private readonly List<Page> _pages = new();

    public Sheet(string url)
    {
        this._url = url;
        this.Load();
    }

    public Page? GetPageByName(string name)
    {
        return this._pages.Find(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public Page this[string name]
    {
        get => this.GetPageByName(name)
               ?? throw new KeyNotFoundException($"Page with name '{name}' not found.");
    }
    
    public IEnumerator<Page> GetEnumerator() => this._pages.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private void Load()
    {
        using HttpClient client = new();
        string htmlData = client.GetStringAsync(this._url).Result;

        // items.push({name: "Substances and Metals", pageUrl: "https:\/\/docs.google.com\/spreadsheets\/d\/e\/2PACX-1vRwlJnemAVQvnasfytRMozXREXuvsC5cKhbudh4I7uMKbRSjubAqGnDXfMGrToP8hg7NLmK8MOZYlWr\/pubhtml\/sheet?headers\x3dfalse&gid=668479616", gid: "668479616"
        string marker = "items.push({name: \"";
        int index = 0;
        while (true)
        {
            int startIndex = htmlData.IndexOf(marker, index);
            if (startIndex == -1)
                break;

            int nameStart = startIndex + marker.Length;
            int nameEnd = htmlData.IndexOf('"', nameStart);
            string name = htmlData[nameStart..nameEnd];

            string pageUrlMarker = "pageUrl: \"";
            int urlStartIndex = htmlData.IndexOf(pageUrlMarker, nameEnd) + pageUrlMarker.Length;
            int urlEndIndex = htmlData.IndexOf('"', urlStartIndex);
            string pageUrl = htmlData[urlStartIndex..urlEndIndex].Replace("\\/", "/");

            pageUrl += "&output=csv";
            pageUrl = pageUrl.Replace("pubhtml/sheet?", "pub?");

            this._pages.Add(new Page(name, pageUrl));

            index = urlEndIndex;
        }
    }
}