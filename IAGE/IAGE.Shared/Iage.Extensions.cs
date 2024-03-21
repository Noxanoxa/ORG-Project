namespace IAGE.Shared;

public static class IAGExtensions
{
    public static string AsString(this object obj) => (string)obj;

    public static Guid AsGuid(this object obj) => Guid.Parse(obj.ToString());

    public static bool AsBool(this object obj) => (bool)obj;

    public static int AsInt(this object obj) => (int)obj;

    public static DateTime AsDateTime(this object obj) => (DateTime)obj;
}