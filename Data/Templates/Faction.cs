namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Faction = new SimpleTemplate(
        name: "Faction",
        icon: "static/images/icons/faction.logo.svg",
        desc: "Groups and factions",
        markdown: 
@"---
tags:
- faction
---"
    );

}

}