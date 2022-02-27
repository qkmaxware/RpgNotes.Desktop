using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class Fate : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Fate Core",
            Abbreviation = "Fate",
            Description = "Fate Core is a flexible system that can support whatever worlds you dream up.",
            Publication = new Publication {
                Publisher = "Evil Hat Productions",
                Url = "https://www.evilhat.com/home/fate-core/",
                YearOfPublication = 2013,
            },
            ThemeColour = new RGB(128, 153, 207),
            IconUrl = "static/images/logos/fate.svg",
        };
    }
}

}