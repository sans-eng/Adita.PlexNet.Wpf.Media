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
    [MarkupExtensionReturnType(typeof(DrawingImage))]
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
        public string IconName { get; set; } = String.Empty;
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
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CreateImageSource(IconName, Kind, 100, Brush);
        }
        #endregion Public methods

        #region Private methods
        private ImageSource CreateImageSource(string iconName, MaterialIconKind kind, double size, Brush brush)
        {
            if (_unicodeStringDictionary[iconName] is not string unicodeString)
            {
                throw new InvalidOperationException($"Specified {nameof(iconName)} is not valid.");
            }

            Glyphs glyphs = new()
            {
                FontUri = GetFontUri(Kind),
                UnicodeString = unicodeString,
                FontRenderingEmSize = 100,
                StyleSimulations = StyleSimulations.None,
                Fill = Brush,
                OriginX = 0,
                OriginY = 100
            };

            return new DrawingImage(new GlyphRunDrawing(Brush, glyphs.ToGlyphRun()));
    }

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
        #endregion Private methods
    }
}
