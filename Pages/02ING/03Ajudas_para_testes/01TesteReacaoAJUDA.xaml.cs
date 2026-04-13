namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteReacaoAJUDA : ContentPage
{
    public TesteReacaoAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("How to play",
            "1. Click Start.\n2. Wait for the 5 lights to turn red.\n3. When the lights GO OUT, click as fast as you can!",
            "Understood");
        await Navigation.PopAsync();
    }
}