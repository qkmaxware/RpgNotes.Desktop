using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace RpgNotes.Desktop.Data {

public class World {
    public string Name {get; set;}
    public DateTime CreatedAt {get; set;}
    public RpgSystem System {get; set;}
    public ArticleCollection Articles {get; set;}
}

public class ArticleCollection {
    private ConcurrentDictionary<string, Article> articles {get; set;}
    public IEnumerable<Article> Pages {
        get => articles?.Values;
        set {
            this.articles = new ConcurrentDictionary<string, Article>(value.ToDictionary((article) => article.Guid));
        }
    }
    public List<Connection> Connections { get; set; }
    private object connectionsKey = new object();

    public Article this[string guid] {
        get => this.articles.GetValueOrDefault(guid);
        set {
            this.articles[guid] = value;
            value.Guid = guid;
        }
    }

    public void Add(Article article) {
        if (articles == null) {
            articles = new ConcurrentDictionary<string, Article>();
        }

        var guid = Guid.NewGuid().ToString();
        article.Guid = guid;
        article.CreatedAt = DateTime.Now;
        articles.TryAdd(guid, article);
    }

    public void Remove(Article article) {
         if (articles != null) {
            Article old;
            if(this.articles.TryRemove(article.Guid, out old)) {
                if (this.Connections != null) {
                    this.Connections.RemoveAll((conn) => conn.Participates(article));
                }
            }
        }
    }

    public void Connect(Connection connection) {
        lock(connectionsKey) {
            if (this.Connections == null)
                this.Connections = new List<Connection>();
            this.Connections.Add(connection);
        }
    }

    public void Disconnect(Connection connection) {
        lock (connectionsKey) {
            if (this.Connections == null) {
                return;
            }
            this.Connections.Remove(connection);
        }
    }

    public IEnumerable<Article> EnumerateArticles() {
        if (articles == null) {
            return Enumerable.Empty<Article>();
        } else {
            return articles.Values;
        }
    }

    public IEnumerable<Article> EnumerateCampaigns() {
        if (articles == null) {
            return Enumerable.Empty<Article>();
        } else {
            return articles.Values.Where(a => a.Campaign != null);
        }
    }

    public IEnumerable<Connection> GetConnectionsForArticle(Article article) {
        if (this.Connections == null || article == null)
            return Enumerable.Empty<Connection>();
        else {
            return GetConnectionsForArticle(article.Guid);
        }
    }

    public IEnumerable<Connection> GetConnectionsForArticle(string guid) {
        if (this.Connections == null || guid == null)
            return Enumerable.Empty<Connection>();
        else {
            List<Connection> conns = null;
            lock (connectionsKey) {
                conns = this.Connections.ToList();
            }
            return conns.Where(conn => conn.Participates(guid));
        }
    }

    public IEnumerable<string> GetAllTags() {
        if (this.Pages == null)
            return Enumerable.Empty<string>();
        return this.Pages.SelectMany(art => art.Tags).Distinct();
    }
}

}