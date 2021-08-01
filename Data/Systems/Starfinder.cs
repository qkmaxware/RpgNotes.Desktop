namespace RpgNotes.Desktop.Data.Systems {

public class Starfinder : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "Starfinder",
        Abbreviation = "Starfinder",
        Description = "",
        Publication = new Publication {
            Publisher = "Paizo",
            Url = "https://paizo.com/starfinder",
            YearOfPublication = 2017,
        },
        ThemeColour = new RGB(36, 163, 231),
        IconImage = new Image { WebPath = "static/images/logos/starfinder.svg" },
        Flags = new RpgSystemFlags {
            MultipleSpecies = true,
        },
    };
}

}