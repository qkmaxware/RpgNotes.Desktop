namespace RpgNotes.Desktop.Data.Systems {

public class StarTrekAdventures : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "Star Trek Adventures",
        Abbreviation = "STA",
        Description = "Starfleet needs a new crew! Welcome to your new assignment, Captain. Your continuing mission, to explore strange new worlds, seek out new life and new civilizations, to boldly go where no one has gone before. Star Trek Adventures is a Tabletop RPG where new discoveries await explorers of Starfleet.",
        Publication = new Publication {
            Publisher = "Mōdiphiüs Entertainment",
            Url = "https://www.modiphius.net/collections/star-trek-adventures",
            YearOfPublication = 2018,
        },
        ThemeColour = new RGB(192, 198, 199),
        IconImage = new Image { WebPath = "static/images/logos/sta.svg" },
        Flags = new RpgSystemFlags {
            MultipleSpecies = true,
        },
        CharacterSheetTemplate = new CharacterSheet {
            Rows = new CharacterBlockGroup[] {
                new CharacterBlockGroup {
                    Layout = Layout.FullWidth,
                    Blocks = new CharacterBlock[] {
                        new CharacterBlock {
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "History",
                                    Layout = Layout.TwoColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsText("Environment"),
                                        Aspect.AsText("Upbringing")
                                    }
                                },
                                new AspectSection {
                                    Name = "Career",
                                    Layout = Layout.TwoColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsText("Assignment"),
                                        Aspect.AsNumber("Years Experience")
                                    }
                                }
                            }
                        },
                    }
                },
                new CharacterBlockGroup {
                    Layout = Layout.TwoColumns,
                    Blocks = new CharacterBlock[] {
                        new CharacterBlock {
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "Attributes",
                                    Layout = Layout.ThreeColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsNumber("Control"),
                                        Aspect.AsNumber("Fitness"),
                                        Aspect.AsNumber("Presence"),
                                        Aspect.AsNumber("Daring"),
                                        Aspect.AsNumber("Insight"),
                                        Aspect.AsNumber("Reason"),
                                    }
                                },
                                new AspectSection {
                                    Name = "Values",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsParagraph(""),
                                        Aspect.AsNumber("Determination")
                                    }
                                },
                                new AspectSection {
                                    Name = "Talents",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsParagraph(""),
                                    }
                                }
                            }
                        },
                        new CharacterBlock {
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "Focuses",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsText("")
                                    }
                                },
                                new AspectSection {
                                    Name = "Stress",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsNumber(""),
                                        Aspect.AsParagraph("Injuries")
                                    }
                                },
                            }
                        }
                    }
                }
            }
        }
    };

}

}