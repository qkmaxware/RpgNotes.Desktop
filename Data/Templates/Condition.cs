namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Condition = new SimpleTemplate(
        name: "Condition",
        icon: "static/images/icons/condition.logo.svg",
        desc: "Conditions and status effects",
        markdown: 
@"---
duration:
tags:
- condition
---"
    );

}

}