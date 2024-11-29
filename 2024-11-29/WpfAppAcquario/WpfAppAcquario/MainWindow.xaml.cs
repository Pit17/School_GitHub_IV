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
using System.Windows.Threading;
//Malzone Pietro 4H 22/11/2024 wpf app acquario
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
            SetupTimer();
            AggiungiOggetti();
        }
        DispatcherTimer timer;
        void SetupTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(timer_tick);
            timer.Start();

        }
        int i = 0;
        
        void timer_tick(object sender,EventArgs args)
        {
            i++;
            Contatore.Text = i.ToString();
            
        }
        Image immagine;
        private void AggiungiOggetti()
        {
            Uri source;
            source = new Uri(@"/Images/pngegg.png", UriKind.RelativeOrAbsolute);
            BitmapImage bitmpas = new BitmapImage(source);
            immagine = new Image();
            immagine.Source = bitmpas;
            immagine.Margin = new Thickness(300, 50, 0, 0);
            Acquario.Children.Add(immagine);
        }
        int x = 0;
        int y = 0;
        private void Translate_Click(object sender, RoutedEventArgs e)
        {
            TranslateTransform translateTransform;
            translateTransform = new TranslateTransform(--x,++y);
            immagine.RenderTransform = translateTransform;
        }
        int gradi = 0;
        private void RotateSx_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform;
            rotateTransform = new RotateTransform(gradi+=10);
            immagine.RenderTransform = rotateTransform;
        }

        private void RotateDx_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform;
            rotateTransform = new RotateTransform(gradi -= 10,100,100);
            immagine.RenderTransform = rotateTransform;
        }
        double scalaX = 1;
        double scalaY = 1;

        private void Scale_add_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform scaleTransform;
            scalaX = scalaX + 0.1;
            scalaY = scalaY + 0.1;
            scaleTransform = new ScaleTransform(scalaX, scalaY);
            immagine.RenderTransform = scaleTransform;
        }

        private void Scale_minus_Click(object sender, RoutedEventArgs e)
        {
            ScaleTransform scaleTransform;
            scalaX = scalaX - 0.1;
            scalaY = scalaY - 0.1;
            scaleTransform = new ScaleTransform(scalaX, scalaY);
            immagine.RenderTransform = scaleTransform;
        }
    }
}