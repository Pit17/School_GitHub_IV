using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace WpfLibrary
{
    public class animatoPilotatoSilurante : animatoPilotato
    {
        private double velocitaProiettile = 15; // Velocità del proiettile in pixel per tick
        private List<(Image, bool)> proiettili; // Lista per gestire i proiettili attivi (Contiene il proiettile e un booleano per la direzione)
        private bool specchiato;
        private List<Image> entita = new List<Image>();

        public animatoPilotatoSilurante() : base()
        {
            proiettili = new List<(Image, bool)>();
        }

        public void AggiungiEntita(Image immagine, object tipoEntita)
        {
            immagine.Tag = tipoEntita;
            Canvas.SetLeft(immagine, new Random().Next(0, (int)(canvasWidth - immagine.Width)));
            Canvas.SetTop(immagine, new Random().Next(0, (int)(canvasHeight - immagine.Height)));
            canvas.Children.Add(immagine);
            entita.Add(immagine);
        }

        public Image AggiungiConControllo(string nome_foto, int altezzaTop, double canvasWidth, double canvasHeight, Window finestra, Canvas canvas, DispatcherTimer timer)
        {
            // Imposta la posizione iniziale
            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);
            Image immagine = base.AggiungiConControllo(nome_foto, altezzaTop, canvasWidth, canvasHeight, finestra, canvas);

            // Salva l'immagine e le dimensioni del canvas
            this.specchiato = false;

            // Avvia l'animazione dei proiettili
            timer.Tick += (sender, e) =>
            {
                ProiettileTimer_Tick();
            };

            return immagine;
        }

        protected new void Finestra_KeyDown(object sender, KeyEventArgs e)
        {
            base.Finestra_KeyDown(sender, e);

            double currentX = Canvas.GetLeft(immagineAnimata);
            double currentY = Canvas.GetTop(immagineAnimata);

            if (double.IsNaN(currentX)) currentX = 0; // Gestisce valori non inizializzati
            if (double.IsNaN(currentY)) currentY = 0; // Gestisce valori non inizializzati

            if (e.Key == Key.Space)
            {
                SparareProiettile(currentX + immagineAnimata.ActualWidth / 2, currentY + immagineAnimata.ActualHeight / 2, canvas);
            }
        }

        public void SparareProiettile(double x, double y, Canvas acquario)
        {
            Image proiettile = new Image
            {
                Width = 20,
                Height = 20,
                Source = new BitmapImage(new Uri(@"/Images/bullet.png", UriKind.Relative)),
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            // Imposta la posizione iniziale del proiettile
            Canvas.SetLeft(proiettile, x - proiettile.Width / 2);
            Canvas.SetTop(proiettile, y - proiettile.Height / 2);

            // Aggiungi il proiettile al canvas e alla lista
            acquario.Children.Add(proiettile);
            proiettili.Add((proiettile, specchiato));
        }

        private void ProiettileTimer_Tick()
        {
            for (int i = proiettili.Count - 1; i >= 0; i--)
            {
                Image proiettile = proiettili[i].Item1;
                bool currentSpecchiato = proiettili[i].Item2;

                double currentX = Canvas.GetLeft(proiettile);
                double currentY = Canvas.GetTop(proiettile);

                if (double.IsNaN(currentX)) currentX = 0;
                if (double.IsNaN(currentY)) currentY = 0;

                // Aggiorna la posizione del proiettile
                Canvas.SetLeft(proiettile, currentX + (currentSpecchiato ? velocitaProiettile : -velocitaProiettile));

                // Rimuovi il proiettile se esce dal canvas
                if (currentX < 0 || currentX > canvasWidth)
                {
                    canvas.Children.Remove(proiettile);
                    proiettili.RemoveAt(i);
                    continue;
                }

                // Controlla collisioni con le entità
                for (int j = entita.Count - 1; j >= 0; j--)
                {
                    Image entitaCorrente = entita[j];
                    if (Collisione(proiettile, entitaCorrente))
                    {
                        Debug.WriteLine("Collisione rilevata!");
                        canvas.Children.Remove(proiettile);
                        canvas.Children.Remove(entitaCorrente);
                        proiettili.RemoveAt(i);
                        entita.Remove(entitaCorrente);
                        break;
                    }
                }
            }
            canvas.UpdateLayout();
        }

        private bool Collisione(Image proiettile, Image immagine)
        {
            Rect r1 = new Rect(Canvas.GetLeft(proiettile), Canvas.GetTop(proiettile), proiettile.Width, proiettile.Height); // Calcola l'hitbox del proiettile
            Rect r2 = new Rect(Canvas.GetLeft(immagine), Canvas.GetTop(immagine), immagine.Width, immagine.Height); // Calcola l'hitbox dell'immagine
            return r1.IntersectsWith(r2); // Restituisce true se i due oggetti si sovrappongono
        }
    }
}