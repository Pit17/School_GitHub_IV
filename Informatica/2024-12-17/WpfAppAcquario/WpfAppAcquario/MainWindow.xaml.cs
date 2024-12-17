using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfLibrary;

namespace WpfAppAcquario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AnimatoSulPosto stella = new AnimatoSulPosto();
            Acquario.Children.Add(stella.AggiungiConSpecchio("stella.png", new Thickness(80, 350, 0, 0)));

            AnimatoSulfondo granchio = new AnimatoSulfondo();
            Acquario.Children.Add(granchio.AggiungiConAnimazione("crab.png", 450, Acquario.Width));

            Inanimato pianta1 = new Inanimato();
            Acquario.Children.Add(pianta1.Aggiungi("alga.png", new Thickness(0, 220, 200, 0)));

            AnimatoInAcqua pesce2 = new AnimatoInAcqua();
            Acquario.Children.Add(pesce2.AggiungiConAnimazione("pngegg.png",0,Acquario.Width,Acquario.Height));

           

            animatoPilotatoSilurante animatoPilotato = new animatoPilotatoSilurante();
            Acquario.Children.Add(animatoPilotato.AggiungiConControllo("submarine.png", 0, Acquario.Width, Acquario.Height, this, Acquario));
        }
    }
}