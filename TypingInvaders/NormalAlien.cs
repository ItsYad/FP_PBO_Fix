using System.Drawing;

namespace TypingInvaders
{
    public class NormalAlien : Alien
    {
        public override int Points => 10;
        public override int Speed => 3;

        public NormalAlien(string word, int x) : base(word, x) { }

        public override void Draw(Graphics g)
        {
            g.DrawString(Word, new Font("Arial", 16), Brushes.LightGreen, X, Y);
        }
    }
}