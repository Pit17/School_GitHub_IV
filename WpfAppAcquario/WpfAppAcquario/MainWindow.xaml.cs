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
            AnimatoInAcqua pesce1 = new AnimatoInAcqua();
            Acquario.Children.Add(pesce1.AggiungiConAnimazione("pngegg.png", 1500, 100));
            Inanimato pianta1 = new Inanimato();
            Acquario.Children.Add(pianta1.Aggiungi("alga.png", new Thickness(0, 400, 200, 200)));

        }
    }
}