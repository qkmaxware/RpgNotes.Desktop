using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RpgNotes.Desktop.Data {

public abstract class Month {
    public string FullName {get; set;}
    public string AbbreviatedName {get; set;}
    public int DaysPerWeek {get; set;}
    public abstract IEnumerable<string> DatesInMonth(int year);
}

public class RealMonth : Month {
    private static string[] names = new string[]{"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    public int MonthIndex;
    public RealMonth(int month) {
        if (month < 1)
            month = 1;
        if (month > names.Length)
            month = names.Length;

        this.MonthIndex = month;
        this.FullName = names[month - 1];
        this.AbbreviatedName = names[month - 1].Substring(3);
        this.DaysPerWeek = 7;
    }

    public static string GetMonthName(int month) {
        if (month < 1)
            month = 1;
        if (month > names.Length)
            month = names.Length;

        return names[month - 1];
    }

    public override IEnumerable<string> DatesInMonth(int year) {
        var dayCount = DateTime.DaysInMonth(year, this.MonthIndex);
        var days = new string[dayCount];
        for (var i = 0; i < dayCount; i++) {
            days[i] = string.Empty; // Days don't have specific names
        }
        return days;
    }
}

public class CustomMonth : Month {
    public List<string> Dates {get; set;}
    public override IEnumerable<string> DatesInMonth(int year) => Dates;
}

public class CalendarDate: IComparable<CalendarDate> {
    public int Year;
    public int Month;
    public int DayInMonth;

    public int CompareTo(CalendarDate other) {
        if (Year > other.Year)
            return 1;
        else if (Year < other.Year)
            return -1;
        // Years are equal here
        if (Month > other.Month)
            return 1;
        else if (Month < other.Month)
            return -1;
        // Months are equal here
        if (DayInMonth > other.DayInMonth)
            return 1;
        else if (DayInMonth < other.DayInMonth)
            return -1;
        // Days are equal here
        return 0;
    }
}

public abstract class Calendar {
    [JsonIgnore]
    public string Name {get; set;}
    [JsonIgnore]
    public IEnumerable<Month> Months {get; set;}

    public Calendar (string name) {
        this.Name = name;
    }
    public Calendar (string name, List<Month> months) {
        this.Name = name;
        this.Months = months;
    }

    public abstract bool TryParseDate(string date, out CalendarDate outDate);
}

public class CustomCalendar : Calendar {
    public string DateFormat {get; set;}
    public List<CustomMonth> MonthDefinitions {get; set;}
    public CustomCalendar() : base(null) {}
    public CustomCalendar(string name) : base(name) {
        this.MonthDefinitions = new List<CustomMonth>();
        this.Months = this.MonthDefinitions.Cast<Month>();
    }
    public CustomCalendar(string name, string pathToFile) : base(name) {
        var json = System.IO.File.ReadAllText(pathToFile);
        var months = JsonSerializer.Deserialize<CustomCalendar>(json);
        this.MonthDefinitions = months.MonthDefinitions;
        this.Name = name;
    }
    public CustomCalendar(FileInfo file) : this(System.IO.Path.GetFileNameWithoutExtension(file.Name), file.FullName) {}

    public override bool TryParseDate(string date, out CalendarDate outDate) {
        if (string.IsNullOrEmpty(this.DateFormat)) {
            outDate = null;
            return false;
        } else {
            // Fetch year
            // Fetch month 
            // Fetch day
            outDate = null;
            return false; //TODO
        }
    }
}

public class GregorianCalendar : Calendar {
    public static readonly GregorianCalendar Instance = new GregorianCalendar();
    private GregorianCalendar() : base("Gregorian", Enumerable.Range(1, 12).Select(monthId => new RealMonth(monthId)).Cast<Month>().ToList()) {}

    public override bool TryParseDate(string date, out CalendarDate outDate) {
        DateTime t;
        if (DateTime.TryParse(date, out t)) {
            outDate = new CalendarDate {
                Year = t.Year,
                Month = t.Month - 1,
                DayInMonth = t.Day - 1
            };
            return true;
        } else {
            outDate = null;
            return false;
        }
    }
}

public class Schedule {
    public string Name;
    public FileInfo SourceFile;
    public string Calendar {get; set;}
    public string ArticleDateFieldName {get; set;}
    public List<string> TagFilters {get; set;}
}

}