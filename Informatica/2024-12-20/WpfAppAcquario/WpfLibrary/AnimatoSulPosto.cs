
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//Malzone Pietro 4H Wpf Acquario
namespace WpfLibrary
{
    public class AnimatoSulPosto : Inanimato
    {
        private DispatcherTimer timer;
        private bool specchiato = false;

        public AnimatoSulPosto() : base() { }

        public Image AggiungiConSpecchio(string nome_foto, Thickness luogo)//aggiungo l'immagine
        {
            var immagine = base.Aggiungi(nome_foto, luogo);
            Start(immagine);

            return immagine;
        }

        private void Start(Image immagine)//start del timer
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (sender, e) =>
            {

                if (specchiato)//verifico la rotazione e lo ruoto dal lato opposto
                {
                    immagine.RenderTransform = new RotateTransform(20);
                    immagine.RenderTransformOrigin = new Point(0.5, 0.5); // Centra il punto di trasformazione
                    specchiato = !specchiato;
                }
                else
                {
                    immagine.RenderTransform = new RotateTransform(-20);
                    immagine.RenderTransformOrigin = new Point(0.5, 0.5); // Centra il punto di trasformazione

                    specchiato = !specchiato;
                }

                
            };

            timer.Start();
        }
    }
}
