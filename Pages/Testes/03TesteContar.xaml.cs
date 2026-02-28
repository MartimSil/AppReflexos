using System.Diagnostics;

namespace REFTEST.Pages.Testes
{
    public partial class TesteContar : ContentPage
    {
        // 'readonly' resolve o aviso IDE0044. 
        // O uso de 'new()' simplificado resolve o aviso IDE0090.
        private readonly Stopwatch cronometro = new();
        private readonly IDispatcherTimer timer;

        // Variável que guardará o número alvo sorteado
        private int numeroAlvo;

        public TesteContar()
        {
            InitializeComponent();

            // Sorteia o número assim que a página abre
            SortearNovoNumero();

            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += (s, e) =>
            {
                lblTempo.Text = cronometro.Elapsed.ToString(@"mm\:ss\.ff");
            };
        }

        private void SortearNovoNumero()
        {
            Random random = new();
            // Gera um número aleatório entre 5 e 15
            numeroAlvo = random.Next(5, 16);

            // Atualiza o texto que aparece na imagem image_bbb839.png
            // Certifique-se que no XAML o Label tem x:Name="lblInstrucao"
            lblInstrucao.Text = $"Clique quando chegar o numero {numeroAlvo}";
        }

        private void OnIniciarPararClicked(object sender, EventArgs e)
        {
            if (!cronometro.IsRunning)
            {
                cronometro.Start();
                timer.Start();

                btnIniciarParar.Text = "Parar";
                btnIniciarParar.BackgroundColor = Colors.Red;
            }
            else
            {
                cronometro.Stop();
                timer.Stop();

                btnIniciarParar.Text = "Iniciar";
                btnIniciarParar.BackgroundColor = Colors.Green;

                // Chama a verificação do resultado
                _ = VerificarResultado();
            }
        }

        // 'async Task' e 'await DisplayAlert' resolvem o aviso CS0618
        private async Task VerificarResultado()
        {
            int segundosPassados = cronometro.Elapsed.Seconds;

            if (segundosPassados == numeroAlvo)
            {
                await DisplayAlertAsync("Boa!", $"Você parou exatamente em {segundosPassados}s!", "OK");
            }
            else
            {
                await DisplayAlertAsync("Quase!", $"O alvo era {numeroAlvo}s, mas você parou em {segundosPassados}s.", "Tentar de novo");
            }
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            cronometro.Reset();
            timer.Stop();
            lblTempo.Text = "00:00.00";

            btnIniciarParar.Text = "Iniciar";
            btnIniciarParar.BackgroundColor = Colors.Green;

            // Sorteia um novo número toda vez que resetar o teste
            SortearNovoNumero();
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}