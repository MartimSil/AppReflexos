using System.Diagnostics;
using REFTEST.Pages.Testes;

namespace REFTEST.Pages.Ajudas_para_testes;

public partial class TesteVisaoAJUDA : ContentPage
{
    public TesteVisaoAJUDA()
    {
        InitializeComponent();
    }
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
    private async void OnTesteVisaoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteVisao());
    }
}