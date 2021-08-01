using System.Text.Json;

namespace RpgNotes.Desktop.Data.Templates {

public class JsonTemplateArticle : IArticleTemplate {
    private string name;
    private string json;

    public string TemplateName() => name;
    public string TemplateHint() => name;
    public Image TemplateIcon() => null;

    /// <summary>
    /// Non-parameterless constructor. Makes this one invisble to the Article.FindArticleTemplates function.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="json"></param>
    public JsonTemplateArticle(string name, string json) {
        this.name = name;
        this.json = json;
    }

    public Article Create(RpgSystem system) {
        var article = JsonSerializer.Deserialize<Article>(json);
        return article;
    }

}

}