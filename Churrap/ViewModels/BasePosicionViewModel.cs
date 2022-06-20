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
using System.Linq;
using System.ComponentModel;

namespace Churrap.ViewModels
{
    public class BasePosicionViewModel : BaseViewModel
    {
        public BasePosicionViewModel()
        {
            ActualizarPosicionActualCommand = new Command(async () => await ActualizarPosicionActual(), () => ActualizandoPosicionCT == null);

            this.PropertyChanged += UpdateIsBusyOnPropertyChanged;
        }

        public void OnAppearing()
        {
            Task.Run(ActualizarPosicionActual);
        }
        public void OnDisappearing()
        {
            if (ActualizandoPosicionCT != null && !ActualizandoPosicionCT.IsCancellationRequested)
            {
                ActualizandoPosicionCT.Cancel();
                ActualizandoPosicionCT.Dispose();
                ActualizandoPosicionCT = null;
            }
        }

        /// <summary>
        /// Muchas acciones pueden estar procesando algo en el background, si cualquiera de ellas está sucediendo, informamos que el VM está ocupado con IsBusy=true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void UpdateIsBusyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ActualizandoPosicionCT"))
            {
                this.IsBusy = this.ActualizandoPosicionCT != null;
            }
        }

        #region ActualizarPosicionActual
        protected CancellationTokenSource actualizandoPosicionCT;
        public CancellationTokenSource ActualizandoPosicionCT
        {
            get => actualizandoPosicionCT;
            private set
            {
                SetProperty(ref actualizandoPosicionCT, value);
                //BUG: por qué comienza el botón deshabilitado a pesar de ser = null? (igual, aparece deshabilitado pero al tocarlo se ejecuta bien el metodo)
                ActualizarPosicionActualCommand?.ChangeCanExecute();
            }
        }
        protected Position posicionActual;
        public Position PosicionActual
        {
            get => posicionActual;
            private set => SetProperty(ref posicionActual, value);
        }
        public Command ActualizarPosicionActualCommand { get; }
        private async Task ActualizarPosicionActual()
        {
            if (ActualizandoPosicionCT != null)
                return;

            try
            {
                ActualizandoPosicionCT = new CancellationTokenSource(); ;

                Location posicionAsLocation = await Geolocation.GetLastKnownLocationAsync();
                if (posicionAsLocation != null)
                {
                    PosicionActual = new Position(posicionAsLocation.Latitude, posicionAsLocation.Longitude);
                }

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                posicionAsLocation = await Geolocation.GetLocationAsync(request, ActualizandoPosicionCT.Token);

                if (posicionAsLocation != null)
                {
                    PosicionActual = new Position(posicionAsLocation.Latitude, posicionAsLocation.Longitude);
                }
            }
            finally
            {
                ActualizandoPosicionCT = null;
            }
        }
        #endregion
    }
}