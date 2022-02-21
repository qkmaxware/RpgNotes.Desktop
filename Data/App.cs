using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

public class AppData {
    
    public string AppDataDirectory;
    public string AppExtensionDirectory;
    public ExtensionHost Extensions; 
    public HddList<string> RecentlyOpened;
    public HddValue<AppConfig> Config;

    public AppData () {
        this.AppDataDirectory = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
            this.GetType().Assembly.GetName().Name
        );
        this.AppExtensionDirectory = Path.Combine(
            this.AppDataDirectory,
            "Extensions"
        );

        Directory.CreateDirectory(this.AppDataDirectory);
        Directory.CreateDirectory(this.AppExtensionDirectory);

        this.RecentlyOpened = new HddList<string>(Path.Combine(this.AppDataDirectory, "recent.json"));
        this.Config = new HddValue<AppConfig>(Path.Combine(this.AppDataDirectory, "config.json"));
        this.Extensions = new ExtensionHost(new DirectoryInfo(this.AppExtensionDirectory));
        if(!this.Config.Value.Extensions.SafeMode) {
            this.Extensions.TryLoadPlugins(); // If not in safe mode, load plugins
        }
    }

}

public struct AppExtensionConfig {
    public bool SafeMode {get; set;}
}

public struct AppConfig {
    public AppExtensionConfig Extensions {get; set;}
}

}