using System.Drawing;

namespace TypingInvaders
{
    public abstract class Alien
    {
        public string Word { get; set; }
        public int X { get; set; }
        public int Y { get; set; } = 0;
        public abstract int Points { get; }
        public abstract int Speed { get; }

        public Alien(string word, int x)
        {
            Word = word;
            X = x;
        }

        public abstract void Draw(Graphics g);
    }
}
