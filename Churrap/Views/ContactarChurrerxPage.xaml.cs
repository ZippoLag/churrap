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
    public partial class ContactarChurrerxPage : ContentPage
    {
        private ContactarChurrerxViewModel _viewModel;

        public ContactarChurrerxPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ContactarChurrerxViewModel();
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
    }
}