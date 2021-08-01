using System.Collections.Generic;
using RpgNotes.Desktop.Data.Custom;

namespace RpgNotes.Desktop.Data.Templates {

public class NpcCharacterArticle : IArticleTemplate {
    public string TemplateName() => "NPC";
    public string TemplateHint() => "non-player characters";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/npc.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.CustomDetails = new List<Custom.MetadataField> {
            new Custom.MetadataField {
                Name = "Age",
                Type = FieldType.Number,
                Value = 0.ToString()
            },
            new Custom.MetadataField {
                Name = "Alive",
                Type = FieldType.Boolean,
                Value = true.ToString()
            }
        };
        if (system.Flags != null && system.Flags.MultipleSpecies) {
            article.CustomDetails.Insert(
                0,
                new Custom.MetadataField {
                    Name = "Species",
                    Type = FieldType.String,
                    Value = string.Empty
                }
            );
        }
        if (system.Flags != null && system.Flags.ClassBased) {
            article.CustomDetails.Insert(
                    0,
                    new Custom.MetadataField {
                    Name = "Class",
                    Type = FieldType.String,
                    Value = string.Empty
                }
            );
        }
        article.Tags = new List<string> {
            "character",
            "non-player character"
        };

        return article;
    }

}


public class PlayerCharacterArticle : IArticleTemplate {
    public string TemplateName() => "Player";
    public string TemplateHint() => "player characters";
    public Image TemplateIcon() => new Image { WebPath="static/images/icons/pc.logo.svg" };

    public Article Create(RpgSystem system) {
        var article = new Article();
        article.CustomDetails = new List<Custom.MetadataField> {
            new Custom.MetadataField {
                Name = "Age",
                Type = FieldType.Number,
                Value = 0.ToString()
            },
            new Custom.MetadataField {
                Name = "Alive",
                Type = FieldType.Boolean,
                Value = true.ToString()
            },
            new Custom.MetadataField {
                Name = "Played By",
                Type = FieldType.String,
                Value = string.Empty
            },
        };
        if (system.Flags != null && system.Flags.MultipleSpecies) {
            article.CustomDetails.Insert(
                0,
                new Custom.MetadataField {
                    Name = "Species",
                    Type = FieldType.String,
                    Value = string.Empty
                }
            );
        }
        if (system.Flags != null && system.Flags.ClassBased) {
            article.CustomDetails.Insert(
                    0,
                    new Custom.MetadataField {
                    Name = "Class",
                    Type = FieldType.String,
                    Value = string.Empty
                }
            );
        }
        article.Tags = new List<string> {
            "character",
            "player character"
        };

        // TODO, add the character sheet to the page
        article.CharacterSheet = system.CharacterSheetTemplate?.Clone();
        
        return article;
    }

}

}