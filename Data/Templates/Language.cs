namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Language = new SimpleTemplate(
        name: "Language",
        icon: "static/images/icons/lang.logo.svg",
        desc: "Written or spoken languages",
        markdown: 
@"---
spoken-by: 
tags:
- language
---"
    );

}

}