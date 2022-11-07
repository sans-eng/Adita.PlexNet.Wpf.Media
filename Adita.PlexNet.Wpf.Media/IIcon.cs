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
