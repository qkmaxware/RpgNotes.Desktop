using System.IO;
using YamlDotNet.Serialization.NamingConventions;

namespace RpgNotes.Desktop.Data {

/// <summary>
/// Base class for character sheets
/// </summary>
public class CharacterSheet {
    private static YamlDotNet.Serialization.ISerializer yaml 
        = new YamlDotNet.Serialization.SerializerBuilder()
        .WithNamingConvention(HyphenatedNamingConvention.Instance)
        .Build();

    /// <summary>
    /// Convert a character sheet to a yaml template
    /// </summary>
    /// <returns>yaml</returns>
    public string Yaml() {
        using (var writer = new StringWriter()) {
            yaml.Serialize(writer, this);

            return writer.ToString();
        }
    }
}

}