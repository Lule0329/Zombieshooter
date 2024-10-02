namespace Zombieshooter
{
    public partial class Form1 : Form
    {
        // lista som innehåller alla levande zombies
        List<Zombie> zombieList = new List<Zombie>();

        // vapen med olika egenskaper
        Weapon revolver = new Weapon(50, TimeSpan.FromMilliseconds(200));
        Weapon shotgun = new Weapon(200, TimeSpan.FromMilliseconds(600));

        // ljudeffekt för shotgun
        System.Media.SoundPlayer shotgunSound =
            new System.Media.SoundPlayer(Properties.Resources.shotgun_sound);

        // ljudeffekt för revolver
        System.Media.SoundPlayer revolverSound =
            new System.Media.SoundPlayer(Properties.Resources.revolver_sound);

        // ljudeffekt för när spelaren dör
        System.Media.SoundPlayer deathSound =
            new System.Media.SoundPlayer(Properties.Resources.wilhelm_scream);

        // ljudeffekt för när zombie dör
        System.Media.SoundPlayer zombieDeathSound =
            new System.Media.SoundPlayer(Properties.Resources.zombie_death);

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 1. Avfyra shotgunen om det inte överskrider "firing rate"
        /// 2. Den närmase zombien skadas
        /// 3. Om den skadade zombien förlorar alla sina HP dör den och spelaren får poäng
        /// </summary>
        private void picShotgun_Click(object sender, EventArgs e)
        {
            shotgunSound.Play();

            bool didFire = shotgun.Fire();
            if (didFire)
            {
                // TODO skada första zombien
            }
        }

        /// <summary>
        /// 1. Avfyra revolvern om det inte överskrider "firing rate"
        /// 2. Den närmase zombien skadas
        /// 3. Om den skadade zombien förlorar alla sina HP dör den och spelaren får poäng
        /// </summary>
        private void picRevolver_Click(object sender, EventArgs e)
        {
            zombieDeathSound.Play();

            bool didFire = revolver.Fire();
            if (didFire)
            {
                // TODO skada första zombien
            }
        }

        /// <summary>
        /// Kontrollera om en zombie kommit hela vägen fram. Kommer en zombie hela vägen fram
        /// förlorar man spelet.
        /// </summary>
        private void loseGameIfZombieIsBiting()
        {
            // TODO om zombie kommit hela vägen fram förlorar man spelet
        }

        /// <summary>
        /// Flytta alla zombies. Kontrollera om någon kommit ända fram.
        /// </summary>
        private void timerMove_Tick(object sender, EventArgs e)
        {
            foreach (Zombie zombie in zombieList)
            {
                zombie.Move(timerMove.Interval);
            }
            loseGameIfZombieIsBiting();
        }

        /// <summary>
        /// Skapa (eng. spawn) en ny zombie och lägg till i listan.
        /// </summary>
        private void timerSpawn_Tick(object sender, EventArgs e)
        {
            AddZombie();
        }

        /// <summary>
        /// Skapa och lägg till en ny zombie.
        /// </summary>
        private void AddZombie()
        {
            // skapa ett nytt zombie-objekt
            Zombie zombie = new Zombie(100, 15, 0);
            // hämta och lägg till alla kontroller i zombien (picture, label m.m.)
            AddControls(zombie.GetControls());
            // lägg till zombien i zombielistan
            zombieList.Add(zombie);
        }

        /// <summary>
        /// Lägg till alla kontroller i en lista till formuläret
        /// </summary>
        private void AddControls(List<Control> controls)
        {
            foreach (Control c in controls)
            {
                Controls.Add(c);
                c.BringToFront();
            }
        }

        /// <summary>
        /// Ta bort alla kontroller i en lista från formuläret
        /// </summary>
        private void RemoveControls(List<Control> controls)
        {
            foreach (Control c in controls)
            {
                Controls.Remove(c);
            }
        }

        /// <summary>
        /// Starta spelet när man klickar på knappen.
        /// </summary>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            timerMove.Start();
            timerSpawn.Start();
            AddZombie();
        }
    }
}