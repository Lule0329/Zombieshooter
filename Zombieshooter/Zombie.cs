﻿namespace Zombieshooter
{
    internal class Zombie
    {
        // det längsta och kortaste tillåtna avståndet från vänster
        private const int MAX_LEFT = 1163;
        private const int MIN_LEFT = 350;

        // antal hitpoint
        private int hitPoints;

        // hastighet i antal procent per sekund. dvs 20, betyder att zombien flyttar sig 20% av
        // sträckan varje sekund
        private int speedPercentPerSec;

        // läget i procent. dvs. 0 är startläget, 50 är halvvägs, 100 är att zombien kommit hela
        // vägen fram och biter spelaren.
        private int locationPercent;

        // bild som representerar zombien
        private PictureBox pic;

        // text som visa antal hitpoints
        private Label label;

        public Zombie(int hitPoints, int speedPercentPerSec, int locationPercent)
        {
            this.hitPoints = hitPoints;
            this.speedPercentPerSec = speedPercentPerSec;
            this.locationPercent = locationPercent;

            // skapa en ny PictureBox och Label för varje zombie
            pic = newPic();
            label = newLabel();

            // uppdatera läge för bild och text. dvs när zombien rör sig ska även bild och
            // text flytta sig.
            updateView();
        }

        /// <summary>
        /// Ger alla kontroller som tillhör zombien och som måste finnas i formuläret
        /// </summary>
        public List<Control> GetControls()
        {
            return new List<Control>() { pic, label };
        }

        /// <summary>
        /// Flytta zombien ett avstånd som är i enlighet med hastigheten.
        /// </summary>
        /// <param name="millis">Antal millisekunder som gått sedan senaste flytten</param>
        public void Move(int millis)
        {
            // avståndet i procent som zombien ska flyttas
            int distancePercent = (millis * speedPercentPerSec) / 1000;
            // nya läget för zombien, flytta inte längre än till 100%
            locationPercent = Math.Min(100, locationPercent + distancePercent);
            // uppdatera läget för bild och text
            updateView();            
        }

        /// <summary>
        /// Skjut zombien med en vapen. Zombien skadas lika mycket som vapnets damage. Returnera
        /// true om zombien dör.
        /// </summary>
        /// <param name="weapon">Vapen som skjuter på zombien</param>
        public bool Shoot(Weapon weapon)
        {
            // om vapnet inte är null så tas mängden damage som vaånet gör bort från zombiens hitpoints
            if (weapon != null) 
            {
                hitPoints -= weapon.GetDamage();
            }

            return NoHitpoints();
        }

        /// <summary>
        /// Returnerar true om zombien inte har några hitpoints kvar.
        /// </summary>
        public bool NoHitpoints()
        {
            if (hitPoints <= 0)
            {
                pic.Visible = false;
                label.Visible = false;
                return true;
            } 

            return false;
        }

        /// <summary>
        /// Returnerar true om zombien har gått hela vägen fram till spelaren.
        /// </summary>
        public bool IsBiting()
        {
            return locationPercent >= 100;
        }

        /// <summary>
        /// Uppdatera läget av bild och text i en enlighet med var zombien är.
        /// </summary>
        private void updateView()
        {
            pic.Left = MIN_LEFT + (100 - locationPercent) * (MAX_LEFT - MIN_LEFT) / 100;
            label.Left = pic.Left;

            label.Text = hitPoints.ToString();
        }

        /// <summary>
        /// Skapa en ny zombiebild.
        /// </summary>
        private static PictureBox newPic()
        {
            PictureBox pic = new PictureBox();
            pic.Image = Properties.Resources.zombie;
            pic.SizeMode = PictureBoxSizeMode.Zoom;
            pic.Top = 12;
            pic.Left = 0; // set elsewhere
            pic.Width = 300;
            pic.Height = 380;
            pic.BackColor = Color.Transparent;
            return pic;
        }

        /// <summary>
        /// Skapa en ny label för hitpoints.
        /// </summary>
        private static Label newLabel()
        {
            Label label = new Label();
            label.Font = new Font("Stencil", 14, FontStyle.Regular);
            label.Top = 12;
            label.Left = 0; // set elsewhere
            label.Width = 130;
            label.Height = 35;
            label.Text = "HP: 999"; // set elsewhere
            label.ForeColor = Color.White;
            label.BackColor = Color.Transparent;
            return label;
        }

        public void HideZombie()
        {
            pic.Visible = false;
            label.Visible = false;
        }
    }
}
