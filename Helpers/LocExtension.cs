using System;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace REFTEST.Helpers
{
    [ContentProperty("Key")]
    public class LocExtension : IMarkupExtension<string>
    {
        public string Key { get; set; }

        public string ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrEmpty(Key))
                return string.Empty;

            return Localization.GetString(Key);
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }
    }
}
