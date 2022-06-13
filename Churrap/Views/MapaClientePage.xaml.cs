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

            _viewModel.PropertyChanged += OnPosicionActualChanged;
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

        protected void OnPosicionActualChanged(object sender, PropertyChangedEventArgs e)
        {
            //TODO: configurar como parametro el radio?
            if (e.PropertyName.Equals("PosicionActual"))
            {
                MapaCliente.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    _viewModel.PosicionActual,
                    Distance.FromKilometers(1)));
            }
        }
    }
}