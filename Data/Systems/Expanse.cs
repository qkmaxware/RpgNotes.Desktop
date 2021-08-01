namespace RpgNotes.Desktop.Data.Systems {

public class Expanse : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "The Expanse Roleplaying Game",
        Abbreviation = "The Expanse",
        Description = "Sci-Fi roleplaying at humanity's edge",
        Publication = new Publication {
            Publisher = "Green Ronin Publishing",
            Url = "https://greenroninstore.com/collections/the-expanse-rpg",
            YearOfPublication = 2019,
        },
        ThemeColour = new RGB(255, 255, 255),
        IconImage = new Image { WebPath = "static/images/logos/expanse.svg" },
        Flags = new RpgSystemFlags {},
    };
}

}