namespace RpgNotes.Desktop.Data {

public class CharacterTemplateOptions : TemplateOptions {
    public RpgSystem System {get; set;}
}

public partial class Template {

    public static readonly Template NPC = new SimpleTemplate(
        name: "NPC",
        icon: "static/images/icons/npc.logo.svg",
        desc: "Character controlled by the game master",
        markdown: 
@"---
age: 0
alive: true
{{ sheet }}
tags:
- character
- non-player character
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