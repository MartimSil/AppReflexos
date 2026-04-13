using System.Diagnostics;
namespace REFTEST.Pages.PT.TesteMaster;

public partial class TesteContarMaster : ContentPage
{
    private readonly Stopwatch _sw = new();
    private readonly IDispatcherTimer _timer;

    public TesteContarMaster()
    {
        InitializeComponent();
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromMilliseconds(10);
        _timer.Tick += (s, e) => lblTempo.Text = _sw.Elapsed.ToString(@"mm\:ss\.ff");
    }

    private async void OnIniciarResetClicked(object sender, EventArgs e)
    {
        if (!_sw.IsRunning)
        {
            _sw.Start(); _timer.Start(); btnIniciarParar.Text = "PARAR";
            await Task.Delay(3000); lblTempo.FadeTo(0, 500);
        }
        else
        {
            _sw.Stop(); _timer.Stop();
            double dif = Math.Abs(_sw.Elapsed.TotalSeconds - 10);
            MasterControl.Scores[2] = dif < 0.2 ? 20 : (dif < 0.5 ? 15 : (dif < 1.0 ? 10 : 5));
            await Navigation.PushAsync(new TesteEscolherMaster());
        }
    }
}