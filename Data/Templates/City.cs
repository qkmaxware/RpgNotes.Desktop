namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template City = new SimpleTemplate(
        name: "City",
        icon: "static/images/icons/city.logo.svg",
        desc: "Cities and towns",
        markdown: 
@"---
population: 0
tags:
- location
- city
---"
    );

}

}