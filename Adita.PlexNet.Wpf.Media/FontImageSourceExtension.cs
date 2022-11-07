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

using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;

namespace Adita.PlexNet.Wpf.Media
{
    /// <summary>
    /// Represents a font image source markup extension.
    /// </summary>
    [MarkupExtensionReturnType(typeof(DrawingImage))]
    public class FontImageSourceExtension : MarkupExtension
    {
        #region Public properties
        /// <summary>
        /// Gets or sets the unicode <see cref="string"/> of the font glyph.
        /// </summary>
        public string UnicodeString { get; set; } = String.Empty;
        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Media.FontFamily"/> for the image source.
        /// </summary>
        public FontFamily FontFamily { get; set; } = new FontFamily();
        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Media.Brush"/> for the image source.
        /// </summary>
        public Brush Brush { get; set; } = Brushes.Black;
        #endregion Public properties

        #region Public methods
        /// <summary>Returns the image source value that is provided as the value of the target property for this markup extension.</summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns>The the image source value to set on the property where the extension is applied.</returns>
        /// <exception cref="InvalidOperationException">Specified <see cref="FontFamily"/> is a composite font.</exception>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(UnicodeString, FontFamily, Brush);
        }
        #endregion Public methods

        #region Private methods
        private static ImageSource CreateImageSource(string unicodeString, FontFamily fontFamily, Brush brush)
        {
            Typeface typeface = new(fontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

            if (!typeface.TryGetGlyphTypeface(out GlyphTypeface glyphTypeface))
            {
                throw new InvalidOperationException($"Specified {nameof(fontFamily)} either is a composite font or invalid font.");
            }

            Glyphs glyphs = new()
            {
                FontUri = glyphTypeface.FontUri,
                UnicodeString = unicodeString,
                FontRenderingEmSize = 100,
                StyleSimulations = StyleSimulations.None,
                Fill = brush,
                OriginX = 0,
                OriginY = 0
            };

            return new DrawingImage(new GlyphRunDrawing(brush, glyphs.ToGlyphRun()));
        }
        #endregion Private methods
    }
}
