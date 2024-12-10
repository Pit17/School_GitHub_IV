using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;

namespace WpfLibrary
{
    public class animatoPilotatoSilurante : animatoPilotato
    {





        private double velocita = 10; // Velocità in pixel per pressione di tasto
        private double velocitaProiettile = 15; // Velocità del proiettile in pixel per tick
        private Image immagineAnimata;
        private double canvasWidth;
        private double canvasHeight;
        private Canvas canvas; // Riferimento al Canvas
        private DispatcherTimer proiettileTimer;
        private List<Image> proiettili; // Lista per gestire i proiettili attivi

        public animatoPilotatoSilurante() : base()
        {
            proiettili = new List<Image>();
            proiettileTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };
            proiettileTimer.Tick += ProiettileTimer_Tick;
            proiettileTimer.Start();
        }

        public Image AggiungiConControllo(string nome_foto, int altezzaTop, double canvasWidth, double canvasHeight, Window finestra, Canvas canvas)
        {
            // Imposta la posizione iniziale
            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);
            Image immagine = base.Aggiungi(nome_foto, luogo);

            // Salva l'immagine e le dimensioni del canvas
            this.immagineAnimata = immagine;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.canvas = canvas;

            // Associa gli eventi di tastiera
            finestra.KeyDown += Finestra_KeyDown;

            return immagine;
        }

        private void Finestra_KeyDown(object sender, KeyEventArgs e)
        {
            double currentX = Canvas.GetLeft(immagineAnimata);
            double currentY = Canvas.GetTop(immagineAnimata);

            if (double.IsNaN(currentX)) currentX = 0; // Gestisce valori non inizializzati
            if (double.IsNaN(currentY)) currentY = 0; // Gestisce valori non inizializzati

            // Calcola la nuova posizione in base al tasto premuto
            switch (e.Key)
            {
                case Key.Up:
                    currentY -= velocita;
                    if (currentY < 0) currentY = 0; // Evita di uscire sopra
                    break;

                case Key.Down:
                    currentY += velocita;
                    if (currentY + immagineAnimata.ActualHeight > canvasHeight)
                        currentY = canvasHeight - immagineAnimata.ActualHeight; // Evita di uscire sotto
                    break;

                case Key.Left:
                    currentX -= velocita;
                    if (currentX < 0) currentX = 0; // Evita di uscire a sinistra
                    SpecchiaImmagine(false);
                    break;

                case Key.Right:
                    currentX += velocita;
                    if (currentX + immagineAnimata.ActualWidth > canvasWidth)
                        currentX = canvasWidth - immagineAnimata.ActualWidth; // Evita di uscire a destra
                    SpecchiaImmagine(true);
                    break;

                case Key.Space:
                    SparareProiettile(currentX + immagineAnimata.ActualWidth / 2, currentY);
                    break;
            }

            // Aggiorna la posizione dell'immagine
            Canvas.SetLeft(immagineAnimata, currentX);
            Canvas.SetTop(immagineAnimata, currentY);
        }

        private void SparareProiettile(double x, double y)
        {
            // Crea un nuovo proiettile (immagine)
            Image proiettile = new Image
            {
                Width = 10,
                Height = 20,
                Source = new System.Windows.Media.Imaging.BitmapImage(new Uri("bullet.png", UriKind.Relative)),
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            // Imposta la posizione iniziale del proiettile
            Canvas.SetLeft(proiettile, x - proiettile.Width / 2);
            Canvas.SetTop(proiettile, y - proiettile.Height);

            // Aggiungi il proiettile al canvas e alla lista
            canvas.Children.Add(proiettile);
            proiettili.Add(proiettile);
        }

        private void ProiettileTimer_Tick(object sender, EventArgs e)
        {
            // Aggiorna la posizione di ogni proiettile
            for (int i = proiettili.Count - 1; i >= 0; i--)
            {
                Image proiettile = proiettili[i];
                double currentY = Canvas.GetTop(proiettile);

                // Sposta il proiettile verso l'alto
                Canvas.SetTop(proiettile, currentY - velocitaProiettile);

                // Rimuovi il proiettile se esce dal canvas
                if (currentY < -proiettile.Height)
                {
                    canvas.Children.Remove(proiettile);
                    proiettili.RemoveAt(i);
                }
            }
        }

        private void SpecchiaImmagine(bool versoDestra)
        {
            ScaleTransform flipTransform = new ScaleTransform
            {
                ScaleX = versoDestra ? 1 : -1, // Ribalta sull'asse X se si muove verso sinistra
                ScaleY = 1 // Mantiene la scala verticale invariata
            };
            immagineAnimata.RenderTransform = flipTransform;
            immagineAnimata.RenderTransformOrigin = new Point(0.5, 0.5); // Centra il punto di trasformazione
        }
    }
}



