namespace RpgNotes.Desktop.Data.Systems {

public class DnD5E : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "Dungeons & Dragons 5th Edition",
        Abbreviation = "D&D 5E",
        Description = "A cooperative storytelling game harnessing ones imagination to explore a medieval fantsy world. Fight monsters, find treasure, and overcome great dangers.",
        Publication = new Publication {
            Publisher = "Wizards of the Coast",
            Url = "https://dnd.wizards.com/",
            YearOfPublication = 2014,
        },
        ThemeColour = new RGB(255, 0, 0),
        IconImage = new Image { 
            WebPath = "static/images/logos/dnd.svg" 
        },
        Flags = new RpgSystemFlags {
            MultipleSpecies = true,
            ClassBased = true,
        },
        CharacterSheetTemplate = new CharacterSheet {
            Rows = new CharacterBlockGroup[] {
                new CharacterBlockGroup {
                    Layout = Layout.ThreeColumns,
                    Blocks = new CharacterBlock[] {
                        new CharacterBlock {
                            Name = "",
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "",
                                    Layout = Layout.ThreeColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsNumber("AC"),
                                        Aspect.AsNumber("Initiative"),
                                        Aspect.AsNumber("Speed"),
                                        Aspect.AsBool("Inspiration"),
                                        Aspect.AsNumber("Proficiency"),
                                        Aspect.AsNumber("Passive Perception"),
                                    }
                                },
                                new AspectSection {
                                    Name = "Hit Points",
                                    Layout = Layout.TwoColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsText("Hit Dice"),
                                        Aspect.AsNumber("Max"),
                                        Aspect.AsNumber("Current"),
                                        Aspect.AsNumber("Temporary"),
                                    }
                                },
                                new AspectSection {
                                    Name = "Death Saves",
                                    Layout = Layout.TwoColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsNumber("Successes"),
                                        Aspect.AsNumber("Failures")
                                    }
                                },
                                new AspectSection {
                                    Name = "",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsParagraph("Attacks & Spellcasting")
                                    }
                                }
                            }
                        },
                        new CharacterBlock {
                            Name = "",
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "Abilities",
                                    Layout = Layout.ThreeColumns,
                                    Aspects = new Aspect[] {
                                        new Aspect {
                                            Name = "Strength",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Dexterity",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Constitution",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Intelligence",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Wisdom",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Charisma",
                                            Value = "0",
                                            Type = AspectType.Number
                                        }
                                    }
                                },
                                new AspectSection {
                                    Name = "Saving Throws",
                                    Layout = Layout.ThreeColumns,
                                    Aspects = new Aspect[] {
                                        new Aspect {
                                            Name = "Strength",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Dexterity",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Constitution",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Intelligence",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Wisdom",
                                            Value = "0",
                                            Type = AspectType.Number
                                        },
                                        new Aspect {
                                            Name = "Charisma",
                                            Value = "0",
                                            Type = AspectType.Number
                                        }
                                    }
                                },
                            }
                        },
                        new CharacterBlock {
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "Traits",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsParagraph("Personality"),
                                        Aspect.AsParagraph("Ideals"),
                                        Aspect.AsParagraph("Bonds"),
                                        Aspect.AsParagraph("Flaws")
                                    }
                                },
                                new AspectSection {
                                    Name = "Features",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsParagraph(string.Empty)
                                    }
                                }
                            }
                        }
                    }
                }, 
                new CharacterBlockGroup {
                    Layout = Layout.FullWidth,
                    Blocks = new CharacterBlock [] {
                        new CharacterBlock {
                            Name = "",
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "Skills",
                                    Layout = Layout.FourColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsNumber("Acrobatics (dex)"),
                                        Aspect.AsNumber("Animal Handling (wis)"),
                                        Aspect.AsNumber("Arcana (int)"),
                                        Aspect.AsNumber("Athletics (str)"),
                                        Aspect.AsNumber("Deception (cha)"),
                                        Aspect.AsNumber("History (int)"),
                                        Aspect.AsNumber("Insight (wis)"),
                                        Aspect.AsNumber("Intimidation (cha)"),
                                        Aspect.AsNumber("Investigation (int)"),
                                        Aspect.AsNumber("Medicine (wis)"),
                                        Aspect.AsNumber("Nature (int)"),
                                        Aspect.AsNumber("Perception (wis)"),
                                        Aspect.AsNumber("Performance (cha)"),
                                        Aspect.AsNumber("Persuasion (cha)"),
                                        Aspect.AsNumber("Religion (int)"),
                                        Aspect.AsNumber("Slight of Hand (dex)"),
                                        Aspect.AsNumber("Stealth (Dex)"),
                                        Aspect.AsNumber("Survival (wis)"),
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