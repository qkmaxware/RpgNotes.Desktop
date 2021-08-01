namespace RpgNotes.Desktop.Data.Systems {

public class Pathfinder : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "Pathfinder",
        Abbreviation = "Pathfinder",
        Description = "",
        Publication = new Publication {
            Publisher = "Paizo",
            Url = "https://paizo.com/pathfinder",
            YearOfPublication = 2009,
        },
        ThemeColour = new RGB(225, 218, 145),
        IconImage = new Image { WebPath = "static/images/logos/pathfinder.svg" },
        Flags = new RpgSystemFlags {
            MultipleSpecies = true,
        },
    };
}

}