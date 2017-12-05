using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CryptoFolio
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllCoinsPage : ContentPage
	{
		public AllCoinsPage()
		{
			InitializeComponent();
            BindingContext = new ViewModel.VMCoinList();
		}
	}
}