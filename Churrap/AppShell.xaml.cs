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

            //TODO: configurar como parametro el radio
            MapaCliente.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    ubicacionActual, Distance.FromKilometers(1)));
        }
    }
}
