using System.IO;
using Microsoft.AspNetCore.Components;

namespace RpgNotes.Desktop.Pages {

public abstract class EditorBase : ComponentBase {
    [Parameter] public string HomeRef {get; set;}
    [Parameter] public string FileRef {get; set;}
    protected DirectoryInfo HomeDirectory {get; private set;}
    protected FileInfo File {get; private set;}
    public bool Exists => File != null && File.Exists;
    protected override void OnParametersSet () {
        base.OnParametersSet();
        this.File           = new FileInfo(FileRef.DecodeBase64());
        this.HomeDirectory  = new DirectoryInfo(HomeRef.DecodeBase64());
    }
}

}