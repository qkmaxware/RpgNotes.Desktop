using System.Linq;

namespace RpgNotes.Desktop.Data {

public enum AspectType {
    String, MultilineString, Number, Boolean
}

public class Aspect : IDeepCloneable<Aspect> {
    public string Name {get; set;}
    public string Value {get; set;}
    public AspectType Type {get; set;}

    public static Aspect AsText(string name) => new Aspect { Name = name, Value = null, Type = AspectType.String };
    public static Aspect AsParagraph(string name) => new Aspect { Name = name, Value = null, Type = AspectType.MultilineString };
    public static Aspect AsNumber(string name) => new Aspect { Name = name, Value = "0", Type = AspectType.Number };
    public static Aspect AsBool(string name) => new Aspect { Name = name, Value = "false", Type = AspectType.Boolean };

    public string AsString() => Value;
    public float AsNumber() {
        float f;
        if (float.TryParse(this.Value, out f)) {
            return f;
        } else {
            return default(float);
        }
    }
    public bool AsBool() {
        bool f;
        if (bool.TryParse(this.Value, out f)) {
            return f;
        } else {
            return default(bool);
        }
    }

    public Aspect Clone() {
        return new Aspect {
            Name = this.Name,
            Value = this.Value,
            Type = this.Type,
        };
    }
}

public class AspectSection : IDeepCloneable<AspectSection> {
    public string Name {get; set;}
    public Layout Layout {get; set;}
    public Aspect[] Aspects {get; set;}

    public AspectSection Clone() {
        return new AspectSection {
            Name = this.Name,
            Layout = this.Layout,
            Aspects = this.Aspects?.Select(a => a.Clone())?.ToArray()
        };
    }
}

public class CharacterBlock : IDeepCloneable<CharacterBlock> {
    public string Name {get; set;}
    public AspectSection[] Sections {get; set;}

    public CharacterBlock Clone() {
        return new CharacterBlock {
            Name = this.Name,
            Sections = this.Sections?.Select(a => a.Clone())?.ToArray()
        };
    }
}

public class CharacterBlockGroup : IDeepCloneable<CharacterBlockGroup> {
    public Layout Layout {get; set;}
    public CharacterBlock[] Blocks {get; set;}

    public CharacterBlockGroup Clone() {
        return new CharacterBlockGroup {
            Layout = this.Layout,
            Blocks = this.Blocks?.Select(a => a.Clone())?.ToArray()
        };
    }
}

public class CharacterSheet : IDeepCloneable<CharacterSheet> {
    public CharacterBlockGroup[] Rows {get; set;}

    public CharacterSheet Clone() {
        return new CharacterSheet {
            Rows = this.Rows?.Select(a => a.Clone())?.ToArray()
        };
    }
}

}