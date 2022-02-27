using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

/// <summary>
/// Publication notes
/// </summary>
public class Publication {
    public string Publisher {get; set;}
    public string Url {get; set;}
    public int YearOfPublication {get; set;}
}


/// <summary>
/// Serializable RGB colour
/// </summary>
public class RGB {
    private int r;
    private int g;
    private int b;

    public RGB() {}
    public RGB(int r, int g, int b) {
        this.Red = r;
        this.Green = g;
        this.Blue = b;
    }

    public int Red {
        get => r;
        set => r = Math.Max(Math.Min(255, value), 0);
    }
    public int Green {
        get => g;
        set => g = Math.Max(Math.Min(255, value), 0);
    }
    public int Blue {
        get => b;
        set => b = Math.Max(Math.Min(255, value), 0);
    }

    public string ToHexString() {
        return $"{Red:X2}{Green:X2}{Blue:X2}";
    }
}

/// <summary>
/// An object that provides defintions for RPG systems
/// </summary>
public interface IRpgSystemProvider {
    /// <summary>
    /// Fetch provided RPG systems
    /// </summary>
    /// <returns>enumerable of systems</returns>
    IEnumerable<RpgSystem> Systems();
}

/// <summary>
/// Role playing game details
/// </summary>
public class RpgSystem {
    public string Name {get; set;}
    public string Abbreviation {get; set;}
    public string Description {get; set;}
    public Publication Publication {get; set;}

    public RGB ThemeColour {get; set;}
    public string IconUrl {get; set;}

    public CharacterSheet CharacterSheetTemplate {get; set;}

    public override string ToString() => Name;

    public static List<RpgSystem> FindRpgSystems() {
        var type = typeof(IRpgSystemProvider);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => p != null && type.IsAssignableFrom(p) && !p.IsInterface);
        var providers = types.Select(type => (IRpgSystemProvider)Activator.CreateInstance(type));
        var systems = providers.SelectMany(provider => provider.Systems());
        return systems.ToList();
    }
}

}