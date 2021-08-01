using System.Collections.Generic;
using RpgNotes.Desktop.Data.Custom;

namespace RpgNotes.Desktop.Data.Templates {

public class ConflictArticle : IArticleTemplate {
    public string TemplateName() => "Conflict";
    public string TemplateHint() => "conflict";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/conflict.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article(); // Generic does nothing
        article.CustomDetails = new List<MetadataField> {
            new MetadataField {
                Name = "Casualties",
                Type = FieldType.Number,
                Value = "0"
            }
        };
        article.Tags = new List<string> {
            "conflict"
        };
        return article;
    }

}

}