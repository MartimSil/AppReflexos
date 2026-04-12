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

        public TesteReacao()
        {
            InitializeComponent();
        }

        // Sintaxe [] corrigindo o aviso IDE0028
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
                // Liga as luzes uma a uma a cada 1 segundo
                foreach (var luz in luzes)
                {
                    await Task.Delay(1000, _cts.Token);
                    luz.BackgroundColor = _corLuzAcesa;
                }

                // Espera um tempo aleatório entre 1.5s e 4s com todas as luzes acesas
                int tempoAtrasoAleatorio = _random.Next(1500, 4000);
                await Task.Delay(tempoAtrasoAleatorio, _cts.Token);

                // Apaga tudo: É o sinal para clicar!
                ApagarTodasAsLuzes();
                _estadoAtual = EstadoJogo.CorridaAMeio;

                lblResultado.Text = "RÁPIDO!";
                btnAcao.Text = "CLICA AGORA!";
                btnAcao.BackgroundColor = Colors.Green;

                _cronometro.Restart();
            }
            catch (TaskCanceledException) { }
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
            bool querGuardar = await DisplayAlertAsync("Fim do Teste", $"Fizeste {tempo} ms!\nQueres guardar este resultado?", "Sim", "Não");

            if (querGuardar)
            {
                string iniciais = await DisplayPromptAsync("Guardar", "Introduz as tuas iniciais (3 letras):", "OK", "Cancelar", "ABC", 3);

                if (!string.IsNullOrWhiteSpace(iniciais))
                {
                    iniciais = iniciais.Length > 3 ? iniciais[..3].ToUpper() : iniciais.ToUpper();

                    string jsonAntigo = Preferences.Get("historico_reacao", "[]");
                    var historico = JsonSerializer.Deserialize<List<RegistoReacao>>(jsonAntigo) ?? [];

                    historico.Add(new RegistoReacao
                    {
                        Nome = iniciais,
                        Milissegundos = tempo,
                        Data = DateTime.Now
                    });

                    Preferences.Set("historico_reacao", JsonSerializer.Serialize(historico));
                    await DisplayAlertAsync("Sucesso", "Resultado guardado!", "OK");
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
            foreach (var luz in ObterLuzes())
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
            await DisplayAlertAsync("Como jogar", "1. Clica em Iniciar.\n2. Espera que as 5 luzes fiquem vermelhas.\n3. Quando as luzes APAGAREM, clica o mais rápido que conseguires!", "Entendido");
        }
    }
}