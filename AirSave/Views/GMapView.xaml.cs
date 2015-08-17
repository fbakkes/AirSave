using AirSave.GMap;
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
        }

        public GMapControlModel MapControl { get { return Map; } }
    }
}
