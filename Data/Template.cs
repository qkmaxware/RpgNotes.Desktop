namespace RpgNotes.Desktop.Data {

public abstract partial class Template {
    public static readonly Template None = new SimpleTemplate("General", "static/images/icons/article.logo.svg", string.Empty);
    
    public string Name {get; set;}
    public string IconUrl {get; set;}
    public abstract string Markdown();
}

/// <summary>
/// Simple template definition
/// </summary>
public class SimpleTemplate : Template {
    private string md;
    public SimpleTemplate(string name, string icon, string markdown) {
        this.Name = name;
        this.IconUrl = icon;
        this.md = markdown;
    }
    public override string Markdown() => md;
}

}