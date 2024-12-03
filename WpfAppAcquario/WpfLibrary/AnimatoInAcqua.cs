using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace WpfLibrary
{
    public class AnimatoInAcqua : Inanimato
    {
        private DispatcherTimer timer;
        private double posizioneCorrente;
        private double velocita = 1;
        private bool direzioneDestra = false;

        public AnimatoInAcqua() : base() { }

        public Image AggiungiConAnimazione(string nome_foto, double larghezzaSchermo,int altezzaTop)
        {
            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);
            Image immagine = base.Aggiungi(nome_foto, luogo);

            posizioneCorrente = larghezzaSchermo - immagine.Width;

            Start(immagine, larghezzaSchermo);

            return immagine;
        }

        private void Start(Image immagine, double larghezzaSchermo)
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };

            timer.Tick += (sender, e) =>
            {
                posizioneCorrente += (direzioneDestra ? velocita : -velocita);//aggiungo o sottraggo la velocità a seconda della direzione

                if (posizioneCorrente <= 0 || posizioneCorrente >= larghezzaSchermo - immagine.Width)
                {
                    direzioneDestra = !direzioneDestra;


                    immagine.RenderTransform = new ScaleTransform(direzioneDestra ? 1 : -1, 1);//specchio l'immagine se cambio direzione
                }

                immagine.Margin = new Thickness((int)posizioneCorrente, 0, 0, 0);
            };

            timer.Start();
        }
    }
}
