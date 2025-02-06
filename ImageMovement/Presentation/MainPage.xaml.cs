using System.Reflection;
using Windows.System;
using Microsoft.UI.Xaml.Input;
using NAudio.Wave;

namespace ImageMovement.Presentation;

public sealed partial class MainPage : Page
{
    private IWavePlayer waveOutDevice;
    private AudioFileReader audioFileReader;
    
    public MainPage()
    {
        this.InitializeComponent();
        this.Loaded += MainPage_Loaded;
        this.KeyDown += MainPage_KeyDown;
        InitializeMediaPlayer();
    }

    public void InitializeMediaPlayer()
    {
        waveOutDevice = new WaveOutEvent();
        string soundFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? throw new InvalidOperationException("no encontrado"), "Assets", "Sounds", "soundNave.mp3");
        audioFileReader = new AudioFileReader(soundFilePath);
        waveOutDevice.Init(audioFileReader);
    }

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        this.Focus(FocusState.Programmatic);
    }

    private void MainPage_KeyDown(object sender, KeyRoutedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Tecla presionada: " + e.Key);  

        int currentRow = Grid.GetRow(nave2);

        switch (e.Key)
        {
            case VirtualKey.Up:
                if (currentRow > 0) Grid.SetRow(nave2, currentRow - 1);
                PlaySound();
                break;
            case VirtualKey.Down:
                if (currentRow < MainGrid.RowDefinitions.Count - 1) Grid.SetRow(nave2, currentRow + 1);
                PlaySound();
                break;
        }
    }

    public void PlaySound()
    {
        waveOutDevice.Stop();
        audioFileReader.Position = 0;
        waveOutDevice.Play();
    }
}
