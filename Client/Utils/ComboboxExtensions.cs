﻿using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Client.Utils;

public static class ComboboxExtensions
{
    private const int DropButtonWidth = 28;

    public static void AutoSizeWidth(this ComboBox comboBox, string text)
    {
        var typeface = new Typeface(comboBox.FontFamily, comboBox.FontStyle, comboBox.FontWeight, comboBox.FontStretch);
        var formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, typeface,
            comboBox.FontSize, Brushes.Black, new NumberSubstitution(), VisualTreeHelper.GetDpi(comboBox).PixelsPerDip);

        comboBox.Width = formattedText.Width + DropButtonWidth;
    }
}