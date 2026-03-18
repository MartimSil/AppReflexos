using REFTEST.Pages.ING.Testes;
using REFTEST.Pages.ING.Main;

namespace REFTEST.Pages.ING.Ajudas_para_testes;
public partial class TesteContarAJUDA : ContentPage
{
	public TesteContarAJUDA()
	{
		InitializeComponent();
	}
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePageING());
    }
    private async void OnTesteContarClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteContar());
    }
}