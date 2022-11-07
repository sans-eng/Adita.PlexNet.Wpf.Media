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
using System.Windows.Markup;
using System.Windows.Media;

namespace Adita.PlexNet.Wpf.Media
{
    /// <summary>
    /// Represents a material icon markup extension.
    /// </summary>
    [MarkupExtensionReturnType(typeof(ImageSource))]
    public class MaterialIconImageSourceExtension : MarkupExtension
    {
        #region Private fields
        private readonly ResourceDictionary _unicodeStringDictionary = (ResourceDictionary)Application.LoadComponent(new Uri(MaterialIconResources.FontNamePath, UriKind.Relative));
        #endregion Private fields

        #region Publis properties
        /// <summary>
        /// Gets or sets the name of the icon to display.
        /// </summary>
        /// <remarks>The name is predefined on system PlexNet framework,
        /// see <a href="https://docs.plexdotnet.adita-engineering.com/wpf/media/material-icon"/> for documentation.</remarks>
        public string IconName { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the kind of the icon.
        /// </summary>
        public MaterialIconKind Kind { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="Brush"/> of the icon.
        /// </summary>
        public Brush Brush { get; set; } = Brushes.Black;
        #endregion Publis properties

        #region Public methods
        /// <summary>Returns the image source value that is provided as the value of the target property for this markup extension.</summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The the image source value to set on the property where the extension is applied.</returns>
        /// <exception cref="InvalidOperationException">Specified <see cref="IconName"/> is not valid.</exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(IconName, Kind, Brush);
        }
        #endregion Public methods

        #region Private methods
        private ImageSource CreateImageSource(string iconName, MaterialIconKind kind, Brush brush)
        {
            if (_unicodeStringDictionary[iconName] is not string unicodeString)
            {
                throw new InvalidOperationException($"Specified {nameof(iconName)} is not valid.");
            }

            Glyphs glyphs = new()
            {
                FontUri = GetFontUri(kind),
                UnicodeString = unicodeString,
                FontRenderingEmSize = 100,
                StyleSimulations = StyleSimulations.None,
                Fill = brush,
                OriginX = 0,
                OriginY = 0
            };

            return new DrawingImage(new GlyphRunDrawing(brush, glyphs.ToGlyphRun()));
        }

        private Uri GetFontUri(MaterialIconKind kind)
        {
            return kind switch
            {
                MaterialIconKind.Standard => new(MaterialIconResources.StandardFontPath),
                MaterialIconKind.Outline => new(MaterialIconResources.OutlineFontPath),
                MaterialIconKind.Round => new(MaterialIconResources.RoundFontPath),
                MaterialIconKind.Sharp => new(MaterialIconResources.SharpFontPath),
                MaterialIconKind.TwoTone => new(MaterialIconResources.TwoToneFontPath),
                _ => throw new ArgumentException($"Specified {nameof(Kind)} is not availabble."),
            };
        }
        #endregion Private methods
    }
}
