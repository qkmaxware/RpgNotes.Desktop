namespace RpgNotes.Desktop.Data {

public abstract partial class Template {
    public static readonly Template None = new SimpleTemplate("General", "static/images/icons/article.logo.svg", "General purpose article", string.Empty);
    
    public string Name {get; set;}
    public string Description {get; set;}
    public string IconUrl {get; set;}
    public TemplateOptions Options {get; set;} = null;
    public abstract string Markdown();
}

/// <summary>
/// Options for template generation
/// </summary>
public class TemplateOptions { }

/// <summary>
/// Simple template definition
/// </summary>
public class SimpleTemplate : Template {
    private string md;
    public SimpleTemplate(string name, string icon, string desc, string markdown) {
        this.Name = name;
        this.IconUrl = icon;
        this.Description = desc;
        this.md = markdown;
    }
    public System.Func<Template, string, string> Transformation {get; set;}
    public override string Markdown() {
        if (Transformation == null) {
            return md;
        } else {
            return Transformation.Invoke(this, md);
        }
    }
}

}