using Windows.System;
using Microsoft.UI.Xaml.Input;

namespace ImageMovement.Presentation;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        this.Loaded += MainPage_Loaded;     // Suscribirse l evento Loaded para enfocar la página
        this.KeyDown += MainPage_KeyDown;   // Suscribirse al evento KeyDown
    }

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        this.Focus(FocusState.Programmatic);  // Asegurar que la página tenga el foco al cargar
    }

    private void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Tecla presionada: " + e.Key);  // Línea de depuración

        int currentRow = Grid.GetRow(nave2);

        switch (e.Key)
        {
            case VirtualKey.Up:
                if (currentRow > 0) Grid.SetRow(nave2, currentRow - 1);
                break;
            case VirtualKey.Down:
                if (currentRow < MainGrid.RowDefinitions.Count - 1) Grid.SetRow(nave2, currentRow + 1);
                break;
        }
    }
}
