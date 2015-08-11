using AirSave.Views;
using Caliburn.Micro;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;

namespace AirSave.ViewModels
{
    public class GMapViewModel : Screen
    {
        public GMapViewModel()
        {

        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            var gmapView = view as GMapView;
            Map = gmapView.MapControl;

            Map.MapProvider = GMapProviders.OpenStreetMap;
            Map.Position = new PointLatLng(-33, 17);
            Map.Zoom = 13;
            Map.MaxZoom = 24;
            Map.MinZoom = 1;
        }


        private GMapControl _map;
        public GMapControl Map
        {
            get { return _map; }
            set
            {
                if (_map != value)
                {
                    _map = value;
                    NotifyOfPropertyChange(() => Map);
                }
            }
        }
    }
}
