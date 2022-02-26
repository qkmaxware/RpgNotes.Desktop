using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RpgNotes.Desktop.Data {

public class ExtensionHost {

    private DirectoryInfo[] extensionDirectories;

    public ExtensionHost (params DirectoryInfo[] extensionDirectories) {
        this.extensionDirectories = extensionDirectories;
    }

    internal List<IMarkdownPreprocessor> MarkdownPreprocessors = new List<IMarkdownPreprocessor>();
    public void InstallPreprocessor(IMarkdownPreprocessor preprocessor) {
        this.MarkdownPreprocessors.Add(preprocessor);
    }

    internal List<Template> ArticleTemplates = new List<Template>() {
        // Default templates
        Template.None,
        Template.PC,
        Template.NPC,
        Template.City,
        Template.Condition,
        Template.Conflict,
        Template.Item,
        Template.Language,
        Template.Planet,
        Template.Quest,
        Template.Region,
        Template.Technology,
        Template.Vehicle
    };
    public void InstallTemplate(Template template) {
        this.ArticleTemplates.Add(template);
    }

    internal Dictionary<FileInfo, IExtension> Loaded = null;

    internal bool TryLoadPlugins() {
        try {
            var plugins = new Dictionary<FileInfo, IExtension>();

            // Load DLLs from extension directories
            Dictionary<FileInfo, Assembly> loadedAssemblies = new Dictionary<FileInfo, Assembly>();
            foreach (var dir in extensionDirectories) {
                if (!dir.Exists)
                    continue;

                var files = dir.GetFiles().Where(file => file.Extension == ".dll");
                foreach (var file in files) {
                    loadedAssemblies.Add(file, Assembly.LoadFile(file.FullName));
                }
            }

            // Create instances of the plugins
            var interfaceType = typeof(IExtension);
            foreach (var entry in loadedAssemblies) {
                var file = entry.Key;
                var assembly = entry.Value;
                var types = assembly.GetTypes().Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass);
                foreach (var type in types) {
                    var plugin = ((IExtension)System.Activator.CreateInstance(type));
                    plugins.Add(file, plugin);
                    plugin.Install(this);
                }
            }

            // Plugins loaded
            this.Loaded = plugins;
            return true;
        } catch  {
            return false;
        }
    }
}

}