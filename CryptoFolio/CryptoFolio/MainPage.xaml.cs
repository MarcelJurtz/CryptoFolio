using CryptoFolio.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CryptoFolio
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new VMMainPage();

            cmdLoadCoinList.Clicked += OnCoinListClick;
		}

        void OnCoinListClick(Object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllCoinsPage());
        }
	}
}
