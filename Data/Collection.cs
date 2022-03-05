using System.Collections.Generic;
using System.IO;

namespace RpgNotes.Desktop.Data {

public class Collection {
    public string Name;
    public bool Expanded;
    public FileInfo SourceFile;
    public List<Article> Articles;

    
    public List<string> Include {get; set;}
}

}