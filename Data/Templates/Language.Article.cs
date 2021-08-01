using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class LanguageArticle : IArticleTemplate {
    public string TemplateName() => "Language";
    public string TemplateHint() => "spoken or written language";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/lang.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article(); // Generic does nothing
        article.Tags = new List<string> {
            "language"
        };
        return article;
    }

}

}