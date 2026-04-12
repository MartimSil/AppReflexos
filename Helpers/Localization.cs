using System.Globalization;
using System.Resources;

namespace REFTEST.Helpers
{
    public static class Localization
    {
        private static ResourceManager resourceManager = new("REFTEST.Resources.Languages.Strings", typeof(Localization).Assembly);

        public static string GetString(string key, params object[] args)
        {
            var culture = CultureInfo.CurrentUICulture; // use CurrentUICulture for resource lookup
            var value = resourceManager.GetString(key, culture) ?? key;
            return args.Length > 0 ? string.Format(value, args) : value;
        }

        public static void SetCulture(string name)
        {
            var culture = new CultureInfo(name);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }
    }
}
