namespace RpgNotes.Desktop.Data.Templates {

public class GenericArticle : IArticleTemplate {
    public string TemplateName() => "Generic";
    public string TemplateHint() => "general purpose articles";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/article.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article(); // Generic does nothing
        return article;
    }

}

}