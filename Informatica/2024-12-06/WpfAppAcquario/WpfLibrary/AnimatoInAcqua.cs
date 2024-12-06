using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.CompilerServices;


namespace WpfLibrary
{
    public class AnimatoInAcqua : Inanimato
    {
        private DispatcherTimer timer;
        private double posizioneCorrente = 0;
        private double velocita = 0.001;
        private bool direzioneDestra = false;

        public AnimatoInAcqua() : base() { }
        int n = 20;
        bool sinistra = false;
        public Image AggiungiConAnimazione(string nome_foto, int altezzaTop,double canvasWidth)
        {

            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);
            Image immagine = base.Aggiungi(nome_foto, luogo);

           

            Start(immagine,canvasWidth);

            return immagine;
        }
        public void Start(Image immagine , double canvasWidth)
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (sender, e) =>
            {

                Move(false, immagine, n, canvasWidth);
                n += 20;

            };

            timer.Start();
        }
        public void Move(bool sinistra, Image immagine, int n,double canvasWidth)
        {
            double currentX = Canvas.GetLeft(immagine);
            if (double.IsNaN(currentX)) currentX = 0; // Gestisce valori non inizializzati

            // Calcola la nuova posizione
            double newX = currentX + (sinistra ? -n : n);

            // Controlla se l'immagine raggiunge i bordi
            if (newX < 0)
            {
                sinistra = false; // Cambia direzione a destra
                newX = 0; // Imposta la posizione al bordo
                SpecchiaImmagine(immagine);
            }
            else if (newX + immagine.ActualWidth > canvasWidth)
            {
                sinistra = true; // Cambia direzione a sinistra
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
                ScaleX = sinistra ? -1 : 1, // Ribalta sull'asse X se si muove a sinistra
                ScaleY = 1 // Mantiene la scala verticale invariata
            };
            immagine.RenderTransform = flipTransform;
            immagine.RenderTransformOrigin = new Point(0.5, 0.5); // Centra il punto di trasformazione
        }
    }
}

