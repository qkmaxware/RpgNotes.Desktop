using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace RpgNotes.Desktop.Data {

/// <summary>
/// File wrapper for articles that may contain front matter
/// </summary>
public class Article {
    /// <summary>
    /// Connection from one article to another
    /// </summary>
    public class Connection {
        public string ArticlePath;
        public string Label;
        public string Relationship;
    }

    /// <summary>
    /// Reference to the underlying file
    /// </summary>
    /// <value>file</value>
    public FileInfo File {get; private set;}

    /// <summary>
    /// Name of the article
    /// </summary>
    /// <returns>article name</returns>
    public string Name => File != null ? System.IO.Path.GetFileNameWithoutExtension(File.Name) : null;

    public Article(FileInfo file) {
        this.File = file;
    }

    private static YamlDotNet.Serialization.Deserializer deserializer = new YamlDotNet.Serialization.Deserializer();

    public static readonly Regex FrontMatterPattern = new Regex(@"\A---(.*?)---", RegexOptions.Multiline | RegexOptions.Singleline);    
    private static readonly String FrontMatterSeparator = "---";

    private void readFrontMatter(StreamReader reader, StringBuilder sb) {
        const int totalDashes = 3;
        int dashesRemaining = totalDashes;

        // Skip the front matter separator
        for (var i = 0; i < totalDashes; i++) {
            reader.Read();
        }

        // Read until the end of the front matter (second separator) if it exists
        while (reader.Peek() != -1) {
            char next = (char)reader.Read();
            if (next == '-') {
                dashesRemaining--;
                if (dashesRemaining <= 0) {
                    return;                // Reached end of the front matter
                }
            } else {
                // There were dashes, but not totalDashes in a row, append the skipped dashes and continue
                sb?.Append(new string('-', totalDashes - dashesRemaining));
                dashesRemaining = totalDashes;
                sb?.Append(next);
            }
        }
    }

    private string frontMatterString() {
        if (File.StartsWith(FrontMatterSeparator)) {
            StringBuilder sb = new StringBuilder();
            using (var reader = new StreamReader(File.OpenRead())) {
                readFrontMatter(reader, sb);
                return sb.ToString();
            }
        } else {
            return null;                                // The file didn't start with any front matter indicators
        }
    }

    public Dictionary<string, object> FrontMatter() {
        var fm = frontMatterString();
        if (string.IsNullOrEmpty(fm)) {
            return new Dictionary<string, object>();
        } else {
            return deserializer.Deserialize<Dictionary<string, object>>(fm);
        }
    }

    public string Contents() {
        if (File.StartsWith(FrontMatterSeparator)) {
            using (var reader = new StreamReader(File.OpenRead())) {
                readFrontMatter(reader, null); // Skip the front matter
                return reader.ReadToEnd();
            }
        } else {
            return System.IO.File.ReadAllText(File.FullName);
        }
    }

    public string AutoLinkedContents(DirectoryInfo root) {
        var contents = Contents();
        return InternalLinkPattern.Replace(contents, (match) => autoLink(root, match));
    }

    public IEnumerable<Connection> OutboundConnections() {
        foreach (Match match in InternalLinkPattern.Matches(Contents())) {
            var path_to_link = match.Groups["link"].Value;
            var relationship = match.Groups["relationship"].Success ? match.Groups["relationship"].Value : null;
            var name = match.Groups["name"].Success ? match.Groups["name"].Value : Path.GetFileNameWithoutExtension(match.Groups["link"].Value);

            yield return new Connection {
                Label = name,
                ArticlePath = path_to_link,
                Relationship = relationship,
            };
        }
    }

    private static readonly Regex InternalLinkPattern = new Regex(@"\[\[(?<link>.*?)(?:\|(?<name>.*?))?\]\](?:\((?<relationship>.*?)\))?");

    private string autoLink(DirectoryInfo root, Match match) {
        // Find out where the file is (relative to project dir)
        var path_to_link = Path.Combine(root.FullName, match.Groups["link"].Value);
        var relationship = match.Groups["relationship"].Success ? match.Groups["relationship"].Value : null;
        var name = match.Groups["name"].Success ? match.Groups["name"].Value : Path.GetFileNameWithoutExtension(match.Groups["link"].Value);

        var @ref = path_to_link.EncodeBase64();
        var title = string.IsNullOrEmpty(relationship) ? string.Empty : " title=\"" + relationship.Replace("\"", "&quot;") + "\"";
        // Compute the FileRef for the file
        return $"<a href=\"home/{root.FullName.EncodeBase64()}/editors/render/{@ref}\"{title}>{name}</a>";
    }
}

}