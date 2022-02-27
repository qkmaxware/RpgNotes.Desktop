using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class StarWars : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Star Wars Roleplaying Game",
            Abbreviation = "Star Wars",
            Description = "A long time ago, in a galaxy far far away...",
            Publication = new Publication {
                Publisher = "Fantasy Flight Games",
                Url = "https://www.fantasyflightgames.com/en/products/star-wars-the-roleplaying-game-30th-anniversary-edition/",
                YearOfPublication = 2013,
            },
            ThemeColour = new RGB(255, 197, 0),
            IconUrl = "static/images/logos/sw.svg"
        };
    }
}

}