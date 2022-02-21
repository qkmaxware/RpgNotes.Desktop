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
}

}