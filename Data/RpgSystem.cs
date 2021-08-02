using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

public interface IRpgSystemProvider {
    static RpgSystem System {get;}
}

public class RpgSystemFlags {
    public bool MultipleSpecies;
    public bool ClassBased;
}

public class RpgSystem {
    public string Name {get; set;}
    public override string ToString() => Name;
    public string Abbreviation {get; set;}
    public string Description {get; set;}
    public Publication Publication {get; set;}
    public RGB ThemeColour {get; set;}

    public Image IconImage {get; set;}
    public RpgSystemFlags Flags {get; set;}
    public CharacterSheet CharacterSheetTemplate {get; set;}

    public static List<RpgSystem> FindRpgSystems() {
        var type = typeof(IRpgSystemProvider);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p != null && type.IsAssignableFrom(p) && !p.IsInterface);
        var constructors = types
            .Select(t => t.GetProperty(nameof(IRpgSystemProvider.System), BindingFlags.Static | BindingFlags.Public))
            .Where(p => p != null);
        var systems = constructors
            .Select(con => (RpgSystem)con.GetValue(null, null))
            .Where(sys => sys != null);
        return systems.ToList();
    }
    public static List<RpgSystem> LoadSystemsFromJsonDirectory(string directory) {
        List<RpgSystem> loaded = new List<RpgSystem>();
        foreach (var file in Directory.GetFiles(directory, "*.json")) {
            try {
                var content = File.ReadAllText(file);
                var system = JsonSerializer.Deserialize<RpgSystem>(content);
                loaded.Add(system);
            } catch {}
        }
        return loaded;
    }
}

}