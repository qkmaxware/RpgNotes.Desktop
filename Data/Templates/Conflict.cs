namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Conflict = new SimpleTemplate(
        name: "Conflict",
        icon: "static/images/icons/conflict.logo.svg",
        markdown: 
@"---
timestamp: 
casualties: 0
tags:
- conflict
---"
    );

}

}