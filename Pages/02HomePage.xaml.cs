using REFTEST.Pages.Ajudas_para_testes;

namespace REFTEST.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnTesteReacaoAJUDAClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteReacaoAJUDA());
        }

        private async void OnTesteVisaoAJUDAClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteVisaoAJUDA());
        }

        private async void OnTesteContarAJUDAClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContarAJUDA());
        }

        private async void OnTesteEscolherAJUDAClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteEscolherAJUDA());
        }

        private async void OnTesteCoresAJUDAClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteCoresAJUDA());
        }
    }
}