namespace REFTEST.Pages.PT.Ajudas_para_testes;

public partial class TesteEscolherAJUDA : ContentPage
{
    public TesteEscolherAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("Como jogar",
            "1. Lê o nome da cor escrito no centro.\n2. Ignora a cor com que a palavra está pintada.\n3. Clica no botão que tem a cor que leste!",
            "Entendido");
        await Navigation.PopAsync();
    }
}