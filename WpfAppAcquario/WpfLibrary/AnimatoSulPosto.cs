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
    public class AnimatoSulPosto : Inanimato
    {
        private DispatcherTimer? timer;
        private bool specchiato = false;

        public AnimatoSulPosto() : base() { }

        public Image AggiungiConSpecchio(string nome_foto, Thickness luogo)
        {
            var immagine = base.Aggiungi(nome_foto, luogo);
            Start(immagine);

            return immagine;
        }

        private void Start(Image immagine)
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            timer.Tick += (sender, e) =>
            {

                if (specchiato)
                {
                    immagine.RenderTransform = new ScaleTransform(1, 1);
                }
                else
                {
                    immagine.RenderTransform = new ScaleTransform(-1, 1);
                }

                specchiato = !specchiato;
            };

            timer.Start();
        }
    }
}
