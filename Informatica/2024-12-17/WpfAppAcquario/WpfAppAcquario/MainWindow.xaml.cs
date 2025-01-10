using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfLibrary;

namespace WpfAppAcquario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            // Inizializza il DispatcherTimer
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(32)
            };
            timer.Start();

            AnimatoSulPosto stella = new AnimatoSulPosto();
            Acquario.Children.Add(stella.AggiungiConSpecchio("stella.png", new Thickness(80, 350, 0, 0)));

            AnimatoSulfondo granchio = new AnimatoSulfondo();
            Acquario.Children.Add(granchio.AggiungiConAnimazione("crab.png", 450, Acquario.Width, timer));

            Inanimato pianta1 = new Inanimato();
            Acquario.Children.Add(pianta1.Aggiungi("alga.png", new Thickness(0, 220, 200, 0)));

            AnimatoInAcqua pesce2 = new AnimatoInAcqua();
            Acquario.Children.Add(pesce2.AggiungiConAnimazione("pngegg.png", 0, Acquario.Width, Acquario.Height, timer));

            animatoPilotatoSilurante animatoPilotato = new animatoPilotatoSilurante();
            Acquario.Children.Add(animatoPilotato.AggiungiConControllo("submarine.png", 0, Acquario.Width, Acquario.Height, this, Acquario, timer));
        }
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}