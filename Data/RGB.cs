using System;

namespace RpgNotes.Desktop.Data {

public class RGB {
    private int r;
    private int g;
    private int b;

    public RGB() {}
    public RGB(int r, int g, int b) {
        this.Red = r;
        this.Green = g;
        this.Blue = b;
    }

    public int Red {
        get => r;
        set => r = Math.Max(Math.Min(255, value), 0);
    }
    public int Green {
        get => g;
        set => g = Math.Max(Math.Min(255, value), 0);
    }
    public int Blue {
        get => b;
        set => b = Math.Max(Math.Min(255, value), 0);
    }

    public string ToHexString() {
        return $"{Red:X2}{Green:X2}{Blue:X2}";
    }
}

}