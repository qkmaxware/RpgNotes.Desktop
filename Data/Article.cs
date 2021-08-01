using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RpgNotes.Desktop.Data.Custom;

namespace RpgNotes.Desktop.Data {

public enum Visibility {
    Public, Private
}
public class Article {
    public string Guid {get; set;} // Set outside of the article
    public DateTime CreatedAt {get; set;} // Set outside of the article
    public Visibility Visibility {get; set;}
    public string Name {get; set;}
    public string MarkdownContent {get; set;}
    public string Notes {get; set;}

    // ----------------------------------
    // Custom Metadata
    // ----------------------------------
    public List<MetadataField> CustomDetails {get; set;}
    public MetadataField this[string key] => CustomDetails.Where(det => det.Name == key).FirstOrDefault();
    public List<MetadataBlock> CustomDataBlocks {get; set;}
    // ----------------------------------


    // ----------------------------------
    // Custom Metadata
    // ----------------------------------
    public List<ContentPage> CustomPages {get; set;} 
    // ----------------------------------

    // ----------------------------------
    // Type Specific Data
    // ----------------------------------
    
    // Map Data
    public Map Map {get; set;}
    public bool IsMapCapable() => Map != null;
    public bool HasMap() => IsMapCapable() && Map.Background != null;
    
    // Character data
    public CharacterSheet CharacterSheet;

    // Campaign data
    public CampaignData Campaign;
    public bool IsCampaign() => Campaign != null;

    // ----------------------------------

    public List<Image> Images {get; set;}
    public Image ProfileImage() => Images?.FirstOrDefault();

    public List<string> Tags {get; set;}

    public static List<IArticleTemplate> FindArticleTemplates() {
        var type = typeof(IArticleTemplate);
        // Get all types that implement IArticleTemplate AND have a parameter-less constructor
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p != null && type.IsAssignableFrom(p) && !p.IsInterface && p.GetConstructor(Type.EmptyTypes) != null);
        // Create instances of all the found types
        var templates = types.Select(t => (IArticleTemplate)Activator.CreateInstance(t));
        return templates.ToList();
    }
}

}