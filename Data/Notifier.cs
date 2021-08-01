using System;

namespace RpgNotes.Desktop.Data {

public class Notifier {
    public event Action<string> OnNotificationReceived = delegate {};

    public void Alert(string message) {
        if (OnNotificationReceived != null) {
            OnNotificationReceived(message);
        }
    }
}

}