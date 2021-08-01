using System.Collections.Generic;
using RpgNotes.Desktop.Data.Custom;

namespace RpgNotes.Desktop.Data.Templates {

public class QuestArticle : IArticleTemplate {
    public string TemplateName() => "Quest";
    public string TemplateHint() => "quests";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/quests.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.CustomDetails = new List<MetadataField> {
            new Custom.MetadataField {
                Name = "Complete",
                Type = FieldType.Boolean,
                Value = true.ToString()
            }
        };
        article.Tags = new List<string> {
            "quest"
        };
        return article; 
    }

}

}