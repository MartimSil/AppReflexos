using System;
using Microsoft.Maui.Controls;
using REFTEST.Pages.ING.Main;
using System.Text.Json;

namespace REFTEST.Pages.ING.Testes
{
    public class RegistoCores
    {
        public string Nome { get; set; } = string.Empty;
        public int Regras { get; set; }
        public DateTime Data { get; set; }
    }

    public partial class TesteCores : ContentPage
    {
        private bool _estaAcorrer = false;
        private int _regrasAtivas = 1;
        private int _ciclos = 0;
        private bool _clicouNesteTurno = false;
        private bool _objetoEhValido = false;
        private CancellationTokenSource? _cts;
        private readonly Random _random = new();

        private readonly string[] descricoesRegras =
        [
            "Click if it is RED",
            "Click if it is a CIRCLE",
            "Click if it is a DIAMOND (45°)",
            "Click if it is BLUE",
            "Click if it has a THICK BORDER",
            "Click if it is GREEN",
            "Click if it is a SQUARE",
            "Click if it is YELLOW",
            "Click if it has a THIN BORDER",
            "Click if it is PINK"
        ];

        public TesteCores()
        {
            InitializeComponent();
        }

        private async void OnActionButtonClicked(object sender, EventArgs e)
        {
            if (!_estaAcorrer) await IniciarJogo();
        }

        private async Task IniciarJogo()
        {
            _estaAcorrer = true;
            _regrasAtivas = 1;
            _ciclos = 0;

            lblFeedback.Text = "";
            btnAcao.IsEnabled = false;
            btnAcao.Opacity = 0.5;
            lblDescricaoRegra.Text = descricoesRegras[0];

            _cts = new CancellationTokenSource();

            try
            {
                while (_estaAcorrer)
                {
                    GerarNovoObjeto();
                    await Task.Delay(1500, _cts.Token);

                    if (_objetoEhValido && !_clicouNesteTurno)
                    {
                        FinalizarJogo("LOST: You let it pass!");
                        break;
                    }

                    _ciclos++;
                    if (_ciclos % 4 == 0 && _regrasAtivas < 10)
                    {
                        _regrasAtivas++;
                        lblDescricaoRegra.Text = descricoesRegras[_regrasAtivas - 1];
                    }

                    lblRegrasAtivas.Text = $"Rules: {_regrasAtivas}/10";
                }
            }
            catch (TaskCanceledException) { }
        }

        private void GerarNovoObjeto()
        {
            _clicouNesteTurno = false;
            lblFeedback.Text = "";

            Color[] cores = [Colors.Red, Colors.Blue, Colors.Green, Colors.Yellow, Colors.Pink, Colors.White, Colors.Gray];
            Color corSorteada = cores[_random.Next(cores.Length)];

            bool eCirculo = _random.Next(2) == 0;
            bool eLosango = _random.Next(2) == 0;
            double espessura = _random.Next(3) switch { 0 => 15, 1 => 5, _ => 1 };

            AlvoVisual.BackgroundColor = corSorteada;
            FormaAlvo.CornerRadius = eCirculo ? 100 : 0;
            AlvoVisual.StrokeThickness = espessura;
            AlvoVisual.Rotation = eLosango ? 45 : 0;

            _objetoEhValido = ValidarRegras(corSorteada, eCirculo, eLosango, espessura);
        }

        private bool ValidarRegras(Color cor, bool circulo, bool losango, double borda)
        {
            bool[] regras =
            [
                cor == Colors.Red,
                circulo,
                losango,
                cor == Colors.Blue,
                borda == 15,
                cor == Colors.Green,
                !circulo && !losango && cor != Colors.Red,
                cor == Colors.Yellow,
                borda == 5,
                cor == Colors.Pink
            ];

            for (int i = 0; i < _regrasAtivas; i++)
            {
                if (regras[i]) return true;
            }
            return false;
        }

        private void OnAlvoTapped(object sender, EventArgs e)
        {
            if (!_estaAcorrer || _clicouNesteTurno) return;

            if (_objetoEhValido)
            {
                _clicouNesteTurno = true;
                lblFeedback.Text = "GOOD!";
                lblFeedback.TextColor = Colors.Green;
            }
            else
            {
                FinalizarJogo("LOST: Wrong Click!");
            }
        }

        private async void FinalizarJogo(string motivo)
        {
            _estaAcorrer = false;
            _cts?.Cancel();

            lblFeedback.Text = motivo;
            lblFeedback.TextColor = Colors.Red;
            lblDescricaoRegra.Text = "";

            bool querGuardar = await DisplayAlertAsync("End of Test",
                $"{motivo}\nRules reached: {_regrasAtivas}\n\nDo you want to save this result?", "Yes", "No");

            if (querGuardar)
            {
                string iniciais = await DisplayPromptAsync("Save", "Enter 3 letters for your name:", "OK", "Cancel", "ABC", 3);

                if (!string.IsNullOrWhiteSpace(iniciais))
                {
                    if (iniciais.Length > 3) iniciais = iniciais[..3];
                    iniciais = iniciais.ToUpper();

                    string jsonAntigo = Preferences.Default.Get("historico_cores", "[]");
                    var historico = JsonSerializer.Deserialize<List<RegistoCores>>(jsonAntigo) ?? new();

                    historico.Add(new RegistoCores
                    {
                        Nome = iniciais,
                        Regras = _regrasAtivas,
                        Data = DateTime.Now
                    });

                    string jsonNovo = JsonSerializer.Serialize(historico);
                    Preferences.Default.Set("historico_cores", jsonNovo);

                    await DisplayAlertAsync("Success", $"Result saved for {iniciais}!", "OK");
                }
            }

            btnAcao.IsEnabled = true;
            btnAcao.Opacity = 1;
            btnAcao.Text = "Restart";
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            _cts?.Cancel();
            await Navigation.PopAsync();
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlertAsync("Rules", "Click the object if it fulfills ANY of the active rules!", "OK");
        }
    }
}