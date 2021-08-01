using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Templates {

public class RegionArticle : IArticleTemplate {
    public string TemplateName() => "Region";
    public string TemplateHint() => "bounded area with particular traits";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/region.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        // Locations are map enabled
        article.Map = new Map(); // Though no map data by default
        article.Tags = new List<string> {
            "region"
        };
        return article; 
    }

}

}