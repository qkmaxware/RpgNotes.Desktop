using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

/// <summary>
/// Interface for a data structure that is backed by a file
/// </summary>
public interface IFileBackedData {
    /// <summary>
    /// Path to the backing file
    /// </summary>
    /// <value>file path</value>
    public string BackingFilePath {get;}
    /// <summary>
    /// Reload the data structure from the source file
    /// </summary>
    public void Reload();
    /// <summary>
    /// Update the source file with values from the data structure
    /// </summary>
    public void Save();
}

/// <summary>
/// Value that is read from a file
/// </summary>
/// <typeparam name="T">Typeof value</typeparam>
public class HddValue<T> : IFileBackedData {
    public string BackingFilePath {get; private set;}
    private T cached;
    public T Value {
        get => cached;
        set {
            this.cached = value;
            Save();
        }
    }

    public HddValue(string pathToFile) {
        this.BackingFilePath = pathToFile;
        Reload();
    }

    public void Reload() {
        if (!System.IO.File.Exists(this.BackingFilePath)) {
            this.Value = default(T);
        }
        try {
            this.Value = JsonSerializer.Deserialize<T>(File.ReadAllText(this.BackingFilePath));
        } catch {
            this.Value = default(T);
        }
    }

    public virtual void Save() {
        if (this.cached != null) {
            using (var writer = new StreamWriter(this.BackingFilePath)) {
                writer.Write(JsonSerializer.Serialize(this.cached));
            }
        }
    }
}

}