using System;
using System.Collections.Generic;

namespace RpgNotes.Desktop.Data {

public class SessionReport {
    public DateTime PlayDate {get; set;}
    public string Content {get; set;}    
}

public class CampaignData {
    public List<SessionReport> SessionReports {get; set;}
}

}