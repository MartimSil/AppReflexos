using System.Diagnostics;
using REFTEST.Pages.PT.Main;
using REFTEST.Pages.PT.Ajudas_para_testes;

namespace REFTEST.Pages.PT.Testes
{
    public partial class TesteReacao : ContentPage
    {
        private enum EstadoJogo { Parado, SequenciaLuzes, CorridaAMeio }
        private EstadoJogo _estadoAtual = EstadoJogo.Parado;

        private readonly Stopwatch _cronometro = new();
        private readonly Random _random = new();
        private CancellationTokenSource? _cts;

        private readonly Color _corLuzApagada = Color.FromArgb("#444444");
        private readonly Color _corLuzAcesa = Colors.Red;

        public TesteReacao()
        {
            InitializeComponent();
        }

        private List<Border> ObterLuzes()
        {
            return [Luz1, Luz2, Luz3, Luz4, Luz5];
        }

        private async void OnActionButtonClicked(object sender, EventArgs e)
        {
            switch (_estadoAtual)
            {
                case EstadoJogo.Parado:
                    await IniciarSequenciaF1();
                    break;

                case EstadoJogo.SequenciaLuzes:
                    PartidaFalsa();
                    break;

                case EstadoJogo.CorridaAMeio:
                    PararCorrida();
                    break;
            }
        }

        private async Task IniciarSequenciaF1()
        {
            _estadoAtual = EstadoJogo.SequenciaLuzes;
            _cts = new CancellationTokenSource();

            ApagarTodasAsLuzes();
            lblResultado.Text = "Espera...";
            btnAcao.Text = "Não cliques!";
            btnAcao.BackgroundColor = Colors.DarkRed;

            var luzes = ObterLuzes();

            try
            {
                foreach (var luz in luzes)
                {
                    await Task.Delay(1000, _cts.Token);
                    luz.BackgroundColor = _corLuzAcesa;
                }

                int tempoAtrasoAleatorio = _random.Next(1000, 4000);
                await Task.Delay(tempoAtrasoAleatorio, _cts.Token);

                ApagarTodasAsLuzes();
                _estadoAtual = EstadoJogo.CorridaAMeio;

                lblResultado.Text = "RÁPIDO!";
                btnAcao.Text = "CLICA!";
                btnAcao.BackgroundColor = Colors.Green;

                _cronometro.Restart();
            }
            catch (TaskCanceledException)
            {
                // Jogo cancelado por queima de largada
            }
        }

        private void PararCorrida()
        {
            _cronometro.Stop();
            _estadoAtual = EstadoJogo.Parado;

            long tempoGasto = _cronometro.ElapsedMilliseconds;

            lblResultado.Text = $"{tempoGasto} ms";
            btnAcao.Text = "Recomeçar";
            btnAcao.BackgroundColor = Colors.DarkSlateBlue;
        }

        private void PartidaFalsa()
        {
            _cts?.Cancel();
            _estadoAtual = EstadoJogo.Parado;

            ApagarTodasAsLuzes();

            lblResultado.Text = "❌ QUEIMA!";
            btnAcao.Text = "Recomeçar";
            btnAcao.BackgroundColor = Colors.DarkSlateBlue;
        }

        private void ApagarTodasAsLuzes()
        {
            var luzes = ObterLuzes();
            foreach (var luz in luzes)
            {
                luz.BackgroundColor = _corLuzApagada;
            }
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            _cts?.Cancel();
            await Navigation.PushAsync(new HomePage());
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            _cts?.Cancel();
            await Navigation.PushAsync(new TesteReacaoAJUDA());
        }
    }
}