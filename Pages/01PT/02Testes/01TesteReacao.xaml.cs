using REFTEST.Pages.PT.Main;
using System.Diagnostics;
using System.Text.Json;

namespace REFTEST.Pages.PT.Testes
{
    public class RegistoReacao
    {
        public string Nome { get; set; } = string.Empty;
        public long Milissegundos { get; set; }
        public DateTime Data { get; set; }
    }

    public partial class TesteReacao : ContentPage
    {
        private enum EstadoJogo { Parado, SequenciaLuzes, CorridaAMeio }
        private EstadoJogo _estadoAtual = EstadoJogo.Parado;

        private readonly Stopwatch _cronometro = new();
        private readonly Random _random = new();
        private CancellationTokenSource? _cts;

        private readonly Color _corLuzApagada = Color.FromArgb("#444444");
        private readonly Color _corLuzAcesa = Colors.Red;

        // CONSTRUTOR: Sem isso, a tela fica branca!
        public TesteReacao()
        {
            InitializeComponent();
        }

        private List<Border> ObterLuzes() => [Luz1, Luz2, Luz3, Luz4, Luz5];

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
                    FinalizarCorrida();
                    break;
            }
        }

        private async Task IniciarSequenciaF1()
        {
            _estadoAtual = EstadoJogo.SequenciaLuzes;
            _cts = new CancellationTokenSource();

            ApagarTodasAsLuzes();
            lblResultado.Text = "Espera...";
            btnAcao.Text = "NÃO CLIQUES!";
            btnAcao.BackgroundColor = Colors.DarkRed;

            var luzes = ObterLuzes();

            try
            {
                foreach (var luz in luzes)
                {
                    await Task.Delay(1000, _cts.Token);
                    luz.BackgroundColor = _corLuzAcesa;
                }

                // Tempo aleatório antes das luzes apagarem (estilo F1)
                await Task.Delay(_random.Next(1500, 4000), _cts.Token);

                ApagarTodasAsLuzes();
                _estadoAtual = EstadoJogo.CorridaAMeio;

                lblResultado.Text = "RÁPIDO!";
                btnAcao.Text = "CLICA AGORA!";
                btnAcao.BackgroundColor = Colors.Green;

                _cronometro.Restart();
            }
            catch (TaskCanceledException)
            {
                // Task cancelada propositalmente na partida falsa
            }
        }

        private void FinalizarCorrida()
        {
            _cronometro.Stop();
            _estadoAtual = EstadoJogo.Parado;

            long tempoGasto = _cronometro.ElapsedMilliseconds;

            lblResultado.Text = $"{tempoGasto} ms";
            btnAcao.Text = "Recomeçar";
            btnAcao.BackgroundColor = Colors.DarkSlateBlue;

            GuardarResultado(tempoGasto);
        }

        private async void GuardarResultado(long tempo)
        {
            bool querGuardar = await DisplayAlertAsync("Fim do Teste", $"Fizeste {tempo} ms!\nQueres guardar?", "Sim", "Não");
            if (querGuardar)
            {
                string iniciais = await DisplayPromptAsync("Guardar", "Iniciais (3 letras):", "OK", "Cancelar", "ABC", 3);
                if (!string.IsNullOrWhiteSpace(iniciais))
                {
                    iniciais = iniciais.ToUpper();
                    var jsonAntigo = Preferences.Get("historico_reacao", "[]");
                    var historico = JsonSerializer.Deserialize<List<RegistoReacao>>(jsonAntigo) ?? new List<RegistoReacao>();

                    historico.Add(new RegistoReacao { Nome = iniciais, Milissegundos = tempo, Data = DateTime.Now });

                    Preferences.Set("historico_reacao", JsonSerializer.Serialize(historico));
                }
            }
        }

        private void PartidaFalsa()
        {
            _cts?.Cancel();
            _estadoAtual = EstadoJogo.Parado;
            ApagarTodasAsLuzes();
            lblResultado.Text = "ANTECIPADO!";
            btnAcao.Text = "Recomeçar";
            btnAcao.BackgroundColor = Colors.DarkSlateBlue;
        }

        private void ApagarTodasAsLuzes()
        {
            var luzes = ObterLuzes();
            foreach (var luz in luzes)
            {
                if (luz != null) luz.BackgroundColor = _corLuzApagada;
            }
        }

        private async void OnGoToHomePageClicked(object sender, EventArgs e)
        {
            _cts?.Cancel();
            // Certifique-se de que a HomePage existe nesta namespace
            await Navigation.PopAsync();
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            _cts?.Cancel();
            await DisplayAlertAsync("Ajuda", "Clica no botão central o mais rápido possível assim que as 5 luzes vermelhas se apagarem!", "OK");
        }
    }
}