using Microsoft.JSInterop;
using Blazored.LocalStorage;

namespace NotebookApp;

public class ThemeService
{
    private readonly IJSRuntime _js;
    private readonly ILocalStorageService _localStorage;
    
    public bool IsDarkMode { get; private set; }
    public event Action? OnThemeChanged; // Событие, чтобы компоненты знали об изменении темы

    public ThemeService(IJSRuntime js, ILocalStorageService localStorage)
    {
        _js = js;
        _localStorage = localStorage;
    }

    // Загрузка темы при старте приложения
    public async Task InitializeThemeAsync()
    {
        IsDarkMode = await _localStorage.GetItemAsync<bool>("is_dark_mode");
        await ApplyThemeAsync();
    }

    // Переключение темы
    public async Task ToggleThemeAsync()
    {
        IsDarkMode = !IsDarkMode;
        await _localStorage.SetItemAsync("is_dark_mode", IsDarkMode);
        await ApplyThemeAsync();
        OnThemeChanged?.Invoke(); // Уведомляем интерфейс
    }

    private async Task ApplyThemeAsync()
    {
        string themeName = IsDarkMode ? "dark" : "light";
        await _js.InvokeVoidAsync("themeManager.setTheme", themeName);
    }
}