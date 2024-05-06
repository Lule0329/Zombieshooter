namespace Zombieshooter
{
    internal class Weapon
    {
        // hur mycket skada som vapnet gör
        private int damage;

        // hur lång tid det måste gå mellan varje skott
        private TimeSpan fireRate;

        // när som vapnet avfyrades senast
        private DateTime lastTimeFired = DateTime.Now;

        public Weapon(int damage, TimeSpan fireRate)
        {
            this.damage = damage;
            this.fireRate = fireRate;
        }

        /// <summary>
        /// Hur mycket damage som vapnet gör
        /// </summary>
        public int GetDamage()
        {
            return damage;
        }

        /// <summary>
        /// Försök att avfyra vapnet. Returnerar false om det inte kunde avfyras eftersom det inte
        /// gått tillräckligt lång tid sen senaste skottet.
        /// </summary>
        public bool Fire()
        {
            if (lastTimeFired + fireRate <= DateTime.Now)
            {
                lastTimeFired = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
