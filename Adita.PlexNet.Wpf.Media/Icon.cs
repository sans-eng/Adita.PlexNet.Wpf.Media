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
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Adita.PlexNet.Wpf.Media
{
    /// <summary>
    /// Represents a base class for an icon.
    /// </summary>
    public abstract class Icon : UIElement, IIcon
    {
        #region Private fields
        private readonly Image _visualChild = new();
        private Size _availableSize = new();
        #endregion Private fields

        #region Constructors
        /// <summary>
        /// Initialize a new instance of <see cref="Icon"/>.
        /// </summary>
        protected Icon()
        {
            AddVisualChild(_visualChild);
        }
        #endregion Constructors

        #region Dependency properties
        /// <summary>
        /// Identifies <see cref="IconName"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IconNameProperty =
            DependencyProperty.Register(nameof(IconName), typeof(string), typeof(Icon), new FrameworkPropertyMetadata(string.Empty, OnIconNameChanged));

        /// <summary>
        /// Identifies <see cref="Size"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register(nameof(Size), typeof(double), typeof(Icon), new FrameworkPropertyMetadata(50d, OnSizeChanged));

        /// <summary>
        /// Identifies <see cref="Brush"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register(nameof(Brush), typeof(Brush), typeof(Icon), new FrameworkPropertyMetadata(Brushes.Black, OnBrushChanged));

        /// <summary>
        /// Identifies <see cref="HorizontalAlignment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalAlignmentProperty =
            DependencyProperty.Register(nameof(HorizontalAlignment), typeof(HorizontalAlignment), typeof(Icon), new FrameworkPropertyMetadata(HorizontalAlignment.Center, OnHorizontalAlignmentChanged));

        /// <summary>
        /// Identifies <see cref="VerticalAlignment"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalAlignmentProperty =
            DependencyProperty.Register(nameof(VerticalAlignment), typeof(VerticalAlignment), typeof(Icon), new FrameworkPropertyMetadata(VerticalAlignment.Center, OnVerticalAlignmentChanged));
        #endregion Dependency properties

        #region Public properties
        /// <summary>
        /// Gets or sets the name of the icon to display.
        /// </summary>
        /// <remarks>The name is predefined on system PlexNet framework,
        /// see <a href="https://docs.plexdotnet.adita-engineering.com/wpf/media/icons"/> for documentation.</remarks>
        public string IconName
        {
            get => (string)GetValue(IconNameProperty);
            set => SetValue(IconNameProperty, value);
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
            _visualChild.Measure(availableSize);
            return _visualChild.DesiredSize;
        }
        /// <summary>Defines the template for WPF core-level arrange layout definition.</summary>
        /// <param name="finalRect">The final area within the parent that element should use to arrange itself and its child elements.</param>
        protected override void ArrangeCore(Rect finalRect)
        {
            base.ArrangeCore(finalRect);
            Rect childRect = GetRect(finalRect, _availableSize, new Size(Size, Size), HorizontalAlignment, VerticalAlignment);
           _visualChild.Arrange(childRect);
        }
        /// <summary>Returns the specified <see cref="Visual" /> in the parent <see cref="VisualCollection" />.</summary>
        /// <param name="index">The index of the visual object in the <see cref="VisualCollection" />.</param>
        /// <returns>The child in the <see cref="VisualCollection" /> at the specified <paramref name="index" /> value.</returns>
        protected override Visual GetVisualChild(int index)
        {
            return _visualChild;
        }
        /// <summary>
        /// When overriden, use this to updates the visual representation of current <see cref="Icon"/>
        /// by calling <see cref="UpdateIcon(string, Uri)"/>.
        /// </summary>
        protected internal abstract void Update();
        /// <summary>
        /// Updates the visual representation of current <see cref="Icon"/> using specified <paramref name="unicodeString"/>
        /// of the glyph from the font that located on <paramref name="fontUri"/>.
        /// </summary>
        /// <param name="unicodeString">The unicode string of the glyph.</param>
        /// <param name="fontUri">An <see cref="Uri"/> to the font file.</param>
        protected internal void UpdateIcon(string unicodeString, Uri fontUri)
        {
            _visualChild.Source = CreateImageSource(unicodeString, fontUri, Brush);
            _visualChild.Width = Size;
            _visualChild.Height = Size;
            _visualChild.HorizontalAlignment = HorizontalAlignment;
            _visualChild.VerticalAlignment = VerticalAlignment;

            InvalidateMeasure();
            UpdateLayout();
        }
        #endregion Protected methods

        #region Private methods
        private ImageSource CreateImageSource(string unicodeString, Uri fontUri, Brush brush)
        {
            Glyphs glyphs = new()
            {
                FontUri = fontUri,
                UnicodeString = unicodeString,
                FontRenderingEmSize = 100,
                StyleSimulations = StyleSimulations.None,
                Fill = brush,
                OriginX = 0,
                OriginY = 0
            };

            return new DrawingImage(new GlyphRunDrawing(glyphs.Fill, glyphs.ToGlyphRun()));
        }
        private Rect GetRect(Rect finalRect,
            Size availableSize,
            Size desiredSize,
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment)
        {
            double x = 0;

            switch (horizontalAlignment)
            {
                case HorizontalAlignment.Left: x = 0; break;
                case HorizontalAlignment.Center: x = double.IsFinite(availableSize.Width) ? (availableSize.Width/2) - (desiredSize.Width / 2) : 0; break;
                case HorizontalAlignment.Right: x = double.IsFinite(availableSize.Width) ? availableSize.Width - desiredSize.Width : 0; break;
                case HorizontalAlignment.Stretch: x = double.IsFinite(availableSize.Width) ? (availableSize.Width / 2) - (desiredSize.Width / 2) : 0; break;
            }

            double y = 0;

            switch (verticalAlignment)
            {
                case VerticalAlignment.Top: y = 0; break;
                case VerticalAlignment.Center: y = double.IsFinite(availableSize.Height) ? (availableSize.Height / 2) - (desiredSize.Height / 2) : 0; break;
                case VerticalAlignment.Bottom: y = double.IsFinite(availableSize.Height) ? availableSize.Height - desiredSize.Height : 0; break;
                case VerticalAlignment.Stretch: y = double.IsFinite(availableSize.Height) ? (availableSize.Height / 2) - (desiredSize.Height / 2) : 0; break;
            }

            if (finalRect.Width < availableSize.Width)
            {
                x = finalRect.X;
            }

            if (finalRect.Height < availableSize.Height)
            {
                y = finalRect.Y;
            }

            return new Rect(x, y, desiredSize.Width, desiredSize.Height);
        }
        #endregion Private methods

        #region Dependency property changed event handlers
        private static void OnIconNameChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Icon icon)
            {
                return;
            }

            icon.Update();
        }
        private static void OnSizeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Icon icon)
            {
                return;
            }

            icon.Update();
        }
        private static void OnBrushChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Icon icon)
            {
                return;
            }

            icon.Update();
        }
        private static void OnHorizontalAlignmentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Icon icon)
            {
                return;
            }

            icon.Update();
        }
        private static void OnVerticalAlignmentChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not Icon icon)
            {
                return;
            }

            icon.Update();
        }
        #endregion Dependency property changed event handlers
    }
}
