using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class ItemArticle : IArticleTemplate {
    public string TemplateName() => "Item";
    public string TemplateHint() => "items";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/item.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.Tags = new List<string> {
            "item"
        };
        return article; 
    }

}

}