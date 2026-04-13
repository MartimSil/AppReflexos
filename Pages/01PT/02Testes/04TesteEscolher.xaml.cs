using REFTEST.Pages.PT.Ajudas_para_testes;
using REFTEST.Pages.PT.Main;
using System.Text.Json;

namespace REFTEST.Pages.PT.Testes
{
    // Modelo para guardar os dados exatamente como nos outros testes
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
                lblTimer.Text = $"Tempo: {Math.Max(0, tempoRestante):F1}s";

                if (tempoRestante <= 0)
                {
                    timer?.Stop();
                    ProcessarErro("Tempo esgotado!");
                }
            };
        }

        private void ProximaRonda()
        {
            timer?.Stop();
            tempoRestante = 1.5;
            lblTimer.Text = "Tempo: 1.5s";

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
                lblPontos.Text = $"Acertos: {pontos}";
                ProximaRonda();
            }
            else
            {
                ProcessarErro("Cor errada!");
            }
        }

        private async void ProcessarErro(string motivo)
        {
            timer?.Stop();
            erros++;
            lblErros.Text = $"Erros: {erros} / {limiteErros}";

            if (erros >= limiteErros)
            {
                // Lógica de guardar igual aos outros códigos
                bool querGuardar = await DisplayAlertAsync("Fim do Teste", $"{motivo}\nFizeste {pontos} pontos!\nQueres guardar este resultado?", "Sim", "Não");

                if (querGuardar)
                {
                    string iniciais = await DisplayPromptAsync("Guardar", "Introduz 3 letras para o nome:", "OK", "Cancelar", "ABC", 3);

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

                        await DisplayAlertAsync("Sucesso", $"Resultado guardado para {iniciais}!", "OK");
                    }
                }

                // Reiniciar o jogo
                erros = 0;
                pontos = 0;
                lblErros.Text = "Erros: 0 / 5";
                lblPontos.Text = "Acertos: 0";

                OverlayStart.IsVisible = true;
                foreach (var btn in botoes) btn.IsEnabled = false;
                return;
            }

            ProximaRonda();
        }

        private static string TraduzirCor(Color c)
        {
            if (c == Colors.Red) return "VERMELHO";
            if (c == Colors.Blue) return "AZUL";
            if (c == Colors.Green) return "VERDE";
            if (c == Colors.Yellow) return "AMARELO";
            if (c == Colors.Purple) return "ROXO";
            if (c == Colors.Orange) return "LARANJA";
            if (c == Colors.Pink) return "ROSA";
            if (c == Colors.Gray) return "CINZENTO";
            if (c == Colors.Brown) return "CASTANHO";
            return "COR";
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            timer?.Stop();
            await Navigation.PushAsync(new HomePage());
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            timer?.Stop();
            await Navigation.PushAsync(new TesteEscolherAJUDA());
        }
    }
}