using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

//Malzone Pietro 4H Wpf Acquario
namespace WpfLibrary
{
    public class Inanimato
    {
        public Inanimato() { }
        Image foto;
        Uri source;

        public Image Aggiungi(string nome_foto, Thickness luogo)//aggiungo una immagine con il path indicato dal Main.Window.cs
        {
            source = new Uri(@$"/Images/{nome_foto}", UriKind.RelativeOrAbsolute);
            BitmapImage bitmpas = new BitmapImage(source);
            foto = new Image();
            foto.Source = bitmpas;
            foto.Margin = luogo;
            return foto;
        }
    }
}
