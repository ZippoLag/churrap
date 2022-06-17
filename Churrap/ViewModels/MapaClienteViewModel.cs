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
    public class MapaClienteViewModel : BaseViewModel
    {
        public ObservableCollection<Churrerx> Churrerxs { get; }
        public Command AddChurrerxCommand { get; }

        public MapaClienteViewModel()
        {
            Title = "Mapa de Churrerxs";
            Churrerxs = new ObservableCollection<Churrerx>();

            ActualizarPosicionActualCommand = new Command(async () => await ActualizarPosicionActual(), () => ActualizandoPosicionCT == null);

            CargarChurrerxsCommand = new Command(async () => await CargarChurrerxs(), () => !CargandoChurrerxs);

            AddChurrerxCommand = new Command(OnAddChurrerx);

            this.PropertyChanged += UpdateIsBusyOnPropertyChanged;
        }

        public void OnAppearing()
        {
            Task.Run(ActualizarPosicionActual);
            Task.Run(CargarChurrerxs);
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
        protected void UpdateIsBusyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("ActualizandoPosicionCT") || e.PropertyName.Equals("CargandoChurrerxs"))
            {
                this.IsBusy = this.CargandoChurrerxs || this.ActualizandoPosicionCT != null;
            }
        }

        #region CargarChurrerxs
        private bool cargandoChurrerxs = false;
        public bool CargandoChurrerxs
        { 
            get => cargandoChurrerxs; 
            private set 
            { 
                SetProperty(ref cargandoChurrerxs, value); 
                CargarChurrerxsCommand?.ChangeCanExecute();
            }
        }
        public Command CargarChurrerxsCommand { get; }
        private async Task CargarChurrerxs()
        {
            if (CargandoChurrerxs)
                return;
            
            try
            {
                CargandoChurrerxs = true;

                Churrerxs.Clear();
                IEnumerable<Churrerx> churrerxs = await ChurrerxService.GetChurrerxsAsync(true);
                foreach (Churrerx churrerx in churrerxs)
                {
                    Churrerxs.Add(churrerx);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                CargandoChurrerxs = false;
            }
        }
        #endregion

        private async void OnAddChurrerx(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewChurrerxPage));
        }

        public async Task SeleccionarChurrerx(string nombre)
        {
            Churrerx chx = Churrerxs.FirstOrDefault(c => c.Nombre.Equals(nombre));

            if (chx == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(ContactarChurrerxPage)}?{nameof(ContactarChurrerxViewModel.Nombre)}={chx.Nombre}");
        }

        #region ActualizarPosicionActual
        protected CancellationTokenSource actualizandoPosicionCT = null;
        public CancellationTokenSource ActualizandoPosicionCT
        {
            get => actualizandoPosicionCT;
            private set
            {
                SetProperty(ref actualizandoPosicionCT, value);
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