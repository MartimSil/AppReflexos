using System.Diagnostics;
using REFTEST.Pages.PT.Testes;
using REFTEST.Pages.PT.Main;
namespace REFTEST.Pages.PT.Ajudas_para_testes;

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