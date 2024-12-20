using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
//Malzone Pietro 4H Wpf Acquario
namespace WpfLibrary
{
    public class AnimatoInAcqua : Inanimato
    {
        private DispatcherTimer timer;
        private double velocitaX;
        private double velocitaY;
        private Random random;
        private Image immagineAnimata;
        private double canvasWidth;
        private double canvasHeight;

        public AnimatoInAcqua() : base()
        {
            random = new Random();
        }

        public Image AggiungiConAnimazione(string nome_foto, int altezzaTop, double canvasWidth, double canvasHeight)
        {
            // Imposta la posizione iniziale
            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);
            Image immagine = base.Aggiungi(nome_foto, luogo);

            // Inizializza le velocità casuali
            velocitaX = random.Next(3, 8); // Velocità orizzontale casuale tra 3 e 8 pixel per tick
            velocitaY = random.Next(3, 8); // Velocità verticale casuale tra 3 e 8 pixel per tick

            // Salva parametri e immagine
            this.immagineAnimata = immagine;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;

            // Avvia l'animazione
            StartAnimazione();

            return immagine;
        }

        private void StartAnimazione()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(32) 
            };

            // Associa il metodo al tick del timer
            timer.Tick += Timer_Tick;

            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            MuoviImmagine();
        }

        private void MuoviImmagine()
        {
            double currentX = Canvas.GetLeft(immagineAnimata);
            double currentY = Canvas.GetTop(immagineAnimata);

            if (double.IsNaN(currentX)) currentX = 0; // Gestisce valori non inizializzati
            if (double.IsNaN(currentY)) currentY = 0; // Gestisce valori non inizializzati

            // Calcola le nuove posizioni
            double newX = currentX + velocitaX;
            double newY = currentY + velocitaY;

            // Controlla i bordi orizzontali (sinistra/destra)
            if (newX < 0)
            {
                velocitaX = Math.Abs(velocitaX); // Cambia direzione verso destra
                newX = 0;
                SpecchiaImmagine();
            }
            else if (newX + immagineAnimata.ActualWidth > canvasWidth)
            {
                velocitaX = -Math.Abs(velocitaX); // Cambia direzione verso sinistra
                newX = canvasWidth - immagineAnimata.ActualWidth;
                SpecchiaImmagine();
            }

            // Controlla i bordi verticali (alto/basso)
            if (newY < 0)
            {
                velocitaY = Math.Abs(velocitaY); // Cambia direzione verso il basso
                newY = 0;
            }
            else if (newY + immagineAnimata.ActualHeight > canvasHeight)
            {
                velocitaY = -Math.Abs(velocitaY); // Cambia direzione verso l'alto
                newY = canvasHeight - immagineAnimata.ActualHeight;
            }

            // Aggiorna la posizione dell'immagine
            Canvas.SetLeft(immagineAnimata, newX);
            Canvas.SetTop(immagineAnimata, newY);
        }

        private void SpecchiaImmagine()
        {
            ScaleTransform flipTransform = new ScaleTransform
            {
                ScaleX = velocitaX > 0 ? 1 : -1, // Ribalta sull'asse X se si muove verso sinistra
                ScaleY = 1 // Mantiene la scala verticale invariata
            };
            immagineAnimata.RenderTransform = flipTransform;
            immagineAnimata.RenderTransformOrigin = new Point(0.5, 0.5); // Centra il punto di trasformazione
        }
    }
}
