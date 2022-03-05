namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Item = new SimpleTemplate(
        name: "Item",
        icon: "static/images/icons/item.logo.svg",
        desc: "Weapons, potions, tools, and other items",
        markdown: 
@"---
tags:
- item
---"
    );

}

}