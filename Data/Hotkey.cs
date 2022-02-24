using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Web;

namespace RpgNotes.Desktop.Data {

public class Hotkey {
    public bool CtrlKey {get; set;}
    public bool ShiftKey {get; set;}
    public bool AltKey {get; set;}
    public bool MetaKey {get; set;}
    public string[] Keys {get; set;}
    [JsonIgnore]
    public string Key {
        get => Keys != null && Keys.Length > 0 ? Keys[0] : null;
        set {
            Keys = new string[] { value };
        }
    }

    public override string ToString() => 
        (ShiftKey ? "shift + " : string.Empty)
        + (CtrlKey ? "ctrl + " : string.Empty)
        +(AltKey ? "alt + " : string.Empty)
        + (MetaKey ? "meta + " : string.Empty)
        + string.Join(" or ", Keys);

    public Hotkey() {}

    public void Rebind(KeyboardEventArgs args) {
        this.CtrlKey    = args.CtrlKey;
        this.AltKey     = args.AltKey;
        this.ShiftKey   = args.ShiftKey;
        this.MetaKey    = args.MetaKey;
        this.Keys       = new string[]{ args.Key };
    }

    private bool isCtrlModifier(KeyboardEventArgs args) => this.CtrlKey ? args.CtrlKey : !args.CtrlKey;
    private bool isShiftModifier(KeyboardEventArgs args) => this.ShiftKey ? args.ShiftKey: !args.ShiftKey;
    private bool isAltModifier(KeyboardEventArgs args) => this.AltKey ? args.AltKey: !args.AltKey;
    private bool isMetaModifier(KeyboardEventArgs args) => this.MetaKey ? args.MetaKey: !args.MetaKey;
    public bool IsTriggered(KeyboardEventArgs args) {
        return isCtrlModifier(args) 
        && isShiftModifier(args) 
        && isAltModifier(args) 
        && isMetaModifier(args) 
        && (this.Keys != null && this.Keys.Contains(args.Key));   
    }
}

}