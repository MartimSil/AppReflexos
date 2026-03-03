using REFTEST.Pages.Testes;

namespace REFTEST.Pages.Ajudas_para_testes
{
    public partial class TesteReacaoAJUDA : ContentPage
    {
        public TesteReacaoAJUDA()
        {
            InitializeComponent();
        }

        // Esta é a função que estava a faltar e causava o erro XC0002
        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Volta para a página anterior
        }
    }
}