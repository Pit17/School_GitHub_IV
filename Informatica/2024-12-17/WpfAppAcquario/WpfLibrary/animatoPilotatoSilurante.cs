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
using System.Windows.Media.Imaging;
using System.Diagnostics;


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
        private List<(Image, bool)> proiettili; // Lista per gestire i proiettili attivi (Contiene il proiettile e un booleano per la direzione)
        private bool specchiato;

        public animatoPilotatoSilurante() : base()
        {
            proiettili = new List<(Image, bool)>();
            proiettileTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) 
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
            this.specchiato = false;

            // Associa gli eventi di tastiera
            finestra.KeyDown -= base.Finestra_KeyDown;
            finestra.KeyDown += Finestra_KeyDown;

            return immagine;
        }

        private new void Finestra_KeyDown(object sender, KeyEventArgs e)
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
                    specchiato = false;
                    SpecchiaImmagine(specchiato);
                    break;

                case Key.Right:
                    currentX += velocita;
                    if (currentX + immagineAnimata.ActualWidth > canvasWidth)
                        currentX = canvasWidth - immagineAnimata.ActualWidth; // Evita di uscire a destra
                    specchiato = true;
                    SpecchiaImmagine(specchiato);
                    break;
               case Key.Space:
                    SparareProiettile(currentX + immagineAnimata.ActualWidth / 4, currentY + immagineAnimata.ActualHeight / 4, canvas);
                    break;
            }

            // Aggiorna la posizione dell'immagine
            Canvas.SetLeft(immagineAnimata, currentX);
            Canvas.SetTop(immagineAnimata, currentY);
        }

        public void SparareProiettile(double x, double y, Canvas acquario)
        {
            
            Image proiettile = new Image
            {
                Width = 100,
                Height = 200,
                Source = new BitmapImage(new Uri(@"/Images/bullet.png", UriKind.Relative)),
                RenderTransformOrigin = new Point(0.5, 0.5)
            };

            // Imposta la posizione iniziale del proiettile
            Canvas.SetLeft(proiettile, x - proiettile.Width / 4);
            Canvas.SetTop(proiettile, y - proiettile.Height / 4);

            // Aggiungi il proiettile al canvas e alla lista
            acquario.Children.Add(proiettile);
            proiettili.Add((proiettile, specchiato));
        }

        private void ProiettileTimer_Tick(object sender, EventArgs e)
        {
            Image entitaInAcqua = new Image
            {
                Source = new BitmapImage(new Uri("path/to/image.png", UriKind.Relative)),
                Tag = new AnimatoInAcqua()
            };
            canvas.Children.Add(entitaInAcqua);

            Image entitaSulfondo = new Image
            {
                Source = new BitmapImage(new Uri("path/to/image.png", UriKind.Relative)),
                Tag = new AnimatoSulfondo()
            };
            canvas.Children.Add(entitaSulfondo);
            // Aggiorna la posizione di ogni proiettile
            for (int i = proiettili.Count - 1; i >= 0; i--)
            {
                Image proiettile = proiettili[i].Item1;
                bool currentSpecchiato = proiettili[i].Item2;

                double currentX = Canvas.GetLeft(proiettile);

                // Sposta il proiettile in base alla direzione
                if (currentSpecchiato)
                {
                    Canvas.SetLeft(proiettile, currentX + velocitaProiettile);
                }
                else
                {
                    Canvas.SetLeft(proiettile, currentX - velocitaProiettile);
                }

                Debug.WriteLine("Proiettile: " + currentX); // Stampa in output la posizione del proiettile

                // Rimuovi il proiettile se esce dal canvas
                if (currentX < proiettile.Width - 75 || currentX > canvasWidth)
                {
                    canvas.Children.Remove(proiettile);
                    proiettili.RemoveAt(i);
                    continue;
                }

                // Usa la funzione Collisione per verificare se il proiettile colpisce un altro oggetto
                foreach (UIElement elemento in canvas.Children)
                {
                    if (elemento is Image && elemento != immagineAnimata && elemento != proiettile && Collisione(proiettile, (Image)elemento))
                    {
                        canvas.Children.Remove(proiettile);
                        proiettili.RemoveAt(i);
                        canvas.Children.Remove(elemento);
                        break;
                    }
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

        private bool Collisione(Image proiettile, Image immagine)
        {
            Rect r1 = new Rect(Canvas.GetLeft(proiettile), Canvas.GetTop(proiettile), proiettile.Width, proiettile.Height); // Calcola l'hitbox del proiettile
            Rect r2 = new Rect(Canvas.GetLeft(immagine), Canvas.GetTop(immagine), immagine.Width, immagine.Height); // Calcola l'hitbox dell'immagine
            return r1.IntersectsWith(r2); // Restituisce true se i due oggetti si sovrappongono
        }
    }
}



