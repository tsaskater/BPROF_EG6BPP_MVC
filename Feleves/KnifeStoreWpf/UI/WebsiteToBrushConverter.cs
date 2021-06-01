// <copyright file="WebsiteToBrushConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf.UI
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Media;

    /// <summary>
    /// Converter for website from link to brush.
    /// </summary>
    internal class WebsiteToBrushConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string website = (string)value;
            string[] splitted = website.Split('.');
            string region = splitted[splitted.Length - 1]; // this way www. is not essential for coloring
            switch (region)
            {
                default: return Brushes.LightGray;
                case "com": return Brushes.LightBlue;
                case "hu": return Brushes.LightGreen;
                case "de": return Brushes.LightGoldenrodYellow;
            }
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}