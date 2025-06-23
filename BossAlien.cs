using System.Drawing;

namespace TypingInvaders
{
    public class BossAlien : Alien
    {
        public override int Points => 30;
        public override int Speed => 2;

        public BossAlien(string word, int x) : base(word, x) { }

        public override void Draw(Graphics g)
        {
            g.DrawString(Word.ToUpper(), new Font("Arial", 20, FontStyle.Bold), Brushes.Red, X, Y);
        }
    }
}
