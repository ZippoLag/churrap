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
            _viewModel.PropertyChanged += OnPosicionActualChangedOrChanging;

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
        /// Si ya existe una posición guardada, es interesante mover la vista del mapa tan pronto como se presione el botón (y luego si hay una posición nueva, se actualizará moviéndose nuevamente). Esto se podría lograr mediante un Event Handler bindeado al Click del botón, no obstante reaccionar al Cancellation Token cumple con el mismo propósito y sólo utilizando Commands
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPosicionActualChangedOrChanging(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("PosicionActual") || e.PropertyName.Equals("ActualizandoPosicionCT"))
            {
                MoverPinYVistaMapaAPosicionActual();
            }
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
        private void MoverPinYVistaMapaAPosicionActual()
        {
            if (_viewModel.PosicionActual != null)
            {
                MapaCliente.MoveToRegion(
                    MapSpan.FromCenterAndRadius(
                        _viewModel.PosicionActual,
                        Distance.FromKilometers(1)));
                
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