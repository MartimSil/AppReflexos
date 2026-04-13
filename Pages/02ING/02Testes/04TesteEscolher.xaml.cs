using REFTEST.Pages.ING.Ajudas_para_testes;
using REFTEST.Pages.ING.Main;
using System.Text.Json;

namespace REFTEST.Pages.ING.Testes
{
    public class RegistoEscolher
    {
        public string Nome { get; set; } = string.Empty;
        public int Pontos { get; set; }
        public DateTime Data { get; set; }
    }

    public partial class TesteEscolher : ContentPage
    {
        private int erros = 0;
        private int pontos = 0;
        private readonly int limiteErros = 5;
        private Color corAlvo = Colors.Transparent;
        private readonly Random random = new();
        private readonly List<Button> botoes = [];
        private IDispatcherTimer? timer;
        private double tempoRestante = 1.5;

        private readonly Color[] coresPossiveis =
        [
            Colors.Red, Colors.Blue, Colors.Green, Colors.Yellow,
            Colors.Purple, Colors.Orange, Colors.Pink, Colors.Gray, Colors.Brown
        ];

        public TesteEscolher()
        {
            InitializeComponent();

            botoes.Add(btn0);
            botoes.Add(btn1);
            botoes.Add(btn2);
            botoes.Add(btn3);

            ConfigurarTimer();
        }

        private void OnStartClicked(object sender, EventArgs e)
        {
            OverlayStart.IsVisible = false;

            foreach (var btn in botoes)
                btn.IsEnabled = true;

            ProximaRonda();
        }

        private void ConfigurarTimer()
        {
            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (s, e) => {
                tempoRestante -= 0.1;
                lblTimer.Text = $"Time: {Math.Max(0, tempoRestante):F1}s"; // Traduzido

                if (tempoRestante <= 0)
                {
                    timer?.Stop();
                    ProcessarErro("Time's up!"); // Traduzido
                }
            };
        }

        private void ProximaRonda()
        {
            timer?.Stop();
            tempoRestante = 1.5;
            lblTimer.Text = "Time: 1.5s"; // Traduzido

            var listaBaralhada = coresPossiveis.OrderBy(c => random.Next()).ToList();
            for (int i = 0; i < botoes.Count; i++)
            {
                botoes[i].BackgroundColor = listaBaralhada[i];
            }

            int indiceAlvo = random.Next(botoes.Count);
            corAlvo = botoes[indiceAlvo].BackgroundColor;

            Color corDoTexto;
            do
            {
                corDoTexto = listaBaralhada[random.Next(botoes.Count)];
            } while (corDoTexto == corAlvo);

            lblPergunta.Text = TraduzirCor(corAlvo);
            lblPergunta.TextColor = corDoTexto;

            timer?.Start();
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            if (sender is not Button btnClicado) return;

            if (btnClicado.BackgroundColor == corAlvo)
            {
                pontos++;
                lblPontos.Text = $"Hits: {pontos}"; // Traduzido
                ProximaRonda();
            }
            else
            {
                ProcessarErro("Wrong color!"); // Traduzido
            }
        }

        private async void ProcessarErro(string motivo)
        {
            timer?.Stop();
            erros++;
            lblErros.Text = $"Errors: {erros} / {limiteErros}"; // Traduzido

            if (erros >= limiteErros)
            {
                bool querGuardar = await DisplayAlertAsync("End of Test", $"{motivo}\nYou scored {pontos} points!\nDo you want to save this result?", "Yes", "No"); // Traduzido

                if (querGuardar)
                {
                    string iniciais = await DisplayPromptAsync("Save", "Enter 3 letters for your name:", "OK", "Cancel", "ABC", 3); // Traduzido

                    if (!string.IsNullOrWhiteSpace(iniciais))
                    {
                        if (iniciais.Length > 3) iniciais = iniciais[..3];
                        iniciais = iniciais.ToUpper();

                        string jsonAntigo = Preferences.Default.Get("historico_escolha", "[]");
                        var historico = JsonSerializer.Deserialize<List<RegistoEscolher>>(jsonAntigo) ?? [];

                        historico.Add(new RegistoEscolher
                        {
                            Nome = iniciais,
                            Pontos = pontos,
                            Data = DateTime.Now
                        });

                        string jsonNovo = JsonSerializer.Serialize(historico);
                        Preferences.Default.Set("historico_escolha", jsonNovo);

                        await DisplayAlertAsync("Success", $"Result saved for {iniciais}!", "OK"); // Traduzido
                    }
                }

                erros = 0;
                pontos = 0;
                lblErros.Text = "Errors: 0 / 5"; // Traduzido
                lblPontos.Text = "Hits: 0"; // Traduzido

                OverlayStart.IsVisible = true;
                foreach (var btn in botoes) btn.IsEnabled = false;
                return;
            }

            ProximaRonda();
        }

        private static string TraduzirCor(Color c)
        {
            if (c == Colors.Red) return "RED";
            if (c == Colors.Blue) return "BLUE";
            if (c == Colors.Green) return "GREEN";
            if (c == Colors.Yellow) return "YELLOW";
            if (c == Colors.Purple) return "PURPLE";
            if (c == Colors.Orange) return "ORANGE";
            if (c == Colors.Pink) return "PINK";
            if (c == Colors.Gray) return "GRAY";
            if (c == Colors.Brown) return "BROWN";
            return "COLOR";
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            timer?.Stop();
            await Navigation.PushAsync(new HomePageING());
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            timer?.Stop();
            await Navigation.PushAsync(new TesteEscolherAJUDA());
        }
    }
}