using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Pietro Malzone 24/02/2025
namespace WpfAppImmagini
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string percorso = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory());//percorso iniziale
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadPhoto_Click(object sender, RoutedEventArgs e)//carica immagine
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = percorso;
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.webp;)|*.jpg; *.jpeg; *.png; *.gif; *.bmp; *.tiff; *.webp;";

            if (openFileDialog.ShowDialog() == true)//se l'utente ha selezionato un file
            {
                percorso = openFileDialog.FileName;
                Indirizzo.Content = percorso;
                Foto.Source = new BitmapImage(new Uri(percorso));
            }
            else//se l'utente non ha selezionato un file
            {
                MessageBox.Show("Nessun file selezionato", "Attenzione", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            
        }
        
        private int CheckInt(string s)//controlla se la stringa è un numero
        {
            int n;
            if (int.TryParse(s, out n))
            {
                return n;
            }
            else
            {
                MessageBox.Show("Inserire un numero valido", "Errore");
                return 0;
            }
        }
        

        private void Transform_Click(object sender, RoutedEventArgs e)//trasforma l'immagine
        {
            int[,] matConvol = new int[3, 3];//matrice di convoluzione
            matConvol[0, 0] = CheckInt(txt00.Text);
            matConvol[0,1] = CheckInt(txt01.Text);
            matConvol[0,2] = CheckInt(txt02.Text);
            matConvol[1,0] = CheckInt(txt03.Text);
            matConvol[1,1] = CheckInt(txt04.Text);
            matConvol[1,2] = CheckInt(txt05.Text);
            matConvol[2,0] = CheckInt(txt06.Text);
            matConvol[2,1] = CheckInt(txt07.Text);
            matConvol[2,2] = CheckInt(txt08.Text);

            try
            {
                System.Drawing.Bitmap Originale = new System.Drawing.Bitmap(percorso);//carica l'immagine
                System.Drawing.Bitmap Modificata = new System.Drawing.Bitmap(Originale.Width - 2, Originale.Height - 2);//crea un'immagine vuota

                Modificata = Convoluzione(Originale, matConvol);

                Foto.Source = BitmapToBitmapSource(Modificata);//mostra l'immagine modificata
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message, "Errore", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
        }

        private System.Drawing.Bitmap Convoluzione(System.Drawing.Bitmap originale, int[,] matConvol)//applica la convoluzione
        {
            System.Drawing.Bitmap modificata = new System.Drawing.Bitmap(originale.Width, originale.Height);
            for (int i = 1; i < originale.Width - 1; i++)//scorre l'immagine
            {
                for (int j = 1; j < originale.Height - 1; j++)//scorre l'immagine
                {
                    modificata.SetPixel(i, j, ApplicaConvoluzione(originale, i, j, matConvol));
                }
            }
            return modificata;
        }

        private System.Drawing.Color ApplicaConvoluzione(Bitmap img, int x, int y, int[,] matrice)//applica la convoluzione
        {
            int R = 0, G = 0, B = 0;
            int offset = 1;

            for (int i = -offset; i <= offset; i++)//scorre la matrice di convoluzione
            {
                for (int j = -offset; j <= offset; j++)
                {
                    int px = x + i;
                    int py = y + j;

                    
                    if (px >= 0 && px < img.Width && py >= 0 && py < img.Height)
                    {
                        System.Drawing.Color pixel = img.GetPixel(px, py);
                        int weight = matrice[i + offset, j + offset];

                        
                        R += pixel.R * weight;
                        G += pixel.G * weight;
                        B += pixel.B * weight;
                    }
                }
            }

            
            R = Math.Min(Math.Max(R, 0), 255);
            G = Math.Min(Math.Max(G, 0), 255);
            B = Math.Min(Math.Max(B, 0), 255);

            return System.Drawing.Color.FromArgb(R, G, B);
        }
        private ImageSource BitmapToBitmapSource(Bitmap source)//converte un'immagine in un'immagine visualizzabile
        {
            using (MemoryStream memory = new MemoryStream())
            {
                source.Save(memory, System.Drawing.Imaging.ImageFormat.Png);//salva l'immagine
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();//crea un'immagine visualizzabile
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;//ritorna l'immagine visualizzabile
            }
        }
    }
}
