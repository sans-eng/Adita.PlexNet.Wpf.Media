//MIT License

//Copyright (c) 2022 Adita

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using Adita.PlexNet.Wpf.Media.Internals;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Adita.PlexNet.Wpf.Media
{
    /// <summary>
    /// Represents an icon.
    /// </summary>
    public sealed class MaterialIcon : Icon
    {
        #region Private fields
        private readonly ResourceDictionary _fontNames = (ResourceDictionary)Application.LoadComponent(new Uri(MaterialIconResources.FontNamePath, UriKind.Relative));
        #endregion Private fields

        #region Dependency properties
        /// <summary>
        /// Identifies <see cref="Kind"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register(nameof(Kind), typeof(MaterialIconKind), typeof(MaterialIcon), new FrameworkPropertyMetadata(MaterialIconKind.Standard, OnKindChanged));
        #endregion Dependency properties

        #region Public properties
        /// <summary>
        /// Gets or sets the kind of current <see cref="MaterialIcon"/>.
        /// </summary>
        public MaterialIconKind Kind
        {
            get => (MaterialIconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }
        #endregion Public properties

        #region Protected internal methods
        /// <summary>
        /// Updates the visual representation of current <see cref="Icon" />.
        /// </summary>
        protected internal override void Update()
        {
            string unicodeString = GetUnicodeString(IconName);

            if (string.IsNullOrWhiteSpace(unicodeString))
            {
                return;
            }

            UpdateIcon(unicodeString, GetFontUri(Kind));
        }
        #endregion Protected internal methods

        #region Private methods
        private Uri GetFontUri(MaterialIconKind iconKind)
        {
            return iconKind switch
            {
                MaterialIconKind.Standard => new(MaterialIconResources.StandardFontPath),
                MaterialIconKind.Outline => new(MaterialIconResources.OutlineFontPath),
                MaterialIconKind.Round => new(MaterialIconResources.RoundFontPath),
                MaterialIconKind.Sharp => new(MaterialIconResources.SharpFontPath),
                MaterialIconKind.TwoTone => new(MaterialIconResources.TwoToneFontPath),
                _ => throw new ArgumentException($"Specified {nameof(Kind)} is not availabble."),
            };
        }
        private string GetUnicodeString(string glyphName)
        {
            return (string)_fontNames[glyphName];
        }
        #endregion Private methods

        #region Dependency property changed event handlers
        private static void OnKindChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.Update();
        }
        #endregion Dependency property changed event handlers
    }
}
