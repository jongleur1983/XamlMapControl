﻿// XAML Map Control - http://xamlmapcontrol.codeplex.com/
// Copyright © 2014 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

#if WINDOWS_RUNTIME
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
#else
using System.Windows;
using System.Windows.Media;
using GenericDependencyProperties;
using GenericDependencyProperties.GenericMetadata;

#endif

namespace MapControl
{
    /// <summary>
    /// Fills a rectangular area with an ImageBrush from the Source property.
    /// </summary>
    public class MapImage : MapRectangle
    {
        public static readonly DependencyProperty SourceProperty = GenericDependencyProperty.Register(
            src => src.Source,
            new GenericPropertyMetadata<ImageSource, MapImage>((mi, args) => mi.SourceChanged(args.NewValue)));
        // the following has been replaced by the generic variant above:
        //public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
        //    nameof(Source),
        //    typeof(ImageSource),
        //    typeof(MapImage),
        //    new PropertyMetadata(
        //        null,
        //        (o, e) => ((MapImage)o).SourceChanged((ImageSource)e.NewValue)));

        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        private void SourceChanged(ImageSource image)
        {
            var transform = new MatrixTransform
            {
                Matrix = new Matrix(1d, 0d, 0d, -1d, 0d, 1d)
            };
            transform.Freeze();

            Fill = new ImageBrush
            {
                ImageSource = image,
                RelativeTransform = transform
            };
        }
    }
}
