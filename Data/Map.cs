namespace RpgNotes.Desktop.Data {
public class Grid {
    public int OffsetX {get; set;}
    public int OffsetY {get; set;}
    public int CellWidth {get; set;}
    public int CellHeight {get; set;}
}

public class Map {
    public Image Background {get; set;}
    public Grid Grid {get; set;}
}

}