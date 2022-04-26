using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace A22_Ex05
{
    internal class GuessRow
    {
        private readonly byte r_NumberOfColorsToGuess;
        private readonly int r_ButtonGap = 45;
        private readonly ColorPickForm r_ColorPickForm;
        private readonly List<Button> r_ButtonGuessList = new List<Button>();
        private readonly Button r_ButtonSumbit = new Button();
        private readonly List<Button> r_ButtonFeedbackList = new List<Button>();

        internal List<Button> ButtonGuessList
        {
            get
            {
                return r_ButtonGuessList;
            }
        }

        internal Button ButtonSumbit
        {
            get
            {
                return r_ButtonSumbit;
            }
        }

        internal List<Button> ButtonFeedbackList
        {
            get
            {
                return r_ButtonFeedbackList;
            }
        }

        internal GuessRow(byte i_NumberOfColorsToGuess, byte i_NumberOfOptionsToPick, int i_XPosition, int i_YPosition, int i_Index)
        {
            r_NumberOfColorsToGuess = i_NumberOfColorsToGuess;
            initializeGuessRowComponent(i_XPosition, i_YPosition + (i_Index * r_ButtonGap));
            initializeSumbitButtonComponent(i_XPosition + (r_ButtonGap * r_NumberOfColorsToGuess), i_YPosition + (i_Index * r_ButtonGap));
            initializeFeedbackComponent(i_XPosition + (r_ButtonGap * (r_NumberOfColorsToGuess + 1)), i_YPosition + (i_Index * r_ButtonGap));
            r_ColorPickForm = new ColorPickForm(i_NumberOfOptionsToPick);
        }

        private void initializeGuessRowComponent(int i_XPosition, int i_YPosition)
        {
            for (int i = 0; i < r_NumberOfColorsToGuess; i++)
            {
                Button buttonGuess = new Button();
                buttonGuess.Location = new Point(i_XPosition + (r_ButtonGap * i), i_YPosition);
                buttonGuess.Size = new Size(40, 40);
                buttonGuess.Click += new EventHandler(this.assignButton);
                buttonGuess.BackColorChanged += enabledButtonSumbitIfRowFull;
                buttonGuess.Enabled = false;
                r_ButtonGuessList.Add(buttonGuess);
            }
        }

        private void initializeSumbitButtonComponent(int i_XPosition, int i_YPosition)
        {
            r_ButtonSumbit.Location = new Point(i_XPosition, i_YPosition + 10);
            r_ButtonSumbit.Size = new Size(40, 20);
            r_ButtonSumbit.Text = "-->>";
            r_ButtonSumbit.Enabled = false;
        }

        private void initializeFeedbackComponent(int i_XPosition, int i_YPosition)
        {
            for (int i = 0; i < r_NumberOfColorsToGuess; i++)
            {
                Button buttonFeedback = new Button();
                buttonFeedback.Location = new Point(i_XPosition + (20 * (i / 2)), 3 + i_YPosition + ((i % 2) * 20));
                buttonFeedback.Size = new Size(15, 15);
                buttonFeedback.Enabled = false;
                r_ButtonFeedbackList.Add(buttonFeedback);
            }
        }

        private void assignButton(object sender, EventArgs e)
        {
            Button buttonSender = sender as Button;
            r_ColorPickForm.ButtonToChangeColor = buttonSender;
            r_ColorPickForm.ShowDialog();
        }

        internal void EnabledButtonGuess(bool isEnabled)
        {
            foreach (Button buttonGuess in r_ButtonGuessList)
            {
                buttonGuess.Enabled = isEnabled;
            }
        }

        private void enabledButtonSumbitIfRowFull(object sender, EventArgs e)
        {
            r_ButtonSumbit.Enabled = true;
            foreach (Button buttonGuess in r_ButtonGuessList)
            {
                if (buttonGuess.UseVisualStyleBackColor)
                {
                    r_ButtonSumbit.Enabled = false;
                }
            }
        }

        internal void DisplayFeedback(byte i_PerfectGuessScore, byte i_AppearanceGuessScour)
        {
            int i;
            for (i = 0; i < i_PerfectGuessScore; i++)
            {
                r_ButtonFeedbackList[i].BackColor = Color.Black;
            }

            for (; i < i_PerfectGuessScore + i_AppearanceGuessScour; i++)
            {
                r_ButtonFeedbackList[i].BackColor = Color.Yellow;
            }
        }
    }
}
