using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Helper
{
    public class CookieHelper
    {
        // Method to create or update a cookie
        public static void SetCookie(HttpContext context, string key, string value, int daysExpiry = 30)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var options = new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(daysExpiry),
                HttpOnly = true, // Consider security based on your needs
                Secure = context.Request.IsHttps // Ensure secure cookies on HTTPS
            };

            context.Response.Cookies.Append(key, value, options);
        }

        // Method to remove a cookie
        public static void RemoveCookie(HttpContext context, string key)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Response.Cookies.Delete(key);
        }

        // Method to check if a cookie exists
        public static bool CookieExists(HttpContext context, string key)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return context.Request.Cookies.ContainsKey(key);
        }

        // Method to get a cookie value
        public static string GetCookie(HttpContext context, string key)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            context.Request.Cookies.TryGetValue(key, out var value);
            return value;
        }
    }
}
