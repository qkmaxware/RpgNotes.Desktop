using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class CallOfCthulhu : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Call of Cthulhu",
            Abbreviation = "CoC",
            Description = "Call of Cthulhu is set in a darker version of our world set in the 1900s and based on the works of H. P. Lovecraft.",
            Publication = new Publication {
                Publisher = "Chaosium Inc.",
                Url = "https://www.chaosium.com/call-of-cthulhu-rpg/",
                YearOfPublication = 1981,
            },
            ThemeColour = new RGB(0, 0, 0),
            IconUrl = "static/images/logos/cthulhu.svg"
        };
    }
}

}