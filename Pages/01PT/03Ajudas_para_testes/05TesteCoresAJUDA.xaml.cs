namespace REFTEST.Pages.PT.Ajudas_para_testes;

public partial class TesteCoresAJUDA : ContentPage
{
    public TesteCoresAJUDA()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // O popup exato como na tua imagem
        await DisplayAlertAsync("Como jogar",
            "1. O jogo adiciona regras novas a cada 4 turnos.\n" +
            "2. Lê a regra ativa no topo do ecrã.\n" +
            "3. Clica no objeto apenas se ele cumprir a regra!",
            "Entendido");

        // Volta automaticamente para o teste após clicar em "Entendido"
        await Navigation.PopAsync();
    }
}