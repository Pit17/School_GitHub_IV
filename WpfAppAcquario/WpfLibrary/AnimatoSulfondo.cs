using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace WpfLibrary
{
    public class AnimatoSulfondo : Inanimato
    {
        private DispatcherTimer timer;
        private double posizioneCorrente;
        private double velocita = 5;
        private bool direzioneDestra = false;

        public AnimatoSulfondo() : base() { }

        public Image AggiungiConAnimazione(string nome_foto, double larghezzaSchermo)
        {
            Thickness luogo = new Thickness(0,700,0,0);
            var immagine = base.Aggiungi(nome_foto,luogo);

            posizioneCorrente = larghezzaSchermo - immagine.Width;
            immagine.Margin = new Thickness(posizioneCorrente, 0, 0, 0);

            Start(immagine, larghezzaSchermo);

            return immagine;
        }

        private void Start(Image immagine, double larghezzaSchermo)
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) 
            };

            timer.Tick += (sender, e) =>
            {
                posizioneCorrente += (direzioneDestra ? velocita : -velocita);//aggiungo o sottraggo la velocità a seconda della direzione

                if (posizioneCorrente <= 0 || posizioneCorrente >= larghezzaSchermo - immagine.Width)
                {
                    direzioneDestra = !direzioneDestra;


                    immagine.RenderTransform = new ScaleTransform(direzioneDestra ? 1 : -1, 1);//specchio l'immagine se cambio direzione
                }

                immagine.Margin = new Thickness(posizioneCorrente, 0, 0, 0);
            };

            timer.Start();
        }
    }
}
