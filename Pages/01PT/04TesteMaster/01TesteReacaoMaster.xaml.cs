using REFTEST.Pages.PT.Main;
using System.Diagnostics;
using System.Text.Json;

namespace REFTEST.Pages.PT.TesteMaster
{
    public class RegistoReacao { public string Nome { get; set; } = string.Empty; public long Milissegundos { get; set; } public DateTime Data { get; set; } }

    public partial class TesteReacao : ContentPage
    {
        private enum EstadoJogo { Parado, SequenciaLuzes, CorridaAMeio }
        private EstadoJogo _estadoAtual = EstadoJogo.Parado;
        private readonly Stopwatch _cronometro = new();
        private readonly Random _random = new();
        private CancellationTokenSource? _cts;
        private readonly Color _corLuzApagada = Color.FromArgb("#444444");

        // MASTER MODE
        private bool _isMasterMode = false;
        private int _pontosAcumulados = 0;

        public TesteReacao(bool isMaster = false, int pontosAtuais = 0)
        {
            InitializeComponent();
            _isMasterMode = isMaster;
            _pontosAcumulados = pontosAtuais;
        }

        private List<Border> ObterLuzes() => [Luz1, Luz2, Luz3, Luz4, Luz5];

        private async void OnActionButtonClicked(object sender, EventArgs e)
        {
            if (_estadoAtual == EstadoJogo.Parado) await IniciarSequenciaF1();
            else if (_estadoAtual == EstadoJogo.SequenciaLuzes) PartidaFalsa();
            else if (_estadoAtual == EstadoJogo.CorridaAMeio) await Finalizar();
        }

        private async Task IniciarSequenciaF1()
        {
            _estadoAtual = EstadoJogo.SequenciaLuzes;
            _cts = new CancellationTokenSource();
            btnAcao.Text = "ESPERA...";
            var luzes = ObterLuzes();
            foreach (var luz in luzes) { await Task.Delay(1000, _cts.Token); luz.BackgroundColor = Colors.Red; }
            await Task.Delay(_random.Next(500, 3000), _cts.Token);
            foreach (var luz in luzes) luz.BackgroundColor = _corLuzApagada;
            _estadoAtual = EstadoJogo.CorridaAMeio;
            _cronometro.Restart();
            btnAcao.Text = "AGORA!";
        }

        private async Task Finalizar()
        {
            _cronometro.Stop();
            long tempo = _cronometro.ElapsedMilliseconds;
            if (_isMasterMode)
            {
                int pts = tempo < 300 ? 20 : (tempo < 450 ? 15 : 10);
                await Navigation.PushAsync(new TesteVisao(true, pts));
            }
            else { /* Lógica normal de guardar que já tinhas... */ }
        }

        private void PartidaFalsa() { _cts?.Cancel(); _estadoAtual = EstadoJogo.Parado; btnAcao.Text = "Recomeçar"; }
        private async void OnGoToHomeClicked(object sender, EventArgs e) => await Navigation.PushAsync(new HomePage());
    }
}