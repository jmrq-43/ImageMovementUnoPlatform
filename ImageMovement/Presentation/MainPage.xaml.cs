using System.Reflection;
using Windows.System;
using Microsoft.UI.Xaml.Input;
using NAudio.Wave;

namespace ImageMovement.Presentation;

public sealed partial class MainPage : Page
{
    private IWavePlayer waveOutDevice;
    private AudioFileReader audioFileReader;
    private const double MoveStep = 10;
    
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

        double currentTop = Canvas.GetTop(nave2);
        double newTop = currentTop;
        
        switch (e.Key)
        {
            case VirtualKey.Up:
                newTop -= MoveStep;
                PlaySound();
                break;
            case VirtualKey.Down:
                newTop += MoveStep;
                PlaySound();
                break;
        }

        if (newTop < 0 ) newTop = 0;
        if (newTop > MainCanvas.ActualHeight - nave2.ActualHeight)
            newTop = MainCanvas.ActualHeight - nave2.ActualHeight;
        Canvas.SetTop(nave2, newTop);
    }

    public void PlaySound()
    {
        waveOutDevice.Stop();
        audioFileReader.Position = 0;
        waveOutDevice.Play();
    }
}
