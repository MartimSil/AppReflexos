namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteEscolherAJUDA : ContentPage
{
    public TesteEscolherAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("How to play",
            "1. Read the name of the color written in the center.\n2. Ignore the color the word is painted with.\n3. Click the button that has the color you read!",
            "Got it");
        await Navigation.PopAsync();
    }
}