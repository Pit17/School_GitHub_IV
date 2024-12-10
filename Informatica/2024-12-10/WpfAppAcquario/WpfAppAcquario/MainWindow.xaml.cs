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
            AnimatoSulPosto pesce1 = new AnimatoSulPosto();
            Acquario.Children.Add(pesce1.AggiungiConSpecchio("stella.png", new Thickness(200, 200, 200, 200)));
           
            Inanimato pianta1 = new Inanimato();
            Acquario.Children.Add(pianta1.Aggiungi("alga.png", new Thickness(0, 000, 200, 200)));
            AnimatoInAcqua pesce2 = new AnimatoInAcqua();
            Acquario.Children.Add(pesce2.AggiungiConAnimazione("pngegg.png",200,Acquario.Width));

        }
    }
}