using System.Diagnostics;
using REFTEST.Pages.PT.Testes;
using REFTEST.Pages.PT.Main;
namespace REFTEST.Pages.PT.Ajudas_para_testes;
public partial class TesteContarAJUDA : ContentPage
{
	public TesteContarAJUDA()
	{
		InitializeComponent();
	}
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
    private async void OnGoToTesteContarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteContar());
    }
}