using System.Collections.Generic;

namespace RpgNotes.Desktop.Data {

public abstract class SessionReportTemplate {
    public abstract string ToMarkdown();
}

public class GenericSessionReport : SessionReportTemplate {
    public override string ToMarkdown() => string.Empty;
}

public class ItemAquiredReport : SessionReportTemplate {
    public override string ToMarkdown() {
return @"# Item Name
Description

**Where:** Where was it found

**Stats:** Item statistics
";
    }
}

public class ImportantEventReport : SessionReportTemplate {
    public override string ToMarkdown() {
return @"# Event Name
Description

**Cause:** Why did the event happen

**Effect:** What does this mean";
    }
}

public class SessionBattleReport : SessionReportTemplate {
    public override string ToMarkdown() {
return @"# Summary
Battle summary

# Teams
Player Team <span class=""ok-text"">Victory</span>

- (Initiative) Player 1
- (Initiative) Player 2
- (Initiative) Player 3

Enemy Team

- (Initiative) Enemy 1
- (Initiative) Enemy 2
- (Initiative) Enemy 3

# Loot
- Treasure 1
- Treasure 2
- Treasure 3
";
    }
}

}