using System;
using System.IO;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

public class FileManager {
    public static string SaveFileExtension = ".rpg";
    private object key = new object();
    private World current;
    public string SavePath {get; private set;}
    private bool dirty;

    private JsonSerializerOptions json = new JsonSerializerOptions {};

    public string AppDataPath {get; private set;}
    private string saveDataPath;
    public string BonusSystemDataPath {get; private set;}
    public string BonusTemplateDataPath {get; private set;}

    public FileManager() {
        var systemDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        AppDataPath = Path.Combine(systemDataPath, typeof(FileManager).Assembly.GetName().Name);
        BonusSystemDataPath = Path.Combine(AppDataPath, "Systems");
        BonusTemplateDataPath = Path.Combine(AppDataPath, "Templates");
        saveDataPath = Path.Combine(AppDataPath, "Save");

        Directory.CreateDirectory(BonusSystemDataPath);
        Directory.CreateDirectory(BonusTemplateDataPath);
        Directory.CreateDirectory(saveDataPath);
    }

    public World Data() {
        return current;
    }
    public void Clear() {
        this.current = null;
    }

    public bool HasLoadedSavefile() {
        return current != null;
    }
    public bool RequiresSaving() {
        return this.SavePath == null || dirty;
    }

    public void MarkDirty() {
        this.dirty = true;
    }

    public bool Load(World campaign) {
        lock(key) {
            this.current = campaign;
            this.SavePath = null;
            return true;
        }
    }

    public bool Load(string file) {
        lock(key) {
            if (File.Exists(file)) {
                // TODO parse file
                try {
                    this.current = JsonSerializer.Deserialize<World>(File.ReadAllText(file), this.json);
                    this.SavePath = file; 
                    return true;
                } catch {
                    return false;
                }
            } else {
                return false;
            }
        }
    }
    public void Save(string path) {
        if (!path.EndsWith(SaveFileExtension)) {
            path = path + SaveFileExtension;
        }
        lock(key) {
            if (this.current != null) {
                File.WriteAllText(path, JsonSerializer.Serialize(this.current));
                this.SavePath = path;
                this.dirty = false;
            }
        }
    }
}

}