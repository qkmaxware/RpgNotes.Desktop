using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class LocationArticle : IArticleTemplate {
    public string TemplateName() => "Location";
    public string TemplateHint() => "places within the world";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/location.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        // Locations are map enabled
        article.Map = new Map(); // Though no map data by default
        article.Tags = new List<string> {
            "location"
        };
        return article; 
    }

}

}