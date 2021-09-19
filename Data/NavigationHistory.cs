using System;
using System.Collections;
using System.Collections.Generic;

namespace RpgNotes.Desktop.Data {

public class NavigationHistory : IEnumerable<Uri> {
    private static LinkedList<Uri> history = new LinkedList<Uri>();
    public static int MaxHistory = 24;

    public void Push(Uri location) {
        PushStatic(location);
    }

    public static void Clear() {
        history.Clear();
    }

    public static void PushStatic(Uri location) {
        if (!(string.IsNullOrEmpty(location.LocalPath) || location.LocalPath == "/") && !history.Contains(location)) {
            history.AddLast(location);
            while (history.Count > MaxHistory) {
                history.RemoveFirst();
            }
        }
    }

    public IEnumerator<Uri> GetEnumerator() {
        return history.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return history.GetEnumerator();
    }
}

}