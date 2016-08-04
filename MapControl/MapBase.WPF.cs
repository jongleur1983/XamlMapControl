// XAML Map Control - http://xamlmapcontrol.codeplex.com/
// Copyright © 2014 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GenericDependencyProperties;
using GenericDependencyProperties.GenericMetadata;

namespace MapControl
{
    public partial class MapBase
    {
        // TODO: requires https://github.com/jongleur1983/genericDependencyProperties/issues/13
        public static readonly DependencyProperty ForegroundProperty =
            System.Windows.Controls.Control.ForegroundProperty.AddOwner(typeof(MapBase));

        //public static readonly DependencyProperty CenterProperty = GenericDependencyProperty.Register<Location, MapBase>(
        //    mb => mb.Center,
        //    new GenericFrameworkPropertyMetadata<Location, MapBase>(
        //        new Location(),
        //        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
        //        (mb, args) => mb.CenterPropertyChanged(args.NewValue)));
        // TODO: replace, requires https://github.com/jongleur1983/genericDependencyProperties/issues/15
        public static readonly DependencyProperty CenterProperty = DependencyProperty.Register(
            nameof(Center),
            typeof(Location),
            typeof(MapBase),
            new FrameworkPropertyMetadata(
                new Location(),
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((MapBase)o).CenterPropertyChanged((Location)e.NewValue)));

        // TODO: replace, requires https://github.com/jongleur1983/genericDependencyProperties/issues/15
        public static readonly DependencyProperty TargetCenterProperty = DependencyProperty.Register(
            nameof(TargetCenter),
            typeof(Location),
            typeof(MapBase),
            new FrameworkPropertyMetadata(
                new Location(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((MapBase)o).TargetCenterPropertyChanged((Location)e.NewValue)));

        // TODO: replace, requires https://github.com/jongleur1983/genericDependencyProperties/issues/15
        public static readonly DependencyProperty ZoomLevelProperty = DependencyProperty.Register(
            nameof(ZoomLevel),
            typeof(double),
            typeof(MapBase),
            new FrameworkPropertyMetadata(
                1d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((MapBase)o).ZoomLevelPropertyChanged((double)e.NewValue)));

        // TODO: replace, requires https://github.com/jongleur1983/genericDependencyProperties/issues/15
        public static readonly DependencyProperty TargetZoomLevelProperty = DependencyProperty.Register(
            nameof(TargetZoomLevel),
            typeof(double),
            typeof(MapBase),
            new FrameworkPropertyMetadata(
                1d,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((MapBase)o).TargetZoomLevelPropertyChanged((double)e.NewValue)));

        // TODO: replace, requires https://github.com/jongleur1983/genericDependencyProperties/issues/15
        public static readonly DependencyProperty HeadingProperty = DependencyProperty.Register(
            nameof(Heading),
            typeof(double),
            typeof(MapBase),
            new FrameworkPropertyMetadata(
                0d,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((MapBase)o).HeadingPropertyChanged((double)e.NewValue)));

        // TODO: replace, requires https://github.com/jongleur1983/genericDependencyProperties/issues/15
        public static readonly DependencyProperty TargetHeadingProperty = DependencyProperty.Register(
            nameof(TargetHeading),
            typeof(double),
            typeof(MapBase),
            new FrameworkPropertyMetadata(
                0d,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (o, e) => ((MapBase)o).TargetHeadingPropertyChanged((double)e.NewValue)));

        static MapBase()
        {
            UIElement.ClipToBoundsProperty.OverrideMetadata(
                typeof(MapBase), new FrameworkPropertyMetadata(true));

            Panel.BackgroundProperty.OverrideMetadata(
                typeof(MapBase), new FrameworkPropertyMetadata(Brushes.Transparent));
        }

        partial void RemoveAnimation(DependencyProperty property)
        {
            BeginAnimation(property, null);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            ResetTransformOrigin();
            UpdateTransform();
        }

        private void SetTransformMatrixes()
        {
            Matrix rotateMatrix = Matrix.Identity;
            rotateMatrix.Rotate(Heading);
            rotateTransform.Matrix = rotateMatrix;
            scaleTransform.Matrix = new Matrix(CenterScale, 0d, 0d, CenterScale, 0d, 0d);
            scaleRotateTransform.Matrix = scaleTransform.Matrix * rotateMatrix;
        }
    }
}
