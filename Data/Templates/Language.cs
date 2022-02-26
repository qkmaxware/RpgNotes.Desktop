namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Language = new SimpleTemplate(
        name: "Language",
        icon: "static/images/icons/lang.logo.svg",
        markdown: 
@"---
spoken-by: 
tags:
- language
---"
    );

}

}