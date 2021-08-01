using System.Collections.Generic;

namespace RpgNotes.Desktop.Data.Custom {

public enum FieldType {
    String, MultilineString, Number, Quantity, Boolean, Date
}

public class MetadataField {
    public string Name {get; set;}
    public string Value {get; set;}
    public FieldType Type {get; set;}

    public static readonly char QuantityUnitSeparator = ',';

    public static MetadataField AsText(string name) => new MetadataField { Name = name, Value = null, Type = FieldType.String };
    public static MetadataField AsParagraph(string name) => new MetadataField { Name = name, Value = null, Type = FieldType.MultilineString };
    public static MetadataField AsNumber(string name) => new MetadataField { Name = name, Value = "0", Type = FieldType.Number };
    public static MetadataField AsBool(string name) => new MetadataField { Name = name, Value = "false", Type = FieldType.Boolean };
    public static MetadataField AsSpeed(string name) => new MetadataField { Name = name, Value = $"0{QuantityUnitSeparator}m/s", Type = FieldType.Quantity };
    public static MetadataField AsWeight(string name) => new MetadataField { Name = name, Value = $"0{QuantityUnitSeparator}kg", Type = FieldType.Quantity };

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
}

public class MetadataBlock {
    public string Name {get; set;}
    public List<MetadataField> Data {get; set;}
}

}