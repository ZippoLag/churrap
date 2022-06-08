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

        public MapaClientePage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MapaClienteViewModel();

            //TODO: configurar como parametro el radio
            MapaCliente.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    _viewModel.PosicionCliente, Distance.FromKilometers(1)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}