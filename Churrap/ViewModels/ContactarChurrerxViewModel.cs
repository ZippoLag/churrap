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
            }
            catch (Exception)
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
                this.Distancia = Distance.BetweenPositions(this.PosicionActual, this.SelectedChurrerx.Posicion).Meters;
            }
        }

        private double distancia;
        public double Distancia
        {
            get => distancia;
            private set => SetProperty(ref distancia, value);
        }
        #endregion
    }
}