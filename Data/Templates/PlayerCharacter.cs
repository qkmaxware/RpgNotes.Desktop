namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template PC = new SimpleTemplate(
        name: "Player",
        icon: "static/images/icons/pc.logo.svg",
        markdown: 
@"---
age: 0
species: 
class:
alive: true
played-by: """"
stats:
    
tags:
- character
- player character
---"
    );

}

}