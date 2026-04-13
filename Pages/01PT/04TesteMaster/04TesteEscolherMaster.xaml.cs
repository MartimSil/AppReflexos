namespace REFTEST.Pages.PT.TesteMaster;

public partial class TesteEscolherMaster : ContentPage
{
    private int erros = 0, acertos = 0;

    private async void ProcessarEscolha(bool correta)
    {
        if (correta) acertos++; else erros++;
        if (erros >= 5)
        {
            MasterControl.Scores[3] = Math.Min(acertos, 20);
            await Navigation.PushAsync(new TesteCoresMaster());
        }
        else ProximaRonda();
    }
}