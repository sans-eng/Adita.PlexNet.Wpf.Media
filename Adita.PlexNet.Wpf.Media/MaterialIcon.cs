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
using System.Windows.Media;

namespace Adita.PlexNet.Wpf.Media
{
    /// <summary>
    /// Represents an icon.
    /// </summary>
    public sealed class MaterialIcon : UIElement, IIcon
    {
        #region Private constants
        private const string _standardFontPath = "pack://application:,,,/Adita.PlexNet.Wpf.Media;component/Resources/Fonts/MaterialIcons-Regular.ttf";
        private const string _outlineFontPath = "pack://application:,,,/Adita.PlexNet.Wpf.Media;component/Resources/Fonts/MaterialIconsOutlined-Regular.otf";
        private const string _roundFontPath = "pack://application:,,,/Adita.PlexNet.Wpf.Media;component/Resources/Fonts/MaterialIconsRound-Regular.otf";
        private const string _sharpFontPath = "pack://application:,,,/Adita.PlexNet.Wpf.Media;component/Resources/Fonts/MaterialIconsSharp-Regular.otf";
        private const string _twoToneFontPath = "pack://application:,,,/Adita.PlexNet.Wpf.Media;component/Resources/Fonts/MaterialIconsTwoTone-Regular.otf";

        private const string _fontNamePath = "/Adita.PlexNet.Wpf.Media;component/Resources/Fonts/MaterialIconNames.xaml";
        #endregion Private constants

        #region Private fields
        private readonly ResourceDictionary _unicodeStringDictionary = (ResourceDictionary)Application.LoadComponent(new Uri(_fontNamePath, UriKind.Relative));
        Glyphs _childGlyps = new();
        private Size _availableSize;
        #endregion Private fields

        #region Constructors
        /// <summary>
        /// Initialize a new instance of <see cref="MaterialIcon"/>.
        /// </summary>
        public MaterialIcon()
        {
            AddVisualChild(_childGlyps);
        }
        #endregion Constructors

        #region Dependency properties
        /// <summary>
        /// Identifies <see cref="IconName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconNameProperty =
            DependencyProperty.Register(nameof(IconName), typeof(string), typeof(MaterialIcon), new FrameworkPropertyMetadata(string.Empty, OnIconNameChanged));

        /// <summary>
        /// Identifies <see cref="Kind"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty KindProperty =
            DependencyProperty.Register(nameof(Kind), typeof(MaterialIconKind), typeof(MaterialIcon), new FrameworkPropertyMetadata(MaterialIconKind.Standard, OnKindChanged));

        /// <summary>
        /// Identifies <see cref="Size"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(double), typeof(MaterialIcon), new FrameworkPropertyMetadata(50d, OnSizeChanged));

        /// <summary>
        /// Identifies <see cref="Brush"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register(nameof(Brush), typeof(Brush), typeof(MaterialIcon), new FrameworkPropertyMetadata(Brushes.Black, OnBrushChanged));

        /// <summary>
        /// Identifies <see cref="HorizontalAlignment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(MaterialIcon), new FrameworkPropertyMetadata(HorizontalAlignment.Center, OnHorizontalAlignmentChanged));

        /// <summary>
        /// Identifies <see cref="VerticalAlignment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalAlignment), typeof(VerticalAlignment), typeof(MaterialIcon), new FrameworkPropertyMetadata(VerticalAlignment.Center, OnVerticalAlignmentChanged));
        #endregion Dependency properties

