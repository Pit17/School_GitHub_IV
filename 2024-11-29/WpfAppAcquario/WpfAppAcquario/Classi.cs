using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfAppAcquario
{
    internal class Classi
    {
        public class Inanimato
        {
            Inanimato() { }

            Image foto;
            Image AggiungiOggetti(string nome_foto, Thickness posizione)
            {
                Uri source;
                source = new Uri($@"/Images/{nome_foto}", UriKind.RelativeOrAbsolute);
                BitmapImage bitmpas = new BitmapImage(source);
                foto = new Image();
                foto.Source = bitmpas;
                foto.Margin = posizione;
                return foto;
            }
        }
        public class AnimatoSulPosto : Inanimato
        {
            void SpecchiaImmagine(Image foto)
            {
                new ScaleTransform() { ScaleX = -1 };
            }
        }
    }
}
