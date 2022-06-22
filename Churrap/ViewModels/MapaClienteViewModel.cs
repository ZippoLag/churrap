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
    public class MapaClienteViewModel : BasePosicionViewModel
    {
        public ObservableCollection<Churrerx> Churrerxs { get; }
        public Command AddChurrerxCommand { get; }

        public MapaClienteViewModel()
        {
            Title = "Mapa de Churrerxs";
            Churrerxs = new ObservableCollection<Churrerx>();

             CargarChurrerxsCommand = new Command(async () => await CargarChurrerxs(), () => !CargandoChurrerxs);

            SeleccionarChurrerxCommand = new Command<string>(async (nombre) => await SeleccionarChurrerx(nombre));

            AddChurrerxCommand = new Command(OnAddChurrerx);

            this.PropertyChanged -= base.UpdateIsBusyOnPropertyChanged;
            this.PropertyChanged += UpdateIsBusyOnPropertyChanged;
        }

        public new void OnAppearing()
        {
            base.OnAppearing();

            Task.Run(CargarChurrerxs);
        }
        public new void OnDisappearing()
        {
            base.OnDisappearing();
        }

        /// <summary>
        /// Muchas acciones pueden estar procesando algo en el background, si cualquiera de ellas está sucediendo, informamos que el VM está ocupado con IsBusy=true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        new protected void UpdateIsBusyOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("IsActualizandoPosicion") || e.PropertyName.Equals("CargandoChurrerxs"))
            {
                this.IsBusy = this.IsActualizandoPosicion || this.CargandoChurrerxs;
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

        public Command SeleccionarChurrerxCommand { get; }
        public async Task SeleccionarChurrerx(string nombre)
        {
            await Shell.Current.GoToAsync($"{nameof(ContactarChurrerxPage)}?{nameof(ContactarChurrerxViewModel.Nombre)}={nombre}");
        }
    }
}