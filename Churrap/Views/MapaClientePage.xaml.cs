using Churrap.Models;
using Churrap.ViewModels;
using Churrap.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Churrap.Views
{
    public partial class MapaClientePage : TabbedPage
    {
        private MapaClienteViewModel _viewModel;

        public readonly Circle CirclePosicionActual;

        public MapaClientePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MapaClienteViewModel();
            
            _viewModel.PropertyChanged += OnIsBusyChanged;
            _viewModel.PropertyChanged += OnPosicionActualChanged;

            CirclePosicionActual = new Circle { Center = new Position(), Radius = Distance.FromMeters(20) };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.OnDisappearing();
        }

        /// <summary>
        /// Toda ContentPage tiene una propiedad interna IsBusy que puede o no mostrar indicadores según el SO y su versión. Aquí la mantenemos actualizada con la propiedad homónima del ViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnIsBusyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = _viewModel.IsBusy;
            }
        }

        /// <summary>
        /// Cada vez que cambia la posición actual, movemos el punto del usuario actual. También, si fue solicitado en la UI, movemos el mapa para centrarse en su posición.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPosicionActualChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("PosicionActual"))
            {
                DibujarPosicionActual();
                MoverMapaSiFueSolicitado();
            }
        }

        private void MoverMapa_Clicked(object sender, EventArgs e)
        {
            MoverMapa();
        }

        private void MoverMapaSiFueSolicitado()
        {
            if (MoverMapaSwitch.IsToggled)
            {
                MoverMapa();
            }
        }

        private void MoverMapa()
        {
            if (_viewModel.PosicionActual != null)
            {
                MapaCliente.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        _viewModel.PosicionActual,
                        Distance.FromKilometers(1)));
            }
        }

        private void DibujarPosicionActual()
        {
            if (_viewModel.PosicionActual != null)
            {
                //TODO: agregar animación en lugar de simplemente mover de un tirón el círculo cambiando la posición del centro
                CirclePosicionActual.Center = _viewModel.PosicionActual;
                if (!MapaCliente.MapElements.Contains(CirclePosicionActual))
                {
                    MapaCliente.MapElements.Add(CirclePosicionActual);
                }
            }
        }

        /// <summary>
        /// Idealmente usaría un command, pero los Pin de Map no soportan Bind a command.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChurrerxPin_MarkerClicked(object sender, PinClickedEventArgs e)
        {
            _ = _viewModel.SeleccionarChurrerx(((Pin)sender).Label);
        }
    }
}