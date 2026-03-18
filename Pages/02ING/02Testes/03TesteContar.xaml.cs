using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Ajudas_para_testes;
using REFTEST.Pages.ING.NotaTestes;

using System.Diagnostics;
namespace REFTEST.Pages.ING.Testes
{
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
            lblInstrucao.Text = $"Clique quando chegar aos {numeroAlvo} seg";
        }

        private async void OnIniciarResetClicked(object sender, EventArgs e)
        {
            if (!cronometro.IsRunning)
            {
                // Parar qualquer contagem anterior de esconder o texto
                _cts?.Cancel();
                _cts = new CancellationTokenSource();

                cronometro.Start();
                timer.Start();

                btnIniciarParar.Text = "Parar";
                btnIniciarParar.BackgroundColor = Colors.Red;

                // Inicia a tarefa de esconder e passa o token
                _ = EsconderCronometroAposTempo(3000, _cts.Token);
            }
            else
            {
                // Cancela o esconder assim que clicas em Parar
                _cts?.Cancel();

                timer.Stop();
                cronometro.Stop();

                // 1. Mostrar resultado
                await VerificarResultado();

                // 2. Reset total
                cronometro.Reset();
                lblTempo.Text = "00:00.00";
                lblTempo.Opacity = 1;
                lblTempo.IsVisible = true;

                btnIniciarParar.Text = "Iniciar";
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
            catch (OperationCanceledException)
            {
                //A Tarefa foi cancelada porque o user clicou em parar/reiniciar
            }
        }

        private async Task VerificarResultado()
        {
            double tempoFinal = cronometro.Elapsed.TotalSeconds;

            double diferenca = Math.Abs(tempoFinal - numeroAlvo);

            string titulo;
            string mensagem;

            if (diferenca <= 0.5)
            {
                titulo = "Espetacular!";
                mensagem = $"Parabéns! O alvo era {numeroAlvo}s e tu paraste aos {tempoFinal:F2}s.";
            }
            else if (diferenca <= 1.5)
            {
                titulo = "Quase!";
                mensagem = $"Estiveste muito perto! O alvo era {numeroAlvo}s e paraste aos {tempoFinal:F2}s.";
            }
            else
            {
                titulo = "Mais sorte na próxima...";
                mensagem = $"Ficaste longe do objetivo. O alvo era {numeroAlvo}s, mas paraste aos {tempoFinal:F2}s.";
            }

            await this.DisplayAlertAsync(titulo, mensagem, "Tentar novamente");
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HomePageING());
        }
        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContarAJUDA());
        }
        private async void OnGoToTesteContarNotaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TesteContarNota());
        }
    }
}