namespace A22_Ex05
{
    internal class GameLogic
    {
        private readonly byte r_NumberOfSymbolsToGuess;
        private readonly byte r_NumberOfGuesses;
        private readonly ByteGenerator r_Generator;
        private GameStatus.eGameStatus m_GameStatus = GameStatus.eGameStatus.PreRun;
        private byte m_Turn;

        internal byte[] RandomSequence
        {
            get
            {
                return r_Generator.RandomSequence;
            }
        }

        internal GameStatus.eGameStatus CurrentGameStatus
        {
            get
            {
                return m_GameStatus;
            }

            set
            {
                m_GameStatus = value;
            }
        }

        internal byte Turn
        {
            get
            {
                return m_Turn;
            }
        }

        internal GameLogic(byte i_NumberOfGuesses, byte i_NumberOfSymbolsToGuess, byte i_NumberOfSymbolsToChoose)
        {
            r_NumberOfSymbolsToGuess = i_NumberOfSymbolsToGuess;
            r_NumberOfGuesses = i_NumberOfGuesses;
            r_Generator = new ByteGenerator(i_NumberOfSymbolsToChoose, r_NumberOfSymbolsToGuess);
            m_Turn = 0;
        }

        internal void StartGame()
        {
            r_Generator.GenerateSequence();
            m_Turn = 0;
            m_GameStatus = GameStatus.eGameStatus.Running;
        }

        internal void HandleGuess(byte[] i_Guess, out byte o_PerfectGuessScore, out byte o_AppearanceGuessScour)
        {
            o_PerfectGuessScore = checkPerfectGuess(i_Guess);
            o_AppearanceGuessScour = checkAppearanceGuess(i_Guess);
            o_AppearanceGuessScour -= o_PerfectGuessScore;
            m_Turn++;
            checkIfGameOver(o_PerfectGuessScore);
        }

        private void checkIfGameOver(byte i_PerfectGuessScore)
        {
            if (m_Turn == r_NumberOfGuesses)
            {
                m_GameStatus = GameStatus.eGameStatus.Lose;
            }

            if (i_PerfectGuessScore == r_NumberOfSymbolsToGuess)
            {
                m_GameStatus = GameStatus.eGameStatus.Win;
            }
        }

        private byte checkPerfectGuess(byte[] i_Guess)
        {
            byte score = 0;
            for (int index = 0; index < r_NumberOfSymbolsToGuess; index++)
            {
                if (r_Generator.RandomSequence[index] == i_Guess[index])
                {
                    score++;
                }
            }

            return score;
        }

        private byte checkAppearanceGuess(byte[] i_Guess)
        {
            byte score = 0;
            foreach (byte byteGuess in i_Guess)
            {
                foreach (byte byteSequence in r_Generator.RandomSequence)
                {
                    if (byteGuess == byteSequence)
                    {
                        score++;
                    }
                }
            }

            return score;
        }
    }
}
