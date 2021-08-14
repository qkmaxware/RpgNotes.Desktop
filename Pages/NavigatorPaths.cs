using Microsoft.AspNetCore.Components;
using RpgNotes.Desktop.Data;

namespace RpgNotes.Desktop.Pages {

public static class NavigatorPaths {
    
    public static void Index(this NavigationManager nav) {
        nav.NavigateTo(string.Empty);
    }
    public static void Home(this NavigationManager nav) {
        nav.NavigateTo("home");
    }

    public static void Create(this NavigationManager nav) {
        nav.NavigateTo("create");
    }
    public static void Browse(this NavigationManager nav) {
        nav.NavigateTo("browse");
    }

    public static void Map(this NavigationManager nav, Article article, int? x=null, int? y=null) {
        Map(nav, article.Guid, x, y);
    }
    public static void Map(this NavigationManager nav, string guid, int? x=null, int? y=null) {
        if (x.HasValue && y.HasValue) {
            nav.NavigateTo($"article/{guid}/map/{x.Value}/{y.Value}");
        } else {
            nav.NavigateTo($"article/{guid}/map");
        }
    }
    public static void RelocateOnMap(this NavigationManager nav, Article article, Connection conn) {
        RelocateOnMap(nav, article.Guid, conn);
    }
    public static void RelocateOnMap(this NavigationManager nav, string guid, Connection conn) {
        var id = conn.GetParticipantGuid(conn.MapOwner.OtherParticipant());
        if (string.IsNullOrEmpty(id)) {
            Map(nav, guid);
        } else {
            nav.NavigateTo($"article/{guid}/map/relocate/{id}");
        }
    }
    public static void Settings(this NavigationManager nav, Article article) {
        Settings(nav, article.Guid);
    }
    public static void Settings(this NavigationManager nav, string guid) {
        nav.NavigateTo($"article/{guid}/settings");
    }
    public static void Article(this NavigationManager nav, Article article) {
        Article(nav, article.Guid);
    }
    public static void Article(this NavigationManager nav, string guid) {
        nav.NavigateTo($"article/{guid}", forceLoad: true);
    }
}

}