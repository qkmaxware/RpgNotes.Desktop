using System;
using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class DnD5eAttribute {
    public int Value {get; set;} = 10;
    public int Modifier => (int)Math.Round((Value - 10) / 2.0, MidpointRounding.ToZero);
}

public class DnD5eAttributes {
    public DnD5eAttribute Strength {get; set;} = new DnD5eAttribute();
    public DnD5eAttribute Dexterity {get; set;} = new DnD5eAttribute();
    public DnD5eAttribute Constitution {get; set;} = new DnD5eAttribute();
    public DnD5eAttribute Intelligence {get; set;} = new DnD5eAttribute();
    public DnD5eAttribute Wisdom {get; set;} = new DnD5eAttribute();
    public DnD5eAttribute Charisma {get; set;} = new DnD5eAttribute();   
}

public class DnD5eSpeed {
    public float Walking {get; set;} = 30;
    public float Climbing {get; set;}
    public float Flying {get; set;}
}

public class DnD5eLevel {
    public int Level {get; set;} = 1;
    public int Experience {get; set;}
}

public class DnD5eCharacterSheet : CharacterSheet {
    public string Race {get; set;}
    public string Class {get; set;}
    public DnD5eLevel Level {get; set;} = new DnD5eLevel();
    public string Alignment {get; set;}
    public int ArmorClass {get; set;}
    public int HitPoints {get; set;}
    public DnD5eAttributes Abilities {get; set;} = new DnD5eAttributes();
    public DnD5eSpeed Speed {get; set;} = new DnD5eSpeed();
    public List<string> Proficiencies {get; set;} = new List<string>();
    public List<string> Languages {get; set;} = new List<string>() {
        "Common"
    };
}

public class DnD5e : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Dungeons & Dragons 5th Edition",
            Abbreviation = "D&D 5E",
            Description = "A cooperative storytelling game harnessing ones imagination to explore a medieval fantasy world. Fight monsters, find treasure, and overcome great dangers.",
            Publication = new Publication {
                Publisher = "Wizards of the Coast",
                Url = "https://dnd.wizards.com/",
                YearOfPublication = 2014,
            },
            ThemeColour = new RGB(255, 0, 0),
            IconUrl = "static/images/logos/dnd.svg",

            CharacterSheetTemplate = new DnD5eCharacterSheet()
        };
    }
}

}