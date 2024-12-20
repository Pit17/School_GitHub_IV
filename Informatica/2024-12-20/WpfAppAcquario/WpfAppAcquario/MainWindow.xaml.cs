using System.Windows;
using System.Windows.Input;
using WpfLibrary;
//Malzone Pietro 4H Wpf Acquario
namespace WpfAppAcquario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)//movimento della finestra
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }
        private void Close_Click(object sender, RoutedEventArgs e)//chiude la finestra
        {
            Application.Current.Shutdown();
        }
        public MainWindow()//test
        {
            InitializeComponent();

            AnimatoSulfondo granchio = new AnimatoSulfondo();
            Acquario.Children.Add(granchio.AggiungiConAnimazione("crab.png", 450, Acquario.Width));

            Inanimato ancora = new Inanimato();
            Acquario.Children.Add(ancora.Aggiungi("Ancora.png", new Thickness(1300, 400, 0, 80)));

            AnimatoSulPosto stella = new AnimatoSulPosto();
            Acquario.Children.Add(stella.AggiungiConSpecchio("stella.png", new Thickness(80, 350, 0, 0)));

            Inanimato pianta1 = new Inanimato();
            Acquario.Children.Add(pianta1.Aggiungi("alga.png", new Thickness(0, 220, 200, 0)));       

            AnimatoInAcqua pesce1 = new AnimatoInAcqua();
            Acquario.Children.Add(pesce1.AggiungiConAnimazione("pngegg.png", 0, Acquario.Width, Acquario.Height));

            AnimatoInAcqua pesce2 = new AnimatoInAcqua();
            Acquario.Children.Add(pesce2.AggiungiConAnimazione("pngegg.png",0,Acquario.Width,Acquario.Height));

            animatoPilotatoSilurante animatoPilotato = new animatoPilotatoSilurante();
            Acquario.Children.Add(animatoPilotato.AggiungiConControllo("submarine.png", 0, Acquario.Width, Acquario.Height, this, Acquario));
        }

        
    }
}