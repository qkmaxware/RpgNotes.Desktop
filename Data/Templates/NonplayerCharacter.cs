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
            var o = (CharacterTemplateOptions)self.Options;
            return tpl.Replace("{{ sheet }}", o.System.CharacterSheetTemplate?.Yaml() ?? string.Empty);
        }
    };

}

}