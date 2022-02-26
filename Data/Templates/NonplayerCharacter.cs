namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template NPC = new SimpleTemplate(
        name: "NPC",
        icon: "static/images/icons/npc.logo.svg",
        markdown: 
@"---
age: 0
species: 
class:
alive: true
stats:
    
tags:
- character
- non-player character
---"
    );

}

}