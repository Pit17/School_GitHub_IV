using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WpfLibrary
{
    internal class Inanimato
    {
        public Inanimato() { }
        Image foto;
        Uri source;
        Image Aggiungi(string nome_foto)
        {
            source = new Uri(@$"/Images/{nome_foto}", UriKind.RelativeOrAbsolute);
            BitmapImage bitmpas = new BitmapImage(source);
            foto = new Image();
            foto.Source = bitmpas;
            foto.Margin = new Thickness(0, 0, 0, 0);
            return foto;
        }
    }
}
