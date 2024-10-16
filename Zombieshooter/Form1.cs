using System.IO;

namespace Zombieshooter
{
    public partial class Form1 : Form
    {
        // lista som innehåller alla levande zombies
        List<Zombie> zombieList = new List<Zombie>();

        // vapen med olika egenskaper
        Weapon revolver = new Weapon(50, TimeSpan.FromMilliseconds(1000));
        Weapon shotgun = new Weapon(100, TimeSpan.FromMilliseconds(2500));

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

        // Spelarens score
        int score = 0;

        int _highscore = 0;

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
            GunFire(shotgun);
        }

        /// <summary>
        /// 1. Avfyra revolvern om det inte överskrider "firing rate"
        /// 2. Den närmase zombien skadas
        /// 3. Om den skadade zombien förlorar alla sina HP dör den och spelaren får poäng
        /// </summary>
        private void picRevolver_Click(object sender, EventArgs e)
        {
            revolverSound.Play();
            GunFire(revolver);
        }

        private void GunFire(Weapon w) // samlar koden för alla vapen i en metod
        {
            bool didFire = w.Fire();

            if (didFire && zombieList.Any()) // Kontrollera om listan inte är tom
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
        /// Kontrollera om en zombie kommit hela vägen fram. Kommer en zombie hela vägen fram
        /// förlorar man spelet.
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
            Zombie zombie = new Zombie(100 * Difficulty(), 15 * Difficulty(), 0);
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
            Clear(true);
            Restart();
        }

        public void Clear(bool restarted)
        {
            // Gör game over texten synlig och flyttar den längst fram.
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

            // Gömmer varje zombie på spelplanen och rensar sedan listan med zombiesarna.
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

            // Gömmer game over texten
            gameover.Visible = false;

            // Återställer spelarens score
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
            // objekt som hjälper till att skriva text till fil
            StreamWriter writetext = new StreamWriter(path);

            writetext.Write(_highscore.ToString());

            writetext.Close();
        }

        private string ReadFile(string path)
        {
            // objekt som hjälper till att läsa text från fil
            StreamReader readtext = new StreamReader(path);

            string result = readtext.ReadLine();
            readtext.Close();

            return result.ToString();
        }
    }
}