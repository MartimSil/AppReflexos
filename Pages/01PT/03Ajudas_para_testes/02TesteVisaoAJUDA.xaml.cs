namespace REFTEST.Pages.PT.Ajudas_para_testes;

public partial class TesteVisaoAJUDA : ContentPage
{
    public TesteVisaoAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("Como jogar",
            "1. Uma palavra aparecerá rapidamente no ecrã.\n2. Tenta memorizar se ela está escrita corretamente.\n3. Responde SIM ou NÃO após a palavra desaparecer.",
            "Entendido");
        await Navigation.PopAsync();
    }
}