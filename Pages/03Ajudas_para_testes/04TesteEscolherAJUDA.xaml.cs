using System.Diagnostics;
using REFTEST.Pages.Testes;

namespace REFTEST.Pages.Ajudas_para_testes;

public partial class TesteEscolherAJUDA : ContentPage
{
    public TesteEscolherAJUDA()
    {
        InitializeComponent();
    }
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
    private async void OnTesteEscolherClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteEscolher());
    }
}