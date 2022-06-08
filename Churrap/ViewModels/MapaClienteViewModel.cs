using Churrap.Models;
using Churrap.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Churrap.ViewModels
{
    public class MapaClienteViewModel : BaseViewModel
    {
        private Churrerx _churrerxSeleccionado;

        public Position PosicionCliente { get; }
        public ObservableCollection<Churrerx> Churrerxs { get; }
        public Command LoadChurrerxsCommand { get; }
        public Command AddChurrerxCommand { get; }
        public Command<Churrerx> ChurrerxTappedCommand { get; }

        public MapaClienteViewModel()
        {
            Title = "Mapa de Churrerxs";
            Churrerxs = new ObservableCollection<Churrerx>();

            //TODO: reemplazar con ubicacion real para centrar el mapa
            PosicionCliente = new Position(-32.96, -60.66);

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
    }
}