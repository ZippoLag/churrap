using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace Churrap
{
    public partial class AppShell : TabbedPage
    {

        private static bool SonMismoPunto(Position a, Position b)
        {
            return Distance.BetweenPositions(a, b).Meters < 1;
        }

        
        public AppShell()
        {
            InitializeComponent();

            //TODO: reemplazar con ubicacion real para centrar el mapa
            var ubicacionActual = new Position(-32.96, -60.66);

            MapaCliente.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    ubicacionActual, Distance.FromKilometers(2)));

            //mostrando radio de churrerxs
            //TODO: configurar como parametro el radio
            Circle radioLocal = new Circle
            {
                Center = ubicacionActual,
                Radius = new Distance(500),
                StrokeColor = Color.FromHex("#88FF0000"),
                StrokeWidth = 8,
                FillColor = Color.FromHex("#88FFC0CB")
            };
            MapaCliente.MapElements.Add(radioLocal);
        }

        private void Street_OnClicked(object sender, EventArgs e)
        {
            MapaCliente.MapType = MapType.Street;
        }

        private void Hybrid_OnClicked(object sender, EventArgs e)
        {
            MapaCliente.MapType = MapType.Hybrid;
        }

        private void Satellite_OnClicked(object sender, EventArgs e)
        {
            MapaCliente.MapType = MapType.Satellite;
        }
    }
}
