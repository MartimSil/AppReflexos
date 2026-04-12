namespace REFTEST.Pages.PT.Ajudas_para_testes;

public partial class TesteReacaoAJUDA : ContentPage
{
    public TesteReacaoAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("Como jogar",
            "1. Clica em Iniciar.\n2. Espera que as 5 luzes fiquem vermelhas.\n3. Quando as luzes APAGAREM, clica o mais rápido que conseguires!",
            "Entendido");
        await Navigation.PopAsync();
    }
}