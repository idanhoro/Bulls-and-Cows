namespace A22_Ex05
{
    using System;

    internal class ByteGenerator
    {
        private readonly byte[] r_RandomSequence;
        private readonly byte r_MaxRange;
        private readonly byte[] r_BytePool;
        private readonly byte r_SequenceSize;
        private readonly Random r_Random = new Random();

        internal byte[] RandomSequence
        {
            get
            {
                return r_RandomSequence;
            }
        }

        internal ByteGenerator(byte i_MaxRange, byte i_SequenceSize)
        {
            r_MaxRange = i_MaxRange;
            r_SequenceSize = i_SequenceSize;
            r_RandomSequence = new byte[i_SequenceSize];
            r_BytePool = generatePoolOfByte();
        }

        private void swapByte(ref byte io_FirstCharToSwap, ref byte io_SecondCharToSwap)
        {
            byte charToSwap = io_FirstCharToSwap;
            io_FirstCharToSwap = io_SecondCharToSwap;
            io_SecondCharToSwap = charToSwap;
        }

        private byte[] generatePoolOfByte()
        {
            byte[] numberPool = new byte[r_MaxRange];
            for (byte index = 0; index < r_MaxRange; index++)
            {
                numberPool[index] = index;
            }

            return numberPool;
        }

        private void shuffleByte()
        {
            for (byte index = 0; index < r_MaxRange; index++)
            {
                int indexToSwap = r_Random.Next(r_MaxRange);
                swapByte(ref r_BytePool[index], ref r_BytePool[indexToSwap]);
            }
        }

        internal void GenerateSequence()
        {
            shuffleByte();
            for (byte index = 0; index < r_SequenceSize; index++)
            {
                r_RandomSequence[index] = r_BytePool[index];
            }
        }
    }
}
