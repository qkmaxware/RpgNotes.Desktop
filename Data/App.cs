using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace RpgNotes.Desktop.Data {

public class AppData {
    
    public string AppDataDirectory {get; private set;}
    public string AppExtensionDirectory {get; private set;}
    public string AppThemeDirectory {get; private set;}
    public ExtensionHost Extensions {get; private set;}
    public HddList<string> RecentlyOpened {get; private set;}
    public HddValue<AppConfig> Config {get; private set;}

    public AppData () {
        this.AppDataDirectory = Path.Combine(
            System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData),
            this.GetType().Assembly.GetName().Name
        );
        this.AppExtensionDirectory = Path.Combine(
            this.AppDataDirectory,
            "Extensions"
        );
        this.AppThemeDirectory = Path.Combine(
            this.AppDataDirectory,
            "Themes"
        );

        Directory.CreateDirectory(this.AppDataDirectory);
        Directory.CreateDirectory(this.AppExtensionDirectory);
        Directory.CreateDirectory(this.AppThemeDirectory);

        this.RecentlyOpened = new HddList<string>(Path.Combine(this.AppDataDirectory, "recent.json"));
        this.Config = new HddValue<AppConfig>(Path.Combine(this.AppDataDirectory, "config.json"));
        this.Extensions = new ExtensionHost(new DirectoryInfo(this.AppExtensionDirectory));
        if(!this.Config.Value.Extensions.SafeMode) {
            this.Extensions.TryLoadPlugins(); // If not in safe mode, load plugins
        }
    }

    public List<FileInfo> FetchThemes() {
        return System.IO.Directory.GetFiles(this.AppThemeDirectory, "*.css", SearchOption.TopDirectoryOnly).Select(path => new FileInfo(path)).ToList();
    }

}

public struct AppAppearanceConfig {
    public string ThemeCssPath {get; set;}
    public string CustomCss {get; set;}
}

public struct AppExtensionConfig {
    public bool SafeMode {get; set;}
}

public struct AppConfig {
    public AppAppearanceConfig Appearance {get; set;}
    public AppExtensionConfig Extensions {get; set;}
}

}