using WriteWiseApp.Auth;

namespace WriteWiseApp;

public partial class App : Application
{
    private readonly AuthService _authService;

    public App(AuthService authService)
	{
        _authService = authService;

		InitializeComponent();

		MainPage = new AppShell();
	}

    protected override Window CreateWindow(IActivationState? activationState)
    {
        Window window = base.CreateWindow(activationState);

        window.Created += async (s, e) =>
        {
            try
            {
                _ = await _authService.SigninAsync(false, CancellationToken.None);
            }catch
            {

            }
        };

        return window;
    }
}
