namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Planet = new SimpleTemplate(
        name: "Planet",
        icon: "static/images/icons/world.logo.svg",
        desc: "Planetary body",
        markdown: 
@"---
population: 0
biosphere: 
tags:
- location
- planet
---"
    );

}

}