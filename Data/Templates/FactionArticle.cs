using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class FactionArticle : IArticleTemplate {
    public string TemplateName() => "Faction";
    public string TemplateHint() => "groups and factions";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/faction.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.Tags = new List<string> {
            "faction"
        };
        return article; 
    }

}

}