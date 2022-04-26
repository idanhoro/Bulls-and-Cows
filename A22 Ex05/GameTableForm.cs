using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace A22_Ex05
{
    internal class GameTableForm : Form
    {
        private readonly int r_ButtonGap = 45;
        private readonly int r_OffsetX = 5;
        private readonly int r_OffsetY = 10;
        private readonly byte r_NumberOfColorsToGuess;
        private readonly byte r_NumberOfOptionsToPick;
        private readonly byte r_NumberOfGuesses;
        private readonly List<GuessRow> r_GuessRowList = new List<GuessRow>();
        private readonly GameLogic r_GameLogic;
        private readonly List<Button> r_ResultRow = new List<Button>();

        internal GameTableForm(byte i_NumberOfGuesses, byte i_NumberOfColorsToGuess, byte i_NumberOfOptionsToPick)
        {
            r_NumberOfGuesses = i_NumberOfGuesses;
            r_NumberOfColorsToGuess = i_NumberOfColorsToGuess;
            r_NumberOfOptionsToPick = i_NumberOfOptionsToPick;
            r_GameLogic = new GameLogic(i_NumberOfGuesses, i_NumberOfColorsToGuess, i_NumberOfOptionsToPick);
            r_GameLogic.StartGame();
            initializeComponent();
            r_GuessRowList[0].EnabledButtonGuess(true);
        }

        private void initializeComponent()
        {
            initializeBoradComponent(r_OffsetX, r_OffsetY);

            // GameTableForm
            int width = (r_OffsetX * 2) + (r_ButtonGap * (r_NumberOfColorsToGuess + 1)) + (20 * ((r_NumberOfColorsToGuess / 2) + (r_NumberOfColorsToGuess % 2)));
            int height = (r_OffsetY * 2) + r_ButtonGap + (r_NumberOfGuesses * r_ButtonGap);
            ClientSize = new Size(width, height);
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "GameTableForm";
            ResumeLayout(false);
        }

        private void initializeBoradComponent(int i_XPosition, int i_YPosition)
        {
            initializeResultRowComponent(i_XPosition, i_YPosition);
            i_YPosition += r_ButtonGap + 10;

            for (int i = 0; i < r_NumberOfGuesses; i++)
            {
                GuessRow guessRow = new GuessRow(r_NumberOfColorsToGuess, r_NumberOfOptionsToPick, i_XPosition, i_YPosition, i);
                for (int j = 0; j < r_NumberOfColorsToGuess; j++)
                {
                    Controls.Add(guessRow.ButtonGuessList[j]);
                    Controls.Add(guessRow.ButtonFeedbackList[j]);
                }

                guessRow.ButtonSumbit.Click += new EventHandler(this.buttonSumbitClick);
                Controls.Add(guessRow.ButtonSumbit);
                r_GuessRowList.Add(guessRow);
            }
        }

        private void initializeResultRowComponent(int i_XPosition, int i_YPosition)
        {
            for (int i = 0; i < r_NumberOfColorsToGuess; i++)
            {
                Button buttonResult = new Button();
                buttonResult.Location = new Point(i_XPosition + (45 * i), i_YPosition);
                buttonResult.Size = new Size(40, 40);
                buttonResult.BackColor = Color.Black;
                buttonResult.Enabled = false;
                r_ResultRow.Add(buttonResult);
                Controls.Add(buttonResult);
            }
        }

        private void buttonSumbitClick(object sender, EventArgs e)
        {
            byte[] colorAsBytes = new byte[r_NumberOfColorsToGuess];
            for (int i = 0; i < r_NumberOfColorsToGuess; i++)
            {
                colorAsBytes[i] = (byte)AvailableColors.sr_ColorsList.IndexOf(r_GuessRowList[r_GameLogic.Turn].ButtonGuessList[i].BackColor);
            }

            byte perfectGuessScore, appearanceGuessScour;
            r_GameLogic.HandleGuess(colorAsBytes, out perfectGuessScore, out appearanceGuessScour);
            r_GuessRowList[r_GameLogic.Turn - 1].DisplayFeedback(perfectGuessScore, appearanceGuessScour);
            r_GuessRowList[r_GameLogic.Turn - 1].EnabledButtonGuess(false);
            r_GuessRowList[r_GameLogic.Turn - 1].ButtonSumbit.Enabled = false;
            if (r_GameLogic.CurrentGameStatus == GameStatus.eGameStatus.Lose || r_GameLogic.CurrentGameStatus == GameStatus.eGameStatus.Win)
            {
                endGame();
            }
            else
            {
                r_GuessRowList[r_GameLogic.Turn].EnabledButtonGuess(true);
            }
        }

        private void revealSequence()
        {
            for (int i = 0; i < r_NumberOfColorsToGuess; i++)
            {
                r_ResultRow[i].BackColor = AvailableColors.sr_ColorsList[r_GameLogic.RandomSequence[i]];
            }
        }

        private void endGame()
        {
            revealSequence();

            // MessageBox.Show(string.Format("You {0}", r_GameLogic.CurrentGameStatus));
        }
    }
}
