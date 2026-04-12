namespace REFTEST.Pages.PT.Ajudas_para_testes;

public partial class TesteContarAJUDA : ContentPage
{
    public TesteContarAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("Como jogar",
            "1. Tenta parar o cronómetro no tempo alvo indicado.\n2. Aos 3 segundos, os números vão desaparecer.\n3. Continua a contar mentalmente e para no segundo certo!",
            "Entendido");
        await Navigation.PopAsync();
    }
}