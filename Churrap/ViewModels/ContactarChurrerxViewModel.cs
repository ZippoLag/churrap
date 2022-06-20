using Churrap.Models;
using Churrap.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using System.Threading;
using System.ComponentModel;
using System.Linq;

namespace Churrap.ViewModels
{
    [QueryProperty(nameof(Nombre), nameof(Nombre))]
    public class ContactarChurrerxViewModel : BasePosicionViewModel
    {
        public ContactarChurrerxViewModel()
        {
            PropertyChanged += OnPosicionChangedUpdateDistancia;
        }

        public new void OnAppearing()
        {
            base.OnAppearing();

            Title = $"Contactar a {selectedChurrerx?.Nombre}?";
        }

        public new void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private string nombre;
        public string Nombre
        {
            get => nombre;
            set
            {
                nombre = value;
                CargarChurrerxAsync(value);
            }
        }

        #region CargarChurrerxAsync
        private Churrerx selectedChurrerx;
        public Churrerx SelectedChurrerx 
        { 
            get => selectedChurrerx; 
            private set => SetProperty(ref selectedChurrerx, value); 
        }

        private async void CargarChurrerxAsync(string nombre)
        {
            try
            {
                SelectedChurrerx = await ChurrerxService.GetChurrerxAsync(nombre);
                ActualizarDistancia();
                ActualizarDireccion();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"No se pudo cargar al Churrerx {nombre}");
            }
        }
        #endregion

        #region Distancia
        protected void OnPosicionChangedUpdateDistancia(object sender, PropertyChangedEventArgs e)
        {
            //TODO: cómo manejamos cambios en la posicion del churrerx?
            if (e.PropertyName.Equals("PosicionActual"))
            {
                ActualizarDistancia();
            }
        }

        private void ActualizarDistancia()
        {
            this.Distancia = Distance.BetweenPositions(this.PosicionActual, this.SelectedChurrerx.Posicion).Meters;
        }

        private double distancia;
        public double Distancia
        {
            get => distancia;
            private set => SetProperty(ref distancia, value);
        }
        #endregion

        #region Direccion
        private async void ActualizarDireccion()
        {
            var placemarks = await Geocoding.GetPlacemarksAsync(SelectedChurrerx.Posicion.Latitude, SelectedChurrerx.Posicion.Longitude);
            var ubicacion = placemarks.FirstOrDefault();
            if (ubicacion != null)
            {
                this.Direccion = $"{ubicacion.Thoroughfare} {ubicacion.SubThoroughfare}";
            }
            else
            {
                this.Direccion = "";
            }
        }

        private string direccion;
        public string Direccion
        {
            get => direccion;
            private set => SetProperty(ref direccion, value);
        }
        #endregion
    }
}