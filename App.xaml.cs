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
            var navigationPage = new NavigationPage(new REFTEST.Pages.PT.Main.MainPage());

            return new Window(navigationPage);
        }
    }
}