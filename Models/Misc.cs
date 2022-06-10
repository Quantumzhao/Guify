namespace Guify.Models;

static class Misc {

	public static string FuckingToString<T>(this T nullable)
		=> nullable?.ToString() ?? string.Empty;
}