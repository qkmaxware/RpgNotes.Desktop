namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template PC = new SimpleTemplate(
        name: "Player",
        icon: "static/images/icons/pc.logo.svg",
        desc: "Characters controlled by players",
        markdown: 
@"---
age: 0
alive: true
played-by: """"
{{ sheet }}
tags:
- character
- player character
---"
    ) {
        Options = new CharacterTemplateOptions(),
        Transformation = (self, tpl) => {
            if (self.Options != null && self.Options is CharacterTemplateOptions o) {
                return tpl.Replace("{{ sheet }}", o.System.CharacterSheetTemplate.Yaml());
            } else {
                return tpl.Replace("{{ sheet }}", string.Empty);
            }
        }
    };

}

}