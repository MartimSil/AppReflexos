namespace REFTEST.Pages.PT.TesteMaster;

public partial class TesteVisaoMaster : ContentPage
{
    private string _original = "", _exibida = "";
    private int _pontos = 0, _contador = 0;
    private readonly string[] _lista = { "SOL", "LUA", "VER", "MAR", "FOCO", "VISAO", "SAUDE", "CLARO" };

    public TesteVisaoMaster() { InitializeComponent(); }

    private async void OnGerarPalavraClicked(object sender, EventArgs e)
    {
        BtnProxima.IsVisible = false;
        LayoutBotoes.IsVisible = false;
        _original = _lista[new Random().Next(_lista.Length)];
        _exibida = (new Random().NextDouble() < 0.4) ? "ERRO" : _original;
        PalavraLabel.Text = _exibida;
        await Task.Delay(1500);
        PalavraLabel.Text = "????";
        LayoutBotoes.IsVisible = true;
    }

    private void OnSimClicked(object sender, EventArgs e) => Validar(_exibida == _original);
    private void OnNaoClicked(object sender, EventArgs e) => Validar(_exibida != _original);

    private async void Validar(bool correta)
    {
        if (correta) _pontos++;
        _contador++;
        if (_contador >= 10)
        {
            MasterControl.Scores[1] = _pontos * 2;
            PalavraLabel.Text = "FIM";
            LayoutBotoes.IsVisible = false;
            btnProximoMaster.IsVisible = true;
        }
        else OnGerarPalavraClicked(null, null);
    }

    private async void OnProximoClicked(object sender, EventArgs e) => await Navigation.PushAsync(new TesteContarMaster());
}