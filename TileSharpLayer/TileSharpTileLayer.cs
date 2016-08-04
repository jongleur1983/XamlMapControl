using System;
using System.Windows;
using GenericDependencyProperties;
using GenericDependencyProperties.GenericMetadata;
using MapControl;
using TileSharp;
using TileSharp.LabelOverlapPreventers;

namespace TileSharpLayer
{
    public class TileSharpTileLayer : TileLayer
    {
        public TileSharpTileLayer()
        {
            OverlapPreventer = new PerfectQuadtreePreventer();
            FeatureCache = new NullCache();
        }

        #region LayerConfig
        public LayerConfig LayerConfig
        {
            get { return (LayerConfig)GetValue(LayerConfigProperty); }
            set { SetValue(LayerConfigProperty, value); }
        }

        public static readonly DependencyProperty LayerConfigProperty = GenericDependencyProperty.Register(
            tstl => tstl.LayerConfig,
            new GenericPropertyMetadata<LayerConfig, TileSharpTileLayer>(
                null,
                (tstl, args) => tstl.ConfigChanged()));
        // replaced by the generic variant above:
        //public static readonly DependencyProperty LayerConfigProperty = DependencyProperty.Register(
        //    nameof(LayerConfig),
        //    LayerConfigPropertyType,
        //    typeof(TileSharpTileLayer),
        //    new PropertyMetadata(
        //        null,
        //        (o, e) => ((TileSharpTileLayer)o).ConfigChanged()));
        #endregion

        #region OverlapPreventer
        public ILabelOverlapPreventer OverlapPreventer
        {
            get { return (ILabelOverlapPreventer)GetValue(OverlapPreventerProperty); }
            set { SetValue(OverlapPreventerProperty, value); }
        }

        public static readonly DependencyProperty OverlapPreventerProperty = GenericDependencyProperty.Register(
            tstl => tstl.OverlapPreventer,
            new GenericPropertyMetadata<ILabelOverlapPreventer, TileSharpTileLayer>(
                null,
                (tstl, args) => tstl.ConfigChanged()));
        // replaced by the generic variant above:
        //public static readonly DependencyProperty OverlapPreventerProperty = DependencyProperty.Register(
        //    nameof(OverlapPreventer),
        //    OverlapPreventerPropertyType,
        //    typeof(TileSharpTileLayer),
        //    new PropertyMetadata(
        //        null,
        //        (o, e) => ((TileSharpTileLayer)o).ConfigChanged()));
        #endregion

        #region FeatureCache
        public IFeatureCache FeatureCache
        {
            get { return (IFeatureCache)GetValue(FeatureCacheProperty); }
            set { SetValue(FeatureCacheProperty, value); }
        }

        public static readonly DependencyProperty FeatureCacheProperty = GenericDependencyProperty.Register(
            tstl => tstl.FeatureCache,
            new GenericPropertyMetadata<IFeatureCache, TileSharpTileLayer>(
                null,
                (tstl, args) => tstl.ConfigChanged()));
        // replaced by the generic variant above:
        //public static readonly DependencyProperty FeatureCacheProperty = DependencyProperty.Register(
        //    nameof(FeatureCache),
        //    FeatureCachePropertyType,
        //    typeof(TileSharpTileLayer),
        //    new PropertyMetadata(
        //        null,
        //        (o, e) => ((TileSharpTileLayer)o).ConfigChanged()));
        #endregion


        private void ConfigChanged()
        {
            TileSource = new TileSharpSource(OverlapPreventer, FeatureCache, LayerConfig);
        }
    }
}