        #region Public properties
        /// <summary>
        /// Gets or sets the name of the icon to display.
        /// </summary>
        /// <remarks>The name is predefined on system PlexNet framework,
        /// see <a href="https://docs.plexdotnet.adita-engineering.com/wpf/media/material-icon"/> for documentation.</remarks>
        public string IconName
        {
            get => (string)GetValue(IconNameProperty);
            set => SetValue(IconNameProperty, value);
        }
        /// <summary>
        /// Gets or sets the kind of current <see cref="MaterialIcon"/>.
        /// </summary>
        public MaterialIconKind Kind
        {
            get => (MaterialIconKind)GetValue(KindProperty);
            set => SetValue(KindProperty, value);
        }
        /// <summary>
        /// Gets or sets a uniform size of current <see cref="MaterialIcon"/>.
        /// </summary>
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }
        /// <summary>
        /// Gets or sets a <see cref="Brush"/> of current <see cref="MaterialIcon"/>.
        /// </summary>
        public Brush Brush
        {
            get => (Brush)GetValue(BrushProperty);
            set => SetValue(BrushProperty, value);
        }
        /// <summary>
        /// Gets or sets a <see cref="System.Windows.HorizontalAlignment"/> of current <see cref="MaterialIcon"/>.
        /// </summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalAlignmentProperty); }
            set { SetValue(HorizontalAlignmentProperty, value); }
        }
        /// <summary>
        /// Gets or sets a <see cref="System.Windows.VerticalAlignment"/> of current <see cref="MaterialIcon"/>.
        /// </summary>
        public VerticalAlignment VerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalAlignmentProperty); }
            set { SetValue(VerticalAlignmentProperty, value); }
        }
        #endregion Public properties

        #region Protected Properties
        /// <summary>Gets the number of child elements for the <see cref="Visual" />.</summary>
        /// <returns>The number of child elements.</returns>
        protected override int VisualChildrenCount => 1;
        #endregion Protected Properties

        #region Protected methods
        /// <summary>Provides measurement logic for sizing this element properly, with consideration of the size of any child element content.</summary>
        /// <param name="availableSize">The available size that the parent element can allocate for the child.</param>
        /// <returns>The desired size of this element in layout.</returns>
        protected override Size MeasureCore(Size availableSize)
        {
            _availableSize = availableSize;
            _childGlyps.Measure(availableSize);
            return _childGlyps.DesiredSize;
        }
        /// <summary>Defines the template for WPF core-level arrange layout definition.</summary>
        /// <param name="finalRect">The final area within the parent that element should use to arrange itself and its child elements.</param>
        protected override void ArrangeCore(Rect finalRect)
        {
            base.ArrangeCore(finalRect);
            _childGlyps.Arrange(GetChildRect());
            //_childGlyps.Arrange(new Rect(0, 0, Size, Size));
        }
        /// <summary>Returns the specified <see cref="Visual" /> in the parent <see cref="VisualCollection" />.</summary>
        /// <param name="index">The index of the visual object in the <see cref="VisualCollection" />.</param>
        /// <returns>The child in the <see cref="VisualCollection" /> at the specified <paramref name="index" /> value.</returns>
        protected override Visual GetVisualChild(int index)
        {
            return _childGlyps;
        }
        #endregion Protected methods

        #region Private methods
        private void UpdateIcon()
        {
            if (_unicodeStringDictionary[IconName] is not string unicodeString)
            {
                throw new ArgumentException($"Specified {nameof(IconName)} is not valid.");
            }

            _childGlyps.FontUri = GetFontUri(Kind);
            _childGlyps.UnicodeString = unicodeString;
            _childGlyps.FontRenderingEmSize = Size;
            _childGlyps.StyleSimulations = StyleSimulations.None;
            _childGlyps.Fill = Brush;
            _childGlyps.OriginX = 0;
            _childGlyps.OriginY = Size;

            InvalidateMeasure();
            UpdateLayout();
        }
        private Uri GetFontUri(MaterialIconKind iconKind)
        {
            return iconKind switch
            {
                MaterialIconKind.Standard => new(_standardFontPath),
                MaterialIconKind.Outline => new(_outlineFontPath),
                MaterialIconKind.Round => new(_roundFontPath),
                MaterialIconKind.Sharp => new(_sharpFontPath),
                MaterialIconKind.TwoTone => new(_twoToneFontPath),
                _ => throw new ArgumentException($"Specified {nameof(Kind)} is not availabble."),
            };
        }
        private Rect GetChildRect()
        {
            double x = HorizontalAlignment switch
            {
                HorizontalAlignment.Center => double.IsFinite(_availableSize.Width) ? (_availableSize.Width / 2) - (Size / 2) : 0,
                HorizontalAlignment.Right => double.IsFinite(_availableSize.Width) ? _availableSize.Width - Size : 0,
                _ => 0
            };
            double y = VerticalAlignment switch
            {
                VerticalAlignment.Center => double.IsFinite(_availableSize.Height) ? (_availableSize.Height / 2) - (Size / 2) : 0,
                VerticalAlignment.Bottom => double.IsFinite(_availableSize.Height) ? _availableSize.Height - Size : 0,
                _ => 0
            };

            return new(x, y, Size, Size);

        }
        #endregion Private methods

        #region Dependency property changed event handlers
        private static void OnIconNameChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.UpdateIcon();
        }
        private static void OnKindChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.UpdateIcon();
        }
        private static void OnSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.UpdateIcon();
        }
        private static void OnBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.UpdateIcon();
        }
        private static void OnHorizontalAlignmentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.UpdateIcon();
        }
        private static void OnVerticalAlignmentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not MaterialIcon materialIcon)
            {
                return;
            }

            materialIcon.UpdateIcon();
        }
        #endregion Dependency property changed event handlers
    }
}
