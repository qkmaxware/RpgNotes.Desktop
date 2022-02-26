namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Quest = new SimpleTemplate(
        name: "Quest",
        icon: "static/images/icons/quests.logo.svg",
        markdown: 
@"---
given-by: 
started-at:
complete: false  
tags:
- quest
---"
    );

}

}