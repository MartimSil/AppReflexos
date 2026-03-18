using REFTEST.Pages.ING.Testes;
using REFTEST.Pages.ING.Main;
namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteEscolherAJUDA : ContentPage
{
    public TesteEscolherAJUDA()
    {
        InitializeComponent();
    }
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePageING());
    }
    private async void OnTesteEscolherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteEscolher());
    }
}