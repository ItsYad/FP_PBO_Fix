using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace TypingInvaders
{
    public partial class MainForm : Form
    {
        private System.Windows.Forms.Timer gameTimer;
        private List<Alien> aliens = new List<Alien>();
        private Random rand = new Random();
        private TextBox inputBox;
        private int score = 0;
        private Label lblScore;
        private string[] wordBank = {
    "object", "class", "method", "attribute", "encapsulation",
    "inheritance", "polymorphism", "abstraction", "constructor",
    "instance", "interface", "override", "static",
    "virtual", "private", "public", "protected", "component", "relationship", "responsibility",
    "implementation", "composition","superclass", "subclass", "initialization", "reference", "binding",
    "message", "runtime", "type"
};



        public MainForm()
        {
            this.Text = "Typing Invaders";
            this.ClientSize = new Size(1000, 600);
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            inputBox = new TextBox() { Font = new Font("Consolas", 16), Width = 300, Location = new Point(250, 500) };
            inputBox.KeyDown += InputBox_KeyDown;
            this.Controls.Add(inputBox);

            lblScore = new Label() { Text = "Score: 0", ForeColor = Color.White, Font = new Font("Consolas", 14), Location = new Point(10, 10), AutoSize = true };
            this.Controls.Add(lblScore);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.Paint += OnPaint;
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string typed = inputBox.Text.Trim().ToLower();
                var target = aliens.FirstOrDefault(a => a.Word == typed);
                if (target != null)
                {
                    score += target.Points;
                    aliens.Remove(target);
                    lblScore.Text = $"Score: {score}";
                }

                inputBox.Clear();
                Invalidate();
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (rand.Next(0, 100) < 5)
            {
                Alien alien;
                if (rand.Next(10) < 2)
                    alien = new BossAlien(wordBank[rand.Next(wordBank.Length)], rand.Next(50, 750));
                else
                    alien = new NormalAlien(wordBank[rand.Next(wordBank.Length)], rand.Next(50, 750));

                aliens.Add(alien);
            }

            foreach (var a in aliens)
                a.Y += a.Speed;

            if (aliens.Any(a => a.Y >= 450))
            {
                gameTimer.Stop();
                MessageBox.Show("Game Over! Alien mencapai tanah!");
                Application.Exit();
            }

            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var a in aliens)
            {
                a.Draw(g);
            }
        }
    }
}
