namespace REFTEST
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Isso cria a pilha de navegação corretamente e evita que a app feche
            // ao usar Navigation.PushAsync
            var navigationPage = new NavigationPage(new REFTEST.Pages.MainPage());

            return new Window(navigationPage);
        }
    }
}