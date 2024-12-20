using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
//Malzone Pietro 4H Wpf Acquario
namespace WpfLibrary
{
    public class animatoPilotato : AnimatoSulfondo
    {

        private double velocita = 10; // Velocità in pixel per pressione di tasto
        private Image immagineAnimata;
        private double canvasWidth;
        private double canvasHeight;
        private Canvas acquario;

        public animatoPilotato() : base() { }

        public Image AggiungiConControllo(string nome_foto, int altezzaTop, double canvasWidth, double canvasHeight, Window finestra, Canvas acquario)
        {
            // Imposta la posizione iniziale
            Thickness luogo = new Thickness(0, altezzaTop, 0, 0);
            Image immagine = base.Aggiungi(nome_foto, luogo);

            // Salva l'immagine e le dimensioni del canvas
            this.immagineAnimata = immagine;
            this.canvasWidth = canvasWidth;
            this.canvasHeight = canvasHeight;
            this.acquario = acquario;

            // Associa gli eventi di tastiera
            finestra.KeyDown += Finestra_KeyDown;

            return immagine;
        }

        protected void Finestra_KeyDown(object sender, KeyEventArgs e)
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
            }

            // Aggiorna la posizione dell'immagine
            Canvas.SetLeft(immagineAnimata, currentX);
            Canvas.SetTop(immagineAnimata, currentY);
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

