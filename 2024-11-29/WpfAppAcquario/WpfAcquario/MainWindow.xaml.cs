using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

//Malzone Pietro 4H 29/11/2024

namespace WpfAcquario
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            AggiungiOggetti();
        }
       
        
        int i = 0;

       
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
            translateTransform = new TranslateTransform(--x, ++y);
            immagine.RenderTransform = translateTransform;
        }
        int gradi = 0;
        private void RotateSx_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform;
            rotateTransform = new RotateTransform(gradi += 10);
            immagine.RenderTransform = rotateTransform;
        }

        private void RotateDx_Click(object sender, RoutedEventArgs e)
        {
            RotateTransform rotateTransform;
            rotateTransform = new RotateTransform(gradi -= 10, 100, 100);
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

