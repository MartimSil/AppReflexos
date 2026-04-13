namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteVisaoAJUDA : ContentPage
{
    public TesteVisaoAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("How to play",
            "1. A word will appear quickly on the screen.\n2. Try to memorize if it is written correctly.\n3. Answer YES or NO after the word disappears.",
            "Got it");
        await Navigation.PopAsync();
    }
}