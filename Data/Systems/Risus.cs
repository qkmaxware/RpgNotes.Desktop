using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class Risus : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Risus",
            Abbreviation = "Risus",
            Description = "The Anything RPG!",
            Publication = new Publication {
                Publisher = "S. John Ross and Dave LeCompte",
                Url = "https://www.drivethrurpg.com/product/170294/Risus-The-Anything-RPG",
                YearOfPublication = 2016,
            },
            ThemeColour = new RGB(131, 37, 125),
            IconUrl = "static/images/logos/risus.svg",
        };
    }
}

}