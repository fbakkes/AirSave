using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Windows.Controls;

namespace AirSave.Views
{
    public partial class GMapView : UserControl
    {
        public GMapView()
        {
            InitializeComponent();
            //Map.MapProvider = GMapProviders.GoogleSatelliteMap;
            //Map.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            //Map.Zoom = 13;
            //Map.MaxZoom = 24;
            //Map.MinZoom = 1;
        }
        public GMapControl MapControl { get { return Map; } }
    }
}
