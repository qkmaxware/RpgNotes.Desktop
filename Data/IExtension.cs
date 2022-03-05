namespace RpgNotes.Desktop.Data {

/// <summary>
/// Interface plugins need to implement
/// </summary>
public interface IExtension {
    string Name { get; }
    string Version { get; }
    string Description { get; }

    void Install(ExtensionHost host);
}

}