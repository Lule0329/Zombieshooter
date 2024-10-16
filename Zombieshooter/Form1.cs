using System.IO;

namespace Zombieshooter
{
    public partial class Form1 : Form
    {
        // lista som inneh�ller alla levande zombies
        List<Zombie> zombieList = new List<Zombie>();

        // vapen med olika egenskaper
        Weapon revolver = new Weapon(50, TimeSpan.FromMilliseconds(1000));
        Weapon shotgun = new Weapon(100, TimeSpan.FromMilliseconds(2500));

        // ljudeffekt f�r shotgun
        System.Media.SoundPlayer shotgunSound =
            new System.Media.SoundPlayer(Properties.Resources.shotgun_sound);

        // ljudeffekt f�r revolver
        System.Media.SoundPlayer revolverSound =
            new System.Media.SoundPlayer(Properties.Resources.revolver_sound);

        // ljudeffekt f�r n�r spelaren d�r
        System.Media.SoundPlayer deathSound =
            new System.Media.SoundPlayer(Properties.Resources.wilhelm_scream);

        // ljudeffekt f�r n�r zombie d�r
        System.Media.SoundPlayer zombieDeathSound =
            new System.Media.SoundPlayer(Properties.Resources.zombie_death);

        // Spelarens score
        int score = 0;

        int _highscore = 0;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 1. Avfyra shotgunen om det inte �verskrider "firing rate"
        /// 2. Den n�rmase zombien skadas
        /// 3. Om den skadade zombien f�rlorar alla sina HP d�r den och spelaren f�r po�ng
        /// </summary>
        private void picShotgun_Click(object sender, EventArgs e)
        {
            shotgunSound.Play();
            GunFire(shotgun);
        }

        /// <summary>
        /// 1. Avfyra revolvern om det inte �verskrider "firing rate"
        /// 2. Den n�rmase zombien skadas
        /// 3. Om den skadade zombien f�rlorar alla sina HP d�r den och spelaren f�r po�ng
        /// </summary>
        private void picRevolver_Click(object sender, EventArgs e)
        {
            revolverSound.Play();
            GunFire(revolver);
        }

        private void GunFire(Weapon w) // samlar koden f�r alla vapen i en metod
        {
            bool didFire = w.Fire();

            if (didFire && zombieList.Any()) // Kontrollera om listan inte �r tom
            {
                var firstZombie = zombieList.First();
                firstZombie.Shoot(w);

                // Kolla om zombien har 0 eller mindre hitpoints
                if (firstZombie.NoHitpoints())
                {
                    zombieDeathSound.Play();
                    zombieList.Remove(firstZombie);
                    score++;
                    labelScore.Text = "SCORE: " + score;
                }
            }
        }

        /// <summary>
        /// Kontrollera om en zombie kommit hela v�gen fram. Kommer en zombie hela v�gen fram
        /// f�rlorar man spelet.
        /// </summary>
        private void loseGameIfZombieIsBiting()
        {
            if (zombieList.Any())
            {
                if (zombieList.First().IsBiting() == true)
                {
                    deathSound.Play();
                    Clear(false);
                }
            }
        }

        /// <summary>
        /// Flytta alla zombies. Kontrollera om n�gon kommit �nda fram.
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
        /// Skapa (eng. spawn) en ny zombie och l�gg till i listan.
        /// </summary>
        private void timerSpawn_Tick(object sender, EventArgs e)
        {
            AddZombie();
        }

        /// <summary>
        /// Skapa och l�gg till en ny zombie.
        /// </summary>
        private void AddZombie()
        {
            // skapa ett nytt zombie-objekt
            Zombie zombie = new Zombie(100 * Difficulty(), 15 * Difficulty(), 0);
            // h�mta och l�gg till alla kontroller i zombien (picture, label m.m.)
            AddControls(zombie.GetControls());
            // l�gg till zombien i zombielistan
            zombieList.Add(zombie);
        }

        /// <summary>
        /// L�gg till alla kontroller i en lista till formul�ret
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
        /// Ta bort alla kontroller i en lista fr�n formul�ret
        /// </summary>
        private void RemoveControls(List<Control> controls)
        {
            foreach (Control c in controls)
            {
                Controls.Remove(c);
            }
        }

        /// <summary>
        /// Starta spelet n�r man klickar p� knappen.
        /// </summary>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Clear(true);
            Restart();
        }

        public void Clear(bool restarted)
        {
            // G�r game over texten synlig och flyttar den l�ngst fram.
            // om spelaren startar om kommer inte game over synas.
            if (restarted == false)
            {
                gameover.BringToFront();
                gameover.Visible = true;
                deathSound.Play();
            }

            // Stoppar spawn- och move timer's
            timerMove.Enabled = false;
            timerSpawn.Enabled = false;

            // G�mmer varje zombie p� spelplanen och rensar sedan listan med zombiesarna.
            foreach (Zombie z in zombieList)
            {
                z.HideZombie();
            }

            zombieList.Clear();
        }

        public void Restart()
        {
            // Startar om alla timers
            timerMove.Start();
            timerSpawn.Start();

            // G�mmer game over texten
            gameover.Visible = false;

            // �terst�ller spelarens score
            if (score > _highscore)
            {
                if (int.Parse(ReadFile("\\Users\\lule0329\\Desktop\\Score.txt")) < score)
                {
                    _highscore = score;
                    SaveScore("\\Users\\lule0329\\Desktop\\Score.txt");
                }
            }

            score = 0;

            labelScore.Text = "SCORE: " + score;
            highscore.Text = "HIGHSCORE: " + _highscore;

            // Spawnar en zombie
            AddZombie();
        }

        public int Difficulty()
        {
            if (score >= 7)
            {
                return 2;
            }
            else if (score >= 15)
            {
                return 3;
            }
            else if (score >= 25)
            {
                return 4;
            }
            else
            {
                return 1;
            }
        }

        private void labelScore_Click(object sender, EventArgs e)
        {
            score++;
        }

        private void SaveScore(string path)
        {
            // objekt som hj�lper till att skriva text till fil
            StreamWriter writetext = new StreamWriter(path);

            writetext.Write(_highscore.ToString());

            writetext.Close();
        }

        private string ReadFile(string path)
        {
            // objekt som hj�lper till att l�sa text fr�n fil
            StreamReader readtext = new StreamReader(path);

            string result = readtext.ReadLine();
            readtext.Close();

            return result.ToString();
        }
    }
}