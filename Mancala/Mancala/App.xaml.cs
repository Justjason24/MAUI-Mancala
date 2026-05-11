namespace Mancala
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new AppShell());
        //}

        protected override Window CreateWindow(IActivationState? activationState)
        {
            const int newHeight = 844;
            const int newWidth = 390;

            var newWindow = new Window(new AppShell())
            {
                Height = newHeight,
                Width = newWidth,
                MinimumWidth = newWidth,
                MinimumHeight = newHeight,
                MaximumWidth = newWidth,
                MaximumHeight = newHeight,
            };

            return newWindow;
        }
    }
}