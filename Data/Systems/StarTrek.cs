using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Systems {

public class StaBackground {
    public string Environment {get; set;}
    public string Upbringing {get; set;}
}

public class StaAssignment {
    public string Role {get; set;}
    public string AssignedTo {get; set;}
}

public class StaAttributes {
    public int Control {get; set;}
    public int Fitness {get; set;}
    public int Presence {get; set;}
    public int Daring {get; set;}
    public int Insight {get; set;}
    public int Reason {get; set;}
}

public class StaDisciplines {
    public int Command {get; set;}
    public int Security {get; set;}
    public int Science {get; set;}
    public int Conn {get; set;}
    public int Engineering {get; set;}
    public int Medicine {get; set;}
}

public class StaCharacterSheet : CharacterSheet {
    public string Species {get; set;}
    public string Rank {get; set;}
    public StaBackground Background {get; set;} = new StaBackground();
    public StaAssignment Assignment {get; set;} = new StaAssignment();
    public List<string> Traits {get; set;} = new List<string>();
    public StaAttributes Attributes {get; set;} = new StaAttributes();
    public StaDisciplines Disciplines {get; set;} = new StaDisciplines();
    public List<string> Values {get; set;} = new List<string>();
    public List<string> Talents {get; set;} = new List<string>();
    public List<string> Focuses {get; set;} = new List<string>();
}

public class StarTrekAdventures : IRpgSystemProvider {
    public IEnumerable<RpgSystem> Systems() {
        yield return new RpgSystem {
            Name = "Star Trek Adventures",
            Abbreviation = "STA",
            Description = "Starfleet needs a new crew! Welcome to your new assignment, Captain. Your continuing mission, to explore strange new worlds, seek out new life and new civilizations, to boldly go where no one has gone before. Star Trek Adventures is a Tabletop RPG where new discoveries await explorers of Starfleet.",
            Publication = new Publication {
                Publisher = "Mōdiphiüs Entertainment",
                Url = "https://www.modiphius.net/collections/star-trek-adventures",
                YearOfPublication = 2018,
            },
            ThemeColour = new RGB(192, 198, 199),
            IconUrl = "static/images/logos/sta.svg",

            CharacterSheetTemplate = new StaCharacterSheet(),
        };
    }
}

}