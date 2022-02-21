using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

/// <summary>
/// A list that is backed up by a file on the hard drive
/// </summary>
/// <typeparam name="T">stored type of object</typeparam>
public class HddList<T> : IList<T>, IFileBackedData {

    private List<T> cached;

    public T this[int index] { 
        get => cached[index]; 
        set {
            cached[index] = value;
            Save();
        }
    }

    public int Count => cached?.Count ?? 0;

    public bool IsReadOnly => false;

    public string BackingFilePath {get; private set;}

    public HddList(string pathToFile) {
        this.BackingFilePath = pathToFile;
        Reload();
    }

    public virtual void Save() {
        if (this.cached != null) {
            using (var writer = new StreamWriter(this.BackingFilePath)) {
                writer.Write(JsonSerializer.Serialize(this.cached));
            }
        }
    }

    public virtual void Reload() {
        if (!System.IO.File.Exists(this.BackingFilePath)) {
            this.cached = new List<T>();
        }
        try {
            this.cached = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(this.BackingFilePath));
        } catch {
            this.cached = new List<T>();
        }
    }

    public void Add(T item) {
        this.cached = this.cached ?? new List<T>();
        this.cached.Add(item);
        Save();
    }

    public void Clear() {
        this.cached = this.cached ?? new List<T>();
        this.cached.Clear();
        Save();
    }

    public bool Contains(T item) {
        return this.cached?.Contains(item) ?? false;
    }

    public void CopyTo(T[] array, int arrayIndex) {
        throw new System.NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator() {
        if (this.cached == null) {
            return Enumerable.Empty<T>().GetEnumerator();
        } else {
            return this.cached.GetEnumerator();
        }
    }

    public int IndexOf(T item) {
        return this.cached?.IndexOf(item) ?? -1;
    }

    public void Insert(int index, T item) {
        this.cached = this.cached ?? new List<T>();
        this.cached.Insert(index, item);
        Save();
    }

    public bool Remove(T item) {
        this.cached = this.cached ?? new List<T>();
        bool removed = this.cached.Remove(item);
        Save();
        return removed;
    }

    public void RemoveAt(int index) {
        this.cached = this.cached ?? new List<T>();
        this.cached.RemoveAt(index);
        Save();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return this.GetEnumerator();
    }
}

}