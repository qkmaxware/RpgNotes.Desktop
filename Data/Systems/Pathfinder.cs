using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class Pathfinder : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Pathfinder",
            Abbreviation = "Pathfinder",
            Description = "Take the first step into an amazing world of fantasy adventure!",
            Publication = new Publication {
                Publisher = "Paizo",
                Url = "https://paizo.com/pathfinder",
                YearOfPublication = 2009,
            },
            ThemeColour = new RGB(225, 218, 145),
            IconUrl = "static/images/logos/pathfinder.svg",

            CharacterSheetTemplate = new DnD5eCharacterSheet()
        };
    }
}

}