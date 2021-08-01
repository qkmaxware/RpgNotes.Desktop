using System;
using System.Collections.Generic;

namespace RpgNotes.Desktop.Data {

public enum Participant {
    None, From, To
}
public static class ParticipantMethods {
    public static Participant OtherParticipant(this Participant p) {
        return p switch {
            Participant.From => Participant.To,
            Participant.To => Participant.From,
            _ => Participant.None
        };
    }
}

public class Position {
    public string Marker {get; set;}
    public int X {get; set;}
    public int Y {get; set;}
}

public class Connection {
    public string GuidFrom {get; set;}
    public string GuidTo {get; set;}

    public string FromToRelation {get; set;}
    public string ToFromRelation {get; set;}

    /*
    Special Data
    */
    // ==============================================
    public bool HasLocationSupport() => MapOwner != Participant.None;
    public Participant MapOwner {get; set;}
    public Position MapPosition {get; set;}

    public bool HasAncestrySupport() => FamilialParent != Participant.None;
    public Participant FamilialParent {get; set;}

    // ==============================================
    public string GetParticipantGuid(Participant participant) {
        return participant switch {
            Participant.From => GuidFrom,
            Participant.To => GuidTo,
            _ => null
        };
    }
    public string ParticipatesWith(Article article) {
        return this.ParticipatesWith(article.Guid);
    }
    
    public string ParticipatesWith(string guid) {
        if (this.GuidFrom == guid)
            return this.GuidTo;
        if (this.GuidTo == guid)
            return this.GuidFrom;
        return null;
    }

    public string FormatRelationship (Article article) {
        return this.FormatRelationship(article.Guid);
    }

    public string FormatRelationship (string guid) {
        if (this.GuidFrom == guid)
            return this.FromToRelation;
        if (this.GuidTo == guid)
            return this.ToFromRelation;
        return null;
    }

    public bool Participates(Article article) {
        return this.Participates(article.Guid);
    }

    public bool Participates(string guid) {
        return this.GuidFrom == guid || this.GuidTo == guid;
    }

}

}