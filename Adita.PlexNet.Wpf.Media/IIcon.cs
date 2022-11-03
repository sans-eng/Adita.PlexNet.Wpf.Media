using System.Windows;
using System.Windows.Media;

namespace Adita.PlexNet.Wpf.Media
{
    /// <summary>
    /// provides an abstraction for an icon.
    /// </summary>
    public interface IIcon
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the icon to display.
        /// </summary>
        /// <remarks>The name is predefined on system PlexNet framework,
        /// see <a href="https://docs.plexdotnet.adita-engineering.com/wpf/media/material-icon"/> for documentation.</remarks>
        string IconName { get; set; }
        /// <summary>
        /// Gets or sets a uniform size of the icon.
        /// </summary>
        double Size { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="Brush"/> of the icon.
        /// </summary>
        Brush Brush { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="System.Windows.HorizontalAlignment"/> of the icon.
        /// </summary>
        HorizontalAlignment HorizontalAlignment { get; set; }
        /// <summary>
        /// Gets or sets a <see cref="System.Windows.VerticalAlignment"/> of the icon.
        /// </summary>
        VerticalAlignment VerticalAlignment { get; set; }
        #endregion Properties
    }
}
