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

    internal bool TryLoadPlugins() {
        try {
            var plugins = new List<IExtension>();

            // Load DLLs from extension directories
            List<Assembly> loadedAssemblies = new List<Assembly>();
            foreach (var dir in extensionDirectories) {
                if (!dir.Exists)
                    continue;

                var files = dir.GetFiles().Where(file => file.Extension == ".dll");
                foreach (var file in files) {
                    loadedAssemblies.Add(Assembly.LoadFile(file.FullName));
                }
            }

            // Create instances of the plugins
            var interfaceType = typeof(IExtension);
            var types = loadedAssemblies
                .SelectMany(a => a.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass);
            foreach (var type in types) {
                var plugin = ((IExtension)System.Activator.CreateInstance(type));
                plugins.Add(plugin);
                plugin.Install(this);
            }

            // Plugins loaded
            return true;
        } catch  {
            return false;
        }
    }
}

}