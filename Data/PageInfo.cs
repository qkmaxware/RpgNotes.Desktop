using System.IO;

namespace RpgNotes {

public class PageInfo {
    private string home;
    public string Editor {get; private set;}
    public string Path {get; private set;}
    public string Title {get; private set;}
    public string Url => $"home/{home}/" + Editor + (string.IsNullOrEmpty(this.Path) ? string.Empty : "/" + Path.EncodeBase64());
    public PageInfo(string home, string editor, string path, string title = null) {
        this.home = home;
        this.Editor = editor;
        this.Path = path;
        this.Title = title ?? System.IO.Path.GetFileNameWithoutExtension(this.Path);
    }
    public PageInfo(DirectoryInfo home, string editor, string path, string title = null) {
        this.home = home.FullName.EncodeBase64();
        this.Editor = editor;
        this.Path = path;
        this.Title = title ?? System.IO.Path.GetFileNameWithoutExtension(this.Path);
    }
    public PageInfo(DirectoryInfo home, string editor, FileInfo file, string title = null) {
        this.home = home.FullName.EncodeBase64();
        this.Editor = editor;
        this.Path = file.FullName;
        this.Title = title ?? System.IO.Path.GetFileNameWithoutExtension(this.Path);
    }
}

}