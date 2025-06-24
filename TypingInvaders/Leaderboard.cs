using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TypingInvaders
{
    public class LeaderboardForm : Form
    {
        private ListBox listBoxScores;
        private Button btnClose;
        private string leaderboardFile = "leaderboard.txt";

        public LeaderboardForm()
        {
            this.Text = "Leaderboard";
            this.Size = new Size(300, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblTitle = new Label()
            {
                Text = "TOP 5 SCORES",
                Font = new Font("Consolas", 16, FontStyle.Bold),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };
            this.Controls.Add(lblTitle);

            listBoxScores = new ListBox()
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 14)
            };
            this.Controls.Add(listBoxScores);

            btnClose = new Button()
            {
                Text = "Close",
                Dock = DockStyle.Bottom,
                Height = 40
            };
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            LoadLeaderboard();
        }

        private void LoadLeaderboard()
        {
            if (File.Exists(leaderboardFile))
            {
                var lines = File.ReadAllLines(leaderboardFile)
                                .Select((s, i) => $"{i + 1}. {s}")
                                .ToArray();
                listBoxScores.Items.AddRange(lines);
            }
            else
            {
                listBoxScores.Items.Add("No scores available.");
            }
        }
    }
}