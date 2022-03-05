namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Region = new SimpleTemplate(
        name: "Region",
        icon: "static/images/icons/region.logo.svg",
        desc: "Geographic areas",
        markdown: 
@"---
tags:
- location
- region
---"
    );

}

}