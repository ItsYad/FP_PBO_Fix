using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

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
        private int highScore = 0;

        private string[] wordBank = {
            "object", "class", "method", "attribute", "encapsulation",
            "inheritance", "polymorphism", "abstraction", "constructor",
            "instance", "interface", "override", "static", "virtual",
            "private", "public", "protected", "component", "relationship",
            "responsibility", "implementation", "composition", "superclass",
            "subclass", "initialization", "reference", "binding",
            "message", "runtime", "type"
        };

        private string leaderboardFile = "leaderboard.txt";
        private List<int> topScores = new List<int>();
        private SoundPlayer hitSound;
        private SoundPlayer backgroundMusic;

        public MainForm()
        {
            this.Text = "Typing Invaders";
            this.ClientSize = new Size(1000, 600);
            this.BackColor = Color.Black;
            this.DoubleBuffered = true;

            try
            {
                this.BackgroundImage = Image.FromFile("background.jpg");
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            catch { }

            inputBox = new TextBox()
            {
                Font = new Font("Consolas", 16),
                Width = 400,
                Location = new Point((this.ClientSize.Width - 400) / 2, 530)
            };
            inputBox.KeyDown += InputBox_KeyDown;
            this.Controls.Add(inputBox);

            lblScore = new Label()
            {
                Text = "Score: 0",
                ForeColor = Color.White,
                Font = new Font("Consolas", 14),
                Location = new Point(10, 10),
                AutoSize = true
            };
            this.Controls.Add(lblScore);

            Button btnLeaderboard = new Button()
            {
                Text = "Leaderboard",
                Location = new Point(850, 10),
                Size = new Size(120, 30),
                Font = new Font("Consolas", 10)
            };
            btnLeaderboard.Click += (s, e) => new LeaderboardForm().ShowDialog();
            this.Controls.Add(btnLeaderboard);

            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 100;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.Paint += OnPaint;

            LoadLeaderboard();
            LoadSounds();
            this.FormClosing += MainForm_FormClosing;
        }

        private void LoadSounds()
        {
            try
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                string hitPath = Path.Combine(basePath, "Resources", "hit.wav");
                string bgmPath = Path.Combine(basePath, "Resources", "bgm.wav");

                hitSound = new SoundPlayer(hitPath);
                hitSound.Load();

                backgroundMusic = new SoundPlayer(bgmPath);
                backgroundMusic.Load();
                backgroundMusic.PlayLooping();
            }
            catch (Exception ex)
            {
                MessageBox.Show("File suara tidak ditemukan di folder Resources: " + ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            backgroundMusic?.Stop();
        }

        private void LoadLeaderboard()
        {
            if (File.Exists(leaderboardFile))
            {
                var lines = File.ReadAllLines(leaderboardFile);
                topScores = lines.Select(line => int.TryParse(line, out int s) ? s : 0).ToList();
            }
        }

        private void SaveLeaderboard()
        {
            topScores.Add(score);
            topScores = topScores.OrderByDescending(s => s).Take(5).ToList();
            File.WriteAllLines(leaderboardFile, topScores.Select(s => s.ToString()));
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
                    hitSound?.Play();
                    lblScore.Text = $"Score: {score}";
                }

                inputBox.Clear();
                Invalidate();
            }
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (rand.Next(100) < 5)
            {
                Alien alien;
                string word = wordBank[rand.Next(wordBank.Length)];
                int x = rand.Next(50, 900);

                if (rand.Next(10) < 2)
                    alien = new BossAlien(word, x);
                else
                    alien = new NormalAlien(word, x);

                aliens.Add(alien);
            }

            foreach (var a in aliens)
                a.Y += a.Speed;

            if (aliens.Any(a => a.Y >= 500))
            {
                gameTimer.Stop();
                SaveLeaderboard();
                highScore = Math.Max(highScore, score);
                string message = $"Game Over!\nSkor kamu: {score}\nHigh Score: {highScore}\n\nTOP 5 SKOR:\n";
                message += string.Join("\n", topScores.Select((s, i) => $"{i + 1}. {s}"));
                MessageBox.Show(message, "Typing Invaders");
                Application.Exit();
            }

            Invalidate();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (var a in aliens)
            {
                g.DrawString(a.Word, new Font("Arial", 16, FontStyle.Bold), Brushes.Gray, a.X + 2, a.Y + 2);
                a.Draw(g);
            }
        }
    }
}
