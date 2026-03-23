using System.Diagnostics;
using REFTEST.Pages.PT.Main;
using REFTEST.Pages.PT.Ajudas_para_testes;
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
                    await PararCorrida();
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

                int tempoAtrasoAleatorio = _random.Next(1500, 4000);
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
            }
        }

        private async Task PararCorrida()
        {
            _cronometro.Stop();
            _estadoAtual = EstadoJogo.Parado;

            long tempoGasto = _cronometro.ElapsedMilliseconds;

            lblResultado.Text = $"{tempoGasto} ms";
            btnAcao.Text = "Recomeçar";
            btnAcao.BackgroundColor = Colors.DarkSlateBlue;

            bool querGuardar = await DisplayAlertAsync("Fim do Teste", $"Fizeste {tempoGasto} ms!\nQueres guardar este resultado?", "Sim", "Não");

            if (querGuardar)
            {
                string iniciais = await DisplayPromptAsync("Guardar", "Introduz 3 letras para o nome:", "OK", "Cancelar", "ABC", 3);

                if (!string.IsNullOrWhiteSpace(iniciais))
                {
                    if (iniciais.Length > 3) iniciais = iniciais[..3];
                    iniciais = iniciais.ToUpper();

                    string jsonAntigo = Preferences.Default.Get("historico_reacao", "[]");
                    var historico = JsonSerializer.Deserialize<List<RegistoReacao>>(jsonAntigo) ?? [];

                    historico.Add(new RegistoReacao
                    {
                        Nome = iniciais,
                        Milissegundos = tempoGasto,
                        Data = DateTime.Now
                    });

                    string jsonNovo = JsonSerializer.Serialize(historico);
                    Preferences.Default.Set("historico_reacao", jsonNovo);

                    await DisplayAlertAsync("Sucesso", $"Resultado guardado para {iniciais}!", "OK");
                }
            }
        }

        private void PartidaFalsa()
        {
            _cts?.Cancel();
            _estadoAtual = EstadoJogo.Parado;

            ApagarTodasAsLuzes();

            lblResultado.Text = "❌ ANTECIPADO!";
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