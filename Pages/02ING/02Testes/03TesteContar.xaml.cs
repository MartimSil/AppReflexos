using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Ajudas_para_testes;
using System.Diagnostics;
using System.Text.Json;

namespace REFTEST.Pages.ING.Testes
{
    public class RegistoTempo
    {
        public string Nome { get; set; } = string.Empty;
        public int Alvo { get; set; }
        public double TempoAtingido { get; set; }
        public DateTime Data { get; set; }

        public string CorResultado
        {
            get
            {
                double diferenca = Math.Abs(TempoAtingido - Alvo);
                if (diferenca <= 0.3) return "#22C55E";
                if (diferenca <= 1.0) return "#EAB308";
                return "#EF4444";
            }
        }
    }

    public partial class TesteContar : ContentPage
    {
        private readonly Stopwatch cronometro = new();
        private readonly IDispatcherTimer timer;
        private int numeroAlvo;
        private CancellationTokenSource? _cts;

        public TesteContar()
        {
            InitializeComponent();
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
            numeroAlvo = random.Next(5, 16);
            lblInstrucao.Text = $"Click when it reaches {numeroAlvo} sec"; // Traduzido
        }

        private async void OnIniciarResetClicked(object sender, EventArgs e)
        {
            if (!cronometro.IsRunning)
            {
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                cronometro.Start();
                timer.Start();

                btnIniciarParar.Text = "Stop"; // Traduzido
                btnIniciarParar.BackgroundColor = Colors.Red;

                _ = EsconderCronometroAposTempo(3000, _cts.Token);
            }
            else
            {
                _cts?.Cancel();
                timer.Stop();
                cronometro.Stop();

                await VerificarEGuardarResultado();

                cronometro.Reset();
                lblTempo.Text = "00:00.00";
                lblTempo.Opacity = 1;
                lblTempo.IsVisible = true;

                btnIniciarParar.Text = "Start"; // Traduzido
                btnIniciarParar.BackgroundColor = Colors.Green;

                SortearNovoNumero();
            }
        }

        private async Task EsconderCronometroAposTempo(int milissegundos, CancellationToken token)
        {
            try
            {
                await Task.Delay(milissegundos, token);

                if (cronometro.IsRunning && !token.IsCancellationRequested)
                {
                    await lblTempo.FadeToAsync(0, 500);
                    lblTempo.IsVisible = false;
                }
            }
            catch (OperationCanceledException) { }
        }

        private async Task VerificarEGuardarResultado()
        {
            double tempoFinal = cronometro.Elapsed.TotalSeconds;
            double diferenca = Math.Abs(tempoFinal - numeroAlvo);

            // Títulos traduzidos
            string titulo = diferenca <= 0.5 ? "Amazing!" :
                           (diferenca <= 1.5 ? "Close!" : "Better luck next time...");

            // Mensagem do alerta traduzida
            bool querGuardar = await DisplayAlertAsync(titulo, $"The target was {numeroAlvo}s and you stopped at {tempoFinal:F2}s.\n\nDo you want to save this result?", "Yes", "No");

            if (querGuardar)
            {
                // Prompts traduzidos
                string iniciais = await DisplayPromptAsync("Save", "Enter 3 letters for your name:", "OK", "Cancel", "ABC", 3);

                if (!string.IsNullOrWhiteSpace(iniciais))
                {
                    if (iniciais.Length > 3) iniciais = iniciais[..3];
                    iniciais = iniciais.ToUpper();

                    // Mantive a chave "historico_salvo" para os rankings ficarem juntos
                    string jsonAntigo = Preferences.Default.Get("historico_salvo", "[]");
                    var historico = JsonSerializer.Deserialize<List<RegistoTempo>>(jsonAntigo) ?? [];

                    historico.Add(new RegistoTempo
                    {
                        Nome = iniciais,
                        Alvo = numeroAlvo,
                        TempoAtingido = Math.Round(tempoFinal, 2),
                        Data = DateTime.Now
                    });

                    string jsonNovo = JsonSerializer.Serialize(historico);
                    Preferences.Default.Set("historico_salvo", jsonNovo);

                    await DisplayAlertAsync("Success", $"Result saved for {iniciais}!", "OK"); // Traduzido
                }
            }
        }

        private async void OnGoToHomePageINGClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContarAJUDA());
        }
    }
}