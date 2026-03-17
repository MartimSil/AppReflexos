using System.Diagnostics;
using REFTEST.Pages.ING.Testes;
using REFTEST.Pages.ING.Main;
namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteCoresAJUDA : ContentPage
{
    public TesteCoresAJUDA()
    {
        InitializeComponent();
    }
    private async void OnGoToHomeClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HomePage());
    }
    private async void OnTesteCoresClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TesteCores());
    }
}