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
//Malzone Pietro 4H Wpf Acquario
namespace WpfLibrary
{
    public class AnimatoSulfondo : Inanimato
    {
        private DispatcherTimer timer;
        private double velocita = 5;
        private bool direzioneDestra = true; // True: verso destra, False: verso sinistra

        public AnimatoSulfondo() : base() { }

        public Image AggiungiConAnimazione(string nome_foto, int altezzaTop, double canvasWidth)
        {

            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);//pos iniziale
            Image immagine = base.Aggiungi(nome_foto, luogo);


            StartAnimazione(immagine, canvasWidth);

            return immagine;
        }

        private void StartAnimazione(Image immagine, double canvasWidth)//tempo animazione
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(32)
            };

            timer.Tick += (sender, e) =>
            {
                MuoviImmagine(immagine, canvasWidth);
            };

            timer.Start();
        }

        private void MuoviImmagine(Image immagine, double canvasWidth)
        {
            double currentX = Canvas.GetLeft(immagine);
            if (double.IsNaN(currentX)) currentX = 0; // Gestisce valori non inizializzati

            // Calcola la nuova posizione
            double newX = currentX + (direzioneDestra ? velocita : -velocita);

            // Controlla se l'immagine raggiunge i bordi
            if (newX < 0)
            {
                direzioneDestra = true; // Cambia direzione a destra
                newX = 0; // Imposta la posizione al bordo
                SpecchiaImmagine(immagine);
            }
            else if (newX + immagine.ActualWidth > canvasWidth)
            {
                direzioneDestra = false; // Cambia direzione a sinistra
                newX = canvasWidth - immagine.ActualWidth; // Imposta la posizione al bordo
                SpecchiaImmagine(immagine);
            }

            // Aggiorna la posizione dell'immagine
            Canvas.SetLeft(immagine, newX);
        }

        private void SpecchiaImmagine(Image immagine)
        {
            ScaleTransform flipTransform = new ScaleTransform
            {
                ScaleX = direzioneDestra ? 1 : -1, // Ribalta sull'asse X se si muove a sinistra
                ScaleY = 1 // Mantiene la scala verticale invariata
            };
            immagine.RenderTransform = flipTransform;
            immagine.RenderTransformOrigin = new Point(0.5, 0.5); // Centra il punto di trasformazione
        }
    }
}

