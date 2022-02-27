using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class DungeonWorldCharacterSheet : CharacterSheet {
    public string Race {get; set;}
    public string Class {get; set;}
    public DnD5eLevel Level {get; set;} = new DnD5eLevel();
    public string Alignment {get; set;}
    public int ArmorClass {get; set;}
    public int HitPoints {get; set;}
    public DnD5eAttributes Abilities {get; set;} = new DnD5eAttributes();
    public List<string> Conditions {get; set;} = new List<string>();
    public List<string> Moves {get; set;} = new List<string>() {
        
    };
}

public class DungeonWorld : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Dungeon World",
            Abbreviation = "DW",
            Description = "Wave hands like a muppet! Play to find out what happens.",
            Publication = new Publication {
                Publisher = "Sage LaTorra and Adam Koebel",
                Url = "https://dungeon-world.com/",
                YearOfPublication = 2015,
            },
            ThemeColour = new RGB(106, 126, 114),
            IconUrl = "static/images/logos/dw.svg",

            CharacterSheetTemplate = new DungeonWorldCharacterSheet(),
        };
    }
}

}