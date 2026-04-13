namespace REFTEST.Pages.PT.TesteMaster;

public partial class NotaMaster : ContentPage
{
    public NotaMaster()
    {
        InitializeComponent();
        lblTotal.Text = MasterControl.CalcularTotal().ToString() + "/100";
    }

    private async void OnFinalizar(object sender, EventArgs e) => await Navigation.PopToRootAsync();
}