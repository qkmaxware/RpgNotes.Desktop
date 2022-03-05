using System.Net;

namespace RpgNotes {

public static class StringExtensions {

    public static string UrlEncode(this string str) => WebUtility.UrlEncode(str);
    public static string EncodeBase64(this string str) => System.Convert.ToBase64String(System.Text.UTF8Encoding.Unicode.GetBytes(str));
    public static string DecodeBase64(this string str) => System.Text.UTF8Encoding.Unicode.GetString(System.Convert.FromBase64String(str));
}

}