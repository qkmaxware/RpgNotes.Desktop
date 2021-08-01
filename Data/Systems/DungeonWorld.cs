namespace RpgNotes.Desktop.Data.Systems {

public class DungeonWorld : IRpgSystemProvider {
    public static RpgSystem System => new RpgSystem {
        Name = "Dungeon World",
        Abbreviation = "DW",
        Description = "Wave hands like a muppet! Play to find out what happens.",
        Publication = new Publication {
            Publisher = "Sage LaTorra and Adam Koebel",
            Url = "https://dungeon-world.com/",
            YearOfPublication = 2015,
        },
        ThemeColour = new RGB(106, 126, 114),
        IconImage = new Image { WebPath = "static/images/logos/dw.svg" },
        Flags = new RpgSystemFlags {
            MultipleSpecies = true,
            ClassBased = true,
        },
        CharacterSheetTemplate = new CharacterSheet {
            Rows = new CharacterBlockGroup[] {
                new CharacterBlockGroup {
                    Layout = Layout.FullWidth,
                    Blocks = new CharacterBlock[] {
                        new CharacterBlock {
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "",
                                    Layout = Layout.FourColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsNumber("Speed"),
                                        Aspect.AsNumber("Armor"),
                                        Aspect.AsNumber("Hit Points"),
                                        Aspect.AsText("Hit Dice"),
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
                                    Name="Weapons & Equipment",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsParagraph("")
                                    }
                                }
                            }
                        },
                        new CharacterBlock {
                            Sections = new AspectSection[] {
                                new AspectSection {
                                    Name = "Skills",
                                    Layout = Layout.FullWidth,
                                    Aspects = new Aspect[] {
                                        Aspect.AsBool("Athletics"),
                                        Aspect.AsBool("Awareness"),
                                        Aspect.AsBool("Deception"),
                                        Aspect.AsBool("Decipher"),
                                        Aspect.AsBool("Heal"),
                                        Aspect.AsBool("Leadership"),
                                        Aspect.AsBool("Lore"),
                                        Aspect.AsBool("Stealth"),
                                        Aspect.AsBool("Survival")
                                    }
                                },
                            }
                        }
                    }
                },
                new CharacterBlockGroup {
                    Layout = Layout.FullWidth,
                    Blocks = new CharacterBlock[] {
                        new CharacterBlock {
                            Sections = new AspectSection[]{
                                new AspectSection {
                                    Name = "Special Abilities",
                                    Layout = Layout.FourColumns,
                                    Aspects = new Aspect[] {
                                        Aspect.AsBool("Bless"), Aspect.AsBool("Cure"),
                                        Aspect.AsBool("Turn"), Aspect.AsBool("Vision"),
                                        Aspect.AsBool("Hardy"), Aspect.AsBool("Skirmish"),
                                        Aspect.AsBool("Slay"), Aspect.AsBool("Tough"),
                                        Aspect.AsBool("Backstab"), Aspect.AsBool("Lucky"),
                                        Aspect.AsBool("Reflexes"), Aspect.AsBool("Thinker"),
                                        Aspect.AsBool("Cantrips"), Aspect.AsBool("Command"),
                                        Aspect.AsBool("Ritual"), Aspect.AsBool("Summon"),
                                        Aspect.AsBool("Pet"), Aspect.AsBool("Scout"),
                                        Aspect.AsBool("Volley"), Aspect.AsBool("Wild"),
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    };

}

}