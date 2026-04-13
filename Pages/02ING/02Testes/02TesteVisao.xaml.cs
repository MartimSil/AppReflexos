using System.Text.Json;
using REFTEST.Pages.ING.Main;
using REFTEST.Pages.ING.Ajudas_para_testes;

namespace REFTEST.Pages.ING.Testes
{
    public class RegistoVisao
    {
        public string Nome { get; set; } = string.Empty;
        public int Pontos { get; set; }
        public DateTime Data { get; set; }
    }

    public partial class TesteVisao : ContentPage
    {
        private readonly Random _random = new();
        private string _palavraOriginal = "";
        private string _palavraExibida = "";
        private int _pontos = 0;
        private int _contadorPalavras = 0;
        private const int TotalTestes = 10;

        public TesteVisao()
        {
            InitializeComponent();
        }

        private async void OnGerarPalavraClicked(object sender, EventArgs e)
        {
            if (_contadorPalavras >= TotalTestes)
            {
                _pontos = 0;
                _contadorPalavras = 0;
            }

            BtnProxima.IsVisible = false;
            LayoutBotoes.IsVisible = false;
            FeedbackLabel.Text = "";

            // Lista de palavras traduzida para Inglês
            string[] lista = [
                "SUN", "MOON", "SEE", "DAY", "LIGHT", "SEA", "SKY", "FOCUS", "EYE", "HAND",
                "VISION", "LOOK", "TEST", "HEALTH", "LENS", "OPTIC", "VIEW", "CLEAR",
                "COLORS", "POINT", "SHAPE", "FAR", "NEAR", "GLARE", "TARGET", "SIGNAL",
                "WORLD", "FIELD", "SOUL", "MIND", "BLUE", "GREEN", "NET", "LIFE"
            ];

            _palavraOriginal = lista[_random.Next(lista.Length)];
            char[] letras = _palavraOriginal.ToCharArray();

            if (_random.NextDouble() < 0.4)
            {
                int idx = _random.Next(letras.Length);
                char letraOriginal = letras[idx];
                char novaLetra;
                do { novaLetra = (char)_random.Next('A', 'Z' + 1); } while (novaLetra == letraOriginal);
                letras[idx] = novaLetra;
            }
            _palavraExibida = new string(letras);

            double posX = 0.1 + (_random.NextDouble() * 0.8);
            double posY = 0.1 + (_random.NextDouble() * 0.4);

            AbsoluteLayout.SetLayoutBounds(PalavraLabel, new Rect(posX, posY, 300, 100));
            PalavraLabel.Text = _palavraExibida;

            await Task.Delay(1500);

            AbsoluteLayout.SetLayoutBounds(PalavraLabel, new Rect(0.5, 0.4, 300, 100));
            PalavraLabel.Text = "??????";
            LayoutBotoes.IsVisible = true;
        }

        private void OnSimClicked(object sender, EventArgs e) => VerificarResposta(true);
        private void OnNaoClicked(object sender, EventArgs e) => VerificarResposta(false);

        private async void VerificarResposta(bool disseQueEstaCerta)
        {
            _contadorPalavras++;
            bool estavaRealmenteCerta = (_palavraExibida == _palavraOriginal);

            if (disseQueEstaCerta == estavaRealmenteCerta)
            {
                _pontos++;
                FeedbackLabel.Text = "CORRECT!"; // Traduzido
                FeedbackLabel.TextColor = Colors.Green;
            }
            else
            {
                FeedbackLabel.Text = $"WRONG! It was: {_palavraOriginal}"; // Traduzido
                FeedbackLabel.TextColor = Colors.Red;
            }

            await AtualizarInterface();
        }

        private async Task AtualizarInterface()
        {
            PontuacaoLabel.Text = $"Score: {_pontos} | Word: {_contadorPalavras}/{TotalTestes}"; // Traduzido
            LayoutBotoes.IsVisible = false;

            if (_contadorPalavras < TotalTestes)
            {
                BtnProxima.Text = "Next Word"; // Traduzido
                BtnProxima.IsVisible = true;
            }
            else
            {
                PalavraLabel.Text = "FINISH!"; // Traduzido
                FeedbackLabel.Text = $"Test Complete! Score: {_pontos}/{TotalTestes}"; // Traduzido

                bool querGuardar = await DisplayAlertAsync("End of Test", $"You scored {_pontos} points!\nDo you want to save this result?", "Yes", "No"); // Traduzido

                if (querGuardar)
                {
                    string iniciais = await DisplayPromptAsync("Save", "Enter 3 letters for your name:", "OK", "Cancel", "ABC", 3); // Traduzido

                    if (!string.IsNullOrWhiteSpace(iniciais))
                    {
                        if (iniciais.Length > 3) iniciais = iniciais[..3];
                        iniciais = iniciais.ToUpper();

                        string jsonAntigo = Preferences.Default.Get("historico_visao", "[]");
                        var historico = JsonSerializer.Deserialize<List<RegistoVisao>>(jsonAntigo) ?? [];

                        historico.Add(new RegistoVisao
                        {
                            Nome = iniciais,
                            Pontos = _pontos,
                            Data = DateTime.Now
                        });

                        string jsonNovo = JsonSerializer.Serialize(historico);
                        Preferences.Default.Set("historico_visao", jsonNovo);

                        await DisplayAlertAsync("Success", $"Result saved for {iniciais}!", "OK"); // Traduzido
                    }
                }

                BtnProxima.Text = "Restart Test"; // Traduzido
                BtnProxima.IsVisible = true;
            }
        }

        private async void OnGoToHomePageINGClicked(object sender, EventArgs e) => await Navigation.PopAsync();

        private async void OnHelpButtonClicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new TesteVisaoAJUDA());
    }
}