// XAML Map Control - http://xamlmapcontrol.codeplex.com/
// Copyright © 2014 Clemens Fischer
// Licensed under the Microsoft Public License (Ms-PL)

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using GenericDependencyProperties;
using GenericDependencyProperties.GenericMetadata;
#if WINDOWS_RUNTIME
using Windows.Foundation;
using Windows.UI.Xaml;
#else
using System.Windows;
using System.ComponentModel;
#endif

namespace MapControl
{
    /// <summary>
    /// A polyline or polygon created from a collection of Locations.
    /// </summary>
    public partial class MapPolyline : MapPath
    {
#if WINDOWS_RUNTIME
        // For Windows Runtime, the Locations dependency property type is declared as object
        // instead of IEnumerable. See http://stackoverflow.com/q/10544084/1136211
        private static readonly Type LocationsPropertyType = typeof(object);
#else
        private static readonly Type LocationsPropertyType = typeof(IEnumerable<Location>);
#endif

        public static readonly DependencyProperty LocationsProperty = GenericDependencyProperty.Register(
            mpl => mpl.Locations,
            new GenericPropertyMetadata<IEnumerable<Location>, MapPolyline>(
                null,
                LocationsPropertyChanged));
        // replaced by the generic variant above:
        //public static readonly DependencyProperty LocationsProperty = DependencyProperty.Register(
        //    nameof(Locations),
        //    LocationsPropertyType,
        //    typeof(MapPolyline),
        //    new PropertyMetadata(
        //        null,
        //        LocationsPropertyChanged));

        public static readonly DependencyProperty IsClosedProperty = GenericDependencyProperty.Register(
            mpl => mpl.IsClosed,
            new GenericPropertyMetadata<bool, MapPolyline>(
                false,
                (mpl, args) => mpl.UpdateData()));
        //public static readonly DependencyProperty IsClosedProperty = DependencyProperty.Register(
        //    nameof(IsClosed),
        //    typeof(bool),
        //    typeof(MapPolyline),
        //    new PropertyMetadata(
        //        false,
        //        (o, e) => ((MapPolyline)o).UpdateData()));

        /// <summary>
        /// Gets or sets the locations that define the polyline points.
        /// </summary>
#if !WINDOWS_RUNTIME
        [TypeConverter(typeof(LocationCollectionConverter))]
#endif
        public IEnumerable<Location> Locations
        {
            get { return (IEnumerable<Location>)GetValue(LocationsProperty); }
            set { SetValue(LocationsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates if the polyline is closed, i.e. is a polygon.
        /// </summary>
        public bool IsClosed
        {
            get { return (bool)GetValue(IsClosedProperty); }
            set { SetValue(IsClosedProperty, value); }
        }

        private void LocationCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateData();
        }

        private static void LocationsPropertyChanged(MapPolyline mapPolyline, DependencyPropertyChangedEventArgs<IEnumerable<Location>> e)
        {
            var oldCollection = e.OldValue as INotifyCollectionChanged; // TODO: this is an unsave typecast (and was it before the generics as well)
            var newCollection = e.NewValue as INotifyCollectionChanged; // TODO: this is an unsave typecast (and was it before the generics as well)

            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= mapPolyline.LocationCollectionChanged;
            }

            if (newCollection != null)
            {
                newCollection.CollectionChanged += mapPolyline.LocationCollectionChanged;
            }

            mapPolyline.UpdateData();
        }
    }
}
