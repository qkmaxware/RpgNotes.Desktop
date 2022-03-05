using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RpgNotes.Desktop.Data {

public class Map {
    public string Name;
    public FileInfo SourceFile;
    public List<ImageLayer> BaseLayers {get; set;}
    public Grid OverlayGrid {get; set;}
    public FeatureCollection Features {get; set;}
}

public class Grid {
    public int OffsetX {get; set;}
    public int OffsetY {get; set;}
    public int CellWidth {get; set;}
    public int CellHeight {get; set;}
}

public class ImageLayer {
    public string ImagePath {get; set;}
}

public class FeatureCollection {
    public List<PointFeature> Points {get; set;}
    public List<LineFeature> Lines {get; set;}
    public List<PolygonFeature> Polygons {get; set;}

    public void Remove(Feature feature) {
        if (Points != null)
            Points.RemoveAll((point) => point == feature);
        if (Lines != null)
            Lines.RemoveAll((line) => line == feature);
        if (Polygons != null)
            Polygons.RemoveAll((poly) => poly == feature);
    }
}

public class Feature {
    public string Name {get; set;}
    public string LinkedArticle {get; set;}
}

public abstract class FeatureCreationWizard {
    protected List<System.Tuple<float, float>> points = new List<System.Tuple<float, float>>();
    public void AddPointToFeature(float x, float y) {
        this.points.Add(new System.Tuple<float, float>(x, y));
    }
    public abstract Feature CreateFeature();
    public abstract bool CanAddPoint();
    public abstract bool CanBeCompleted();
    public abstract bool AutoComplete {get;}

    public IEnumerable<System.Tuple<float, float>> Enumerate() {
        foreach (var point in points)
            yield return point;
    }
}
public abstract class FeatureCreationWizard<T> : FeatureCreationWizard where T:Feature {
    public abstract T Create();
    public override Feature CreateFeature() => Create();
}

public class PointFeature : Feature{
    public string MarkerUrl {get; set;}
    public float X {get; set;}
    public float Y {get; set;}
}

public class PointFeatureWizard : FeatureCreationWizard<PointFeature> {
    public override PointFeature Create() => new PointFeature {
        X = this.points[0].Item1,
        Y = this.points[0].Item2
    };
    public override bool CanAddPoint() => points.Count < 1;
    public override bool CanBeCompleted() => points.Count > 0;
    public override bool AutoComplete => true;
}

public class LineFeature : Feature{
    public string Stroke {get; set;}
    public float X1 {get; set;}
    public float Y1 {get; set;}

    public float X2 {get; set;}
    public float Y2 {get; set;}
}

public class LineFeatureWizard : FeatureCreationWizard<LineFeature> {
    public override LineFeature Create() => new LineFeature {
        Stroke = "red",
        X1 = this.points[0].Item1,
        Y1 = this.points[0].Item2,
        X2 = this.points[1].Item1,
        Y2 = this.points[1].Item2
    };
    public override bool CanAddPoint() => points.Count < 2;
    public override bool CanBeCompleted() => points.Count >= 2;
    public override bool AutoComplete => true;
}

public class PolygonVertex {
    public float X {get; set;}
    public float Y {get; set;}
    public override string ToString() => $"{X},{Y}";
}
public class PolygonFeature : Feature {
    public string Fill {get; set;}
    public float Opacity {get; set;}
    public List<PolygonVertex> Vertices {get; set;}
}

public class PolygonFeatureWizard : FeatureCreationWizard<PolygonFeature> {
    public override PolygonFeature Create() => new PolygonFeature {
        Fill="red", 
        Opacity=0.25f,
        Vertices = this.points.Select(point => new PolygonVertex{ X=point.Item1, Y=point.Item2 }).ToList()
    };
    public override bool CanAddPoint() => true;
    public override bool CanBeCompleted() => points.Count > 2; // at least 3 points
    public override bool AutoComplete => false;
}

}