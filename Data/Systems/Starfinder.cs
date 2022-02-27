using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class Starfinder : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Starfinder",
            Abbreviation = "Starfinder",
            Description = "Blast off into a galaxy of adventure.",
            Publication = new Publication {
                Publisher = "Paizo",
                Url = "https://paizo.com/starfinder",
                YearOfPublication = 2017,
            },
            ThemeColour = new RGB(36, 163, 231),
            IconUrl = "static/images/logos/starfinder.svg",
        };
    }
}

}