using System;
using Microsoft.Maui.Controls;
using REFTEST.Pages.PT.Main;

namespace REFTEST.Pages.PT.Testes
{
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
            "Clica se for VERMELHO",
            "Clica se for CÍRCULO",
            "Clica se for LOSANGO (45°)",
            "Clica se for AZUL",
            "Clica se tiver BORDA ESPESSA",
            "Clica se for VERDE",
            "Clica se for QUADRADO",
            "Clica se for AMARELO",
            "Clica se tiver BORDA FINA",
            "Clica se for ROSA"
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
                        FinalizarJogo("PERDESTE: Deixaste passar!");
                        break;
                    }

                    _ciclos++;
                    if (_ciclos % 4 == 0 && _regrasAtivas < 10)
                    {
                        _regrasAtivas++;
                        lblDescricaoRegra.Text = descricoesRegras[_regrasAtivas - 1];
                    }

                    lblRegrasAtivas.Text = $"Regras: {_regrasAtivas}/10";
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
                lblFeedback.Text = "BOM!";
                lblFeedback.TextColor = Colors.Green;
            }
            else
            {
                FinalizarJogo("PERDESTE: Clique Errado!");
            }
        }

        private void FinalizarJogo(string motivo)
        {
            _estaAcorrer = false;
            _cts?.Cancel();

            lblFeedback.Text = motivo;
            lblFeedback.TextColor = Colors.Red;
            lblDescricaoRegra.Text = "";

            DisplayAlertAsync("Fim do Teste", $"Regras acumuladas: {_regrasAtivas}", "OK");

            btnAcao.IsEnabled = true;
            btnAcao.Opacity = 1;
            btnAcao.Text = "Recomeçar";
        }

        private async void OnGoToHomeClicked(object sender, EventArgs e)
        {
            _cts?.Cancel();
            await Navigation.PopAsync();
        }

        private async void OnHelpButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlertAsync("Regras", "Clica no objeto se ele cumprir QUALQUER uma das regras ativas!", "OK");
        }
    }
}