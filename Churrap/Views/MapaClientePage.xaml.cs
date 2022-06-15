﻿using Churrap.Models;
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

        protected void OnPosicionActualChanged(object sender, PropertyChangedEventArgs e)
        {
            //TODO: configurar como parametro el radio?
            if (e.PropertyName.Equals("PosicionActual"))
            {
                MoverPinYVistaMapaAPosicionActual();
            }
        }

        protected void OnActualizarPosicionTapped(object sender, EventArgs e)
        {
            MoverPinYVistaMapaAPosicionActual();

            _viewModel.ActualizarPosicionActualCommand.Execute(sender);
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

        private void ChurrerxPin_MarkerClicked(Pin sender, PinClickedEventArgs e)
        {
            _viewModel.SeleccionarChurrerx(sender.Label);
        }

        private void ChurrerxListItem_Tapped(object sender, EventArgs e)
        {
            //TODO: obtener detalles del churrerx y usarlo para llamar a SeleccionarChurrerx
            Console.WriteLine(sender);
        }
    }
}