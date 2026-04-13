namespace REFTEST.Pages.ING.Ajudas_para_testes;

public partial class TesteContarAJUDA : ContentPage
{
    public TesteContarAJUDA() => InitializeComponent();

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await DisplayAlertAsync("How to play",
            "1. Try to stop the stopwatch at the indicated target time.\n2. After 3 seconds, the numbers will disappear.\n3. Keep counting in your head and stop at the right second!",
            "Got it");
        await Navigation.PopAsync();
    }
}