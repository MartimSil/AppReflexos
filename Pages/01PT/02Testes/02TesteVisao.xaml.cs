using System.Text.Json;
using REFTEST.Pages.PT.Main;
using REFTEST.Pages.PT.Ajudas_para_testes;

namespace REFTEST.Pages.PT.Testes
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

            string[] lista = [
                "SOL", "LUA", "VER", "DIA", "LUZ", "MAR", "CEU", "FOCO", "OLHO", "MAO",
                "VISAO", "OLHAR", "TESTE", "SAUDE", "LENTE", "OTICA", "VISTA", "CLARO",
                "CORES", "PONTO", "FORMA", "LONGE", "PERTO", "BRILHO", "ALVO", "SINAL",
                "MUNDO", "CAMPO", "ALMA", "MENTE", "AZUL", "VERDE", "REDE", "VIDA"
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
                FeedbackLabel.Text = "CORRETO!";
                FeedbackLabel.TextColor = Colors.Green;
            }
            else
            {
                FeedbackLabel.Text = $"ERRADO! Era: {_palavraOriginal}";
                FeedbackLabel.TextColor = Colors.Red;
            }

            await AtualizarInterface();
        }

        private async Task AtualizarInterface()
        {
            PontuacaoLabel.Text = $"Pontos: {_pontos} | Palavra: {_contadorPalavras}/{TotalTestes}";
            LayoutBotoes.IsVisible = false;

            if (_contadorPalavras < TotalTestes)
            {
                BtnProxima.Text = "Próxima Palavra";
                BtnProxima.IsVisible = true;
            }
            else
            {
                PalavraLabel.Text = "FIM!";
                FeedbackLabel.Text = $"Teste Concluído! Pontuação: {_pontos}/{TotalTestes}";

                // LÓGICA DE GUARDAR COPIADA DO TEU EXEMPLO
                bool querGuardar = await DisplayAlertAsync("Fim do Teste", $"Fizeste {_pontos} pontos!\nQueres guardar este resultado?", "Sim", "Não");

                if (querGuardar)
                {
                    string iniciais = await DisplayPromptAsync("Guardar", "Introduz 3 letras para o nome:", "OK", "Cancelar", "ABC", 3);

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

                        await DisplayAlertAsync("Sucesso", $"Resultado guardado para {iniciais}!", "OK");
                    }
                }

                BtnProxima.Text = "Reiniciar Teste";
                BtnProxima.IsVisible = true;
            }
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e) => await Navigation.PopAsync();

        private async void OnHelpButtonClicked(object sender, EventArgs e) =>
            await Navigation.PushAsync(new TesteVisaoAJUDA());
    }
}