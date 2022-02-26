namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Technology = new SimpleTemplate(
        name: "Technology",
        icon: "static/images/icons/tech.logo.svg",
        markdown: 
@"---
tags:
- technology
---"
    );

}

}