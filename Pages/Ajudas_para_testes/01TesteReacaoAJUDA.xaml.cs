using System.Diagnostics;
using REFTEST.Pages.Testes;

namespace REFTEST.Pages.Ajudas_para_testes;

public partial class TesteReacaoAJUDA : ContentPage
{
    public TesteReacaoAJUDA()
    {
        InitializeComponent();
    }
    private async void OnTesteReacaoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteReacao());
    }
}