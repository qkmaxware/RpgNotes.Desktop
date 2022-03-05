using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RpgNotes {

public static class FsExtensions {
    private static string[] markdown_extensions = new string[]{
        ".markdown",
        ".mdown",
        ".mkdn",
        ".md",
        ".mkd",
        ".mdwn",
        ".mdtxt",
        ".mdtext",
        ".txt",
        ".text",
        ".Rmd",
    };

    public static bool StartsWith(this FileInfo file, string str) {
        using (var reader = new StreamReader(file.OpenRead())) {
            for (var i = 0; i < str.Length; i++) {
                if (reader.Peek() == str[i]) {
                    continue;
                } else {
                    return false;
                }
            }
        }
        return true;
    }

    public static bool IsMarkdown(this FileInfo file) {
        return Array.IndexOf(markdown_extensions, file.Extension) >= 0;
    }

    private static string[] image_extensions = new string[]{
        ".bmp",
        ".ico", ".cur",
        // ".tif", ".tiff" // Safari Only
        ".apng",
        ".avif",
        ".gif",
        ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp",
        ".png",
        ".svg",
        ".webp",
    };

    public static bool IsImage(this FileInfo file) {
        return Array.IndexOf(image_extensions, file.Extension) >= 0;
    }   

    public static bool IsJson(this FileInfo file) {
        return file.Extension == ".json";
    }

    public static IEnumerable<FileInfo> GetMarkdownArticles(this DirectoryInfo directory) {
        return directory.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(file => file.IsMarkdown());
    }

    public static IEnumerable<FileInfo> GetJsonAssets(this DirectoryInfo directory) {
        return directory.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(file => file.IsJson());
    }

    public static IEnumerable<FileInfo> GetImages(this DirectoryInfo directory) {
        return directory.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(file => file.IsImage());
    }


    public static void ShowInExplorer(this FileInfo file) {
        if (System.Environment.OSVersion.Platform == PlatformID.Win32NT) {
            System.Diagnostics.Process.Start("explorer.exe", "/select, " + file.FullName);
        } else {
            System.Diagnostics.Process.Start("mimeopen", Path.GetDirectoryName(file.FullName));
        }
    }

    public static void ShowInExplorer(this DirectoryInfo dir) {
        if (System.Environment.OSVersion.Platform == PlatformID.Win32NT) {
            System.Diagnostics.Process.Start("explorer.exe", dir.FullName);
        } else {
            System.Diagnostics.Process.Start("mimeopen", Path.GetDirectoryName(dir.FullName));
        }
    }

    public static string MimeType(this FileInfo file) {
        // Based on list here https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
        return file.Extension switch {
            ".aac"  => "audio/aac",
            ".abw"  => "application/x-abiword",
            ".arc"  => "application/x-freearc",
            ".avif" => "image/avif",
            ".avi"  => "video/x-msvideo",
            ".azw"  => "application/vnd.amazon.ebook",
            ".bin"  => "application/octet-stream",
            ".bmp"  => "image/bmp",
            ".bz"   => "image/x-bzip",
            ".bz2"  => "image/x-bzip2",
            ".cda"  => "image/x-cdf",
            ".csh"  => "image/x-csh",
            ".css"  => "text/css",
            ".csv"  => "text/csv",
            ".doc"  => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".eot"  => "application/vnd.ms-fontobject",
            ".epub" => "application/epub+zip",
            ".gz"   => "application/gzip",
            ".gif"  => "image/gif",
            ".htm"  => "text/html",
            ".html" => "text/html",
            ".ico"  => "image/vnd.microsoft.icon",
            ".ics"  => "text/calendar",
            ".jar"  => "application/java-archive",
            ".jpeg" => "image/jpeg",
            ".jpg"  => "image/jpeg",
            ".js"   => "text/javascript",
            ".json" => "application/json",
            ".jsonld"=> "application/ld+json",
            ".mid"  => "audio/midi",
            ".midi" => "audio/midi",
            ".mjs"  => "text/javascript",
            ".mp3"  => "audio/mpeg",
            ".mp4"  => "video/mp4",
            ".mpeg" => "video/mpeg",
            ".mpkg" => "application/vnd.apple.installer+xml",
            ".odp"  => "application/vnd.oasis.opendocument.presentation",
            ".ods"  => "application/vnd.oasis.opendocument.spreadsheet",
            ".odt"  => "application/vnd.oasis.opendocument.text",
            ".oga"  => "audio/ogg",
            ".ogv"  => "video/ogg",
            ".ogx"  => "application/ogg",
            ".opus" => "audio/opus",
            ".otf"  => "font/otf",
            ".png"  => "image/png",
            ".pdf"  => "application/pdf",
            ".php"  => "application/x-httpd-php",
            ".ppt"  => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            ".rar"  => "application/vnd.rar",
            ".rtf"  => "application/rtf",
            ".sh"   => "application/x-sh",
            ".svg"  => "image/svg+xml",
            ".swf"  => "application/x-shockwave-flash",
            ".tar"  => "application/x-tar",
            ".tif"  => "image/tiff",
            ".tiff" => "image/tiff",
            ".ts"   => "video/mp2t",
            ".ttf"  => "font/ttf",
            ".txt"  => "text/plain",
            ".vsd"  => "application/vnd.visio",
            ".wav"  => "audio/wav",
            ".weba" => "audio/webm",
            ".webm" => "video/webm",
            ".webp" => "image/webp",
            ".woff" => "font/woff",
            ".woff2"=> "font/woff2",
            ".xhtml"=> "application/xhtml+xml",
            ".xls"  => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".xml"  => "application/xml",
            ".xul"  => "application/vnd.mozilla.xul+xml",
            ".zip"  => "application/zip",
            ".3gp"  => "video/3gpp",
            ".3g2"  => "video/3gpp2",
            ".7z"   => "application/x-7z-compressed",
            
            _       => "application/octet-stream"
        };
    }
}

}