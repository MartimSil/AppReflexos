using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Testes;

namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteVisaoAJUDA : ContentPage
{
    public TesteVisaoAJUDA()
    {
        InitializeComponent();
    }
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePageING());
    }
    private async void OnTesteVisaoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteVisao());
    }
}