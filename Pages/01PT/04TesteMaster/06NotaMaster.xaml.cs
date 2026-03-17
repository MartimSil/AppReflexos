using REFTEST.Pages.PT.Main;

namespace REFTEST.Pages.PT.TesteMaster
{
    public partial class NotaMaster : ContentPage
    {
        public NotaMaster()
        {
            InitializeComponent();
        }
        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}

