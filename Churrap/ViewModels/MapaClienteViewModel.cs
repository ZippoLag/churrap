﻿using Churrap.Models;
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

namespace Churrap.ViewModels
{
    public class MapaClienteViewModel : BaseViewModel
    {
        private Churrerx _churrerxSeleccionado;

        protected CancellationTokenSource geoCTS;
        protected Position posicionActual;
        public Position PosicionActual
        {
            get => posicionActual;
            private set => SetProperty(ref posicionActual, value);
        }
        public ObservableCollection<Churrerx> Churrerxs { get; }
        public Command LoadChurrerxsCommand { get; }
        public Command AddChurrerxCommand { get; }
        public Command<Churrerx> ChurrerxTappedCommand { get; }

        public Command ActualizarPosicionActualCommand { get; }

        public MapaClienteViewModel()
        {
            Title = "Mapa de Churrerxs";
            Churrerxs = new ObservableCollection<Churrerx>();

            ActualizarPosicionActualCommand = new Command(async () => await ActualizarPosicionActual());

            LoadChurrerxsCommand = new Command(async () => await ExecuteLoadChurrerxsCommand());

            ChurrerxTappedCommand = new Command<Churrerx>(OnItemSelected);

            AddChurrerxCommand = new Command(OnAddChurrerx);
        }

        private async Task ExecuteLoadChurrerxsCommand()
        {
            IsBusy = true;

            try
            {
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
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedChurrerx = null;

            Task.Run(ActualizarPosicionActual);
        }
        public void OnDisappearing()
        {
            if (geoCTS != null && !geoCTS.IsCancellationRequested)
            {
                geoCTS.Cancel();
            }
        }

        public Churrerx SelectedChurrerx
        {
            get => _churrerxSeleccionado;
            set
            {
                SetProperty(ref _churrerxSeleccionado, value);
                OnItemSelected(value);
            }
        }

        private async void OnAddChurrerx(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewChurrerxPage));
        }

        private async void OnItemSelected(Churrerx chx)
        {
            if (chx == null)
                return;

            //await Shell.Current.GoToAsync($"{nameof(ChurrerxDetailPage)}?{nameof(ChurrerxDetailViewModel.ChurrerxId)}={item.Id}");
        }

        private async Task ActualizarPosicionActual()
        {
            IsBusy = true;

            try
            {
                Location posicionAsLocation = await Geolocation.GetLastKnownLocationAsync();
                if (posicionAsLocation != null)
                {
                    PosicionActual = new Position(posicionAsLocation.Latitude, posicionAsLocation.Longitude);
                }

                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                geoCTS = new CancellationTokenSource();
                posicionAsLocation = await Geolocation.GetLocationAsync(request, geoCTS.Token);

                if (posicionAsLocation != null)
                {
                    PosicionActual = new Position(posicionAsLocation.Latitude, posicionAsLocation.Longitude);
                }
            }
            finally
            {
                IsBusy = false;
                geoCTS = null;
            }
        }
    }
}