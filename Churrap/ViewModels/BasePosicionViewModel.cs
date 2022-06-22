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
            this.PropertyChanged += UpdateIsBusyOnPropertyChanged;
        }

        public void OnAppearing()
        {
            actualizandoPosicionTask = Task.Run(ActualizarPosicionActual);
        }
        public void OnDisappearing()
        {
            if (actualizandoPosicionCT != null && !actualizandoPosicionCT.IsCancellationRequested)
            {
                actualizandoPosicionCT.Cancel();
            }
        }

        /// <summary>
        /// Muchas acciones pueden estar procesando algo en el background, si cualquiera de ellas está sucediendo, informamos que el VM está ocupado con IsBusy=true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void UpdateIsBusyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsActualizandoPosicion"))
            {
                this.IsBusy = this.IsActualizandoPosicion;
            }
        }

        #region ActualizarPosicionActual
        protected CancellationTokenSource actualizandoPosicionCT;
        protected Task actualizandoPosicionTask;
        protected bool isActualizandoPosicion;
        public bool IsActualizandoPosicion
        {
            get => isActualizandoPosicion;
            private set => SetProperty(ref isActualizandoPosicion, value);
        }
        protected Position posicionActual;
        public Position PosicionActual
        {
            get => posicionActual;
            private set => SetProperty(ref posicionActual, value);
        }

        private async Task ActualizarPosicionActual()
        {
            if (actualizandoPosicionCT?.IsCancellationRequested ?? false)
                return;

            try
            {
                actualizandoPosicionCT = new CancellationTokenSource();
                Location posicionAsLocation = null;

                if (PosicionActual == null)
                {
                    IsActualizandoPosicion = true;
                    posicionAsLocation = await Geolocation.GetLastKnownLocationAsync();

                    if (posicionAsLocation != null)
                    {
                        PosicionActual = new Position(posicionAsLocation.Latitude, posicionAsLocation.Longitude);
                    }
                    IsActualizandoPosicion = false;
                }

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

                do
                {
                    IsActualizandoPosicion = true;
                    posicionAsLocation = await Geolocation.GetLocationAsync(request, actualizandoPosicionCT.Token);

                    if (posicionAsLocation != null)
                    {
                        PosicionActual = new Position(posicionAsLocation.Latitude, posicionAsLocation.Longitude);
                    }

                    IsActualizandoPosicion = false;
                    await Task.Delay(10000);
                } while (!actualizandoPosicionCT.IsCancellationRequested);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                IsActualizandoPosicion = false;

                actualizandoPosicionCT.Dispose();
                actualizandoPosicionCT = null;

                actualizandoPosicionTask.Dispose();
                actualizandoPosicionTask = null;
            }
        }
        #endregion
    }
}