namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteCoresAJUDA : ContentPage
{
    public TesteCoresAJUDA()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Popup translated to English
        await DisplayAlertAsync("How to play",
            "1. The game adds new rules every 4 turns.\n" +
            "2. Read the active rule at the top of the screen.\n" +
            "3. Click the object only if it fulfills the rule!",
            "Got it");

        // Automatically returns to the test after clicking "Got it"
        await Navigation.PopAsync();
    }
}