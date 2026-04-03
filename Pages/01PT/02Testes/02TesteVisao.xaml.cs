namespace REFTEST.Pages.PT.Testes;
using REFTEST.Pages.PT.Ajudas_para_testes;
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

        string[] lista = ["VISAO", "OLHAR", "FOCO", "TESTE", "SAUDE", "LENTE", "OTICA", "BRILHO", "VISTA", "CLARO"];
        _palavraOriginal = lista[_random.Next(lista.Length)];
        char[] letras = _palavraOriginal.ToCharArray();

        if (_random.Next(0, 2) == 0)
        {
            int idx = _random.Next(letras.Length);
            letras[idx] = (char)_random.Next('A', 'Z' + 1);
        }
        _palavraExibida = new string(letras);

        PalavraLabel.Text = _palavraExibida;
        await Task.Delay(3000);

        PalavraLabel.Text = "??????";
        LayoutBotoes.IsVisible = true;
    }

    private void OnSimClicked(object sender, EventArgs e) => VerificarResposta(true);
    private void OnNaoClicked(object sender, EventArgs e) => VerificarResposta(false);

    private void VerificarResposta(bool disseQueEstaCerta)
    {
        _contadorPalavras++;
        bool estavaRealmenteCerta = (_palavraExibida == _palavraOriginal);

        if (disseQueEstaCerta == estavaRealmenteCerta)
        {
            _pontos++;
            FeedbackLabel.Text = "ACERTOU!";
            FeedbackLabel.TextColor = Colors.Green;
        }
        else
        {
            FeedbackLabel.Text = $"ERROU! A original era {_palavraOriginal}";
            FeedbackLabel.TextColor = Colors.Red;
        }

        AtualizarInterface();
    }

    private void AtualizarInterface()
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
            FeedbackLabel.Text = $"Teste Terminado! Pontuação Final: {_pontos} de {TotalTestes}";
            BtnProxima.Text = "Reiniciar Teste";
            BtnProxima.IsVisible = true;
        }
    }

    private async void OnGoToHomeClicked(object sender, EventArgs e) => await Navigation.PopAsync();
    private async void OnHelpButtonClicked(object sender, EventArgs e) => await Navigation.PushAsync(new TesteVisaoAJUDA());
}