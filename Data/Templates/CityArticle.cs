using System.Collections.Generic;
using RpgNotes.Desktop.Data.Custom;

namespace RpgNotes.Desktop.Data.Templates {

public class CityArticle : IArticleTemplate {
    public string TemplateName() => "City";
    public string TemplateHint() => "cities";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/city.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.CustomDetails = new List<MetadataField> {
            new MetadataField {
                Name = "Population",
                Type = FieldType.Number,
                Value = "0"
            }
        };
        article.Tags = new List<string> {
            "location",
            "city",
        };
        // Locations are map enabled
        article.Map = new Map(); // Though no map data by default
        return article; 
    }

}

}