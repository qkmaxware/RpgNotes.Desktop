using System.Collections.Generic;
using RpgNotes.Desktop.Data.Custom;

namespace RpgNotes.Desktop.Data.Templates {

public class VehicleArticle : IArticleTemplate {
    public string TemplateName() => "Vehicle";
    public string TemplateHint() => "vehicles and transportation";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/vehicle.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article(); // Generic does nothing
        article.CustomDetails = new List<MetadataField> {
            new MetadataField {
                Name = "Manufacturer",
                Type = FieldType.String,
                Value = null
            },
            MetadataField.AsSpeed("Top Speed")
        };
        article.Tags = new List<string> {
            "vehicle"
        };
        return article;
    }

}

}