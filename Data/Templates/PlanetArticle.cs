using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class PlanetArticle : IArticleTemplate {
    public string TemplateName() => "Planet";
    public string TemplateHint() => "planetary bodies";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/world.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        // Locations are map enabled
        article.Map = new Map(); // Though no map data by default
        article.Tags = new List<string> {
            "location",
            "planet"
        };
        return article; 
    }

}

}