using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class ConditionArticle : IArticleTemplate {
    public string TemplateName() => "Condition";
    public string TemplateHint() => "conditions and status effects";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/condition.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.Tags = new List<string> {
            "condition"
        };
        return article; 
    }

}

}