using System;
using System.IO;
using System.Web;

namespace RpgNotes.Desktop.Data {

public class Image {
    public string WebPath {get; set;}
    public string FilePath {get; set;}
    public string Base64Data {get; set;}
    
    public string GetUrl() => WebPath ?? Base64Data ?? FilePath; 

    public static Image InternetUrl(string url) {
        return new Image {
            WebPath = url,
        };
    }
    public static Image HardDriveLink(string path) {
        var safe_path = HttpUtility.UrlEncode(path);
        var url = $"api/files/get/{safe_path}";
        return new Image {
            FilePath = url
        };
    }
    
    public static Image EmbedBase64(string pathlike) {
        return new Image { 
            Base64Data = "data:image/" 
            + Path.GetExtension(pathlike).Replace(".","")
            + ";base64," 
            + Convert.ToBase64String(File.ReadAllBytes(pathlike))
        };
    }
}

}