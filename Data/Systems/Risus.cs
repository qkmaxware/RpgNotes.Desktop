namespace RpgNotes.Desktop.Data.Systems {

public class Risus : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "Risus",
        Abbreviation = "Risus",
        Description = "The Anything RPG!",
        Publication = new Publication {
            Publisher = "S. John Ross and Dave LeCompte",
            Url = "https://www.drivethrurpg.com/product/170294/Risus-The-Anything-RPG",
            YearOfPublication = 2016,
        },
        ThemeColour = new RGB(131, 37, 125),
        IconImage = new Image { WebPath = "static/images/logos/risus.svg" },
        Flags = new RpgSystemFlags {},
    };
}

}