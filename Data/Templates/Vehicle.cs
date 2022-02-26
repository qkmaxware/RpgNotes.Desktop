namespace RpgNotes.Desktop.Data {

public partial class Template {

    public static readonly Template Vehicle = new SimpleTemplate(
        name: "Vehicle",
        icon: "static/images/icons/vehicle.logo.svg",
        markdown: 
@"---
owner: 
manufacturer: 
modal: 
cost: 
speed:
    air:
    water:
    land: 
tags:
- vehicle
---"
    );

}

}