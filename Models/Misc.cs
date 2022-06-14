namespace Guify.Models;

static class Misc {
	
	public static string FuckingToString<T>(this T nullable)
		=> nullable?.ToString() ?? string.Empty;

	public static int? ToInt(this string? str)
		=> str == null ? null : int.Parse(str);

	public static float? ToFloat(this string? str)
		=> str == null ? null : float.Parse(str);

	public static bool? ToBool(this string? str)
		=> str == null ? null : bool.Parse(str);
	
	public static string Flatten(this string[] strs)
		=> string.Join(" [AND] ", strs);

	public static string[] Expand(this string str)
		=> str.Split(" [AND] ");
}