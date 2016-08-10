// XAML Map Control - http://xamlmapcontrol.codeplex.com/
// Copyright © 2014 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using GenericDependencyProperties;
using GenericDependencyProperties.GenericMetadata;

namespace MapControl
{
    public partial class MapPath : Shape
    {
        public static readonly DependencyProperty DataProperty = GenericDependencyProperty.Register(
            mp => mp.Data,
            new GenericFrameworkPropertyMetadata<Geometry, MapPath>(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public Geometry Data
        {
            get { return (Geometry)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        protected override Geometry DefiningGeometry => Data;
    }
}
