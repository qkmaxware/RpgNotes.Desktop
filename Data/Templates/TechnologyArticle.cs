using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class TechnologyArticle : IArticleTemplate {
    public string TemplateName() => "Technology";
    public string TemplateHint() => "technology";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/tech.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.Tags = new List<string> {
            "technology"
        };
        return article; 
    }

}

}