using System;

namespace SerialPortChating
{


    public class SerialAnswer
    {
        public byte[] Message { get; private set; }        
        
        public SerialAnswer(byte[] buffer)
        {
            Message = buffer;
        }

        public SerialAnswer()
        {
        }

        public static float ConcatBytesToFloat(byte[] buffer, int persition)
        {
            if (buffer.Length != 4)
                throw new ArgumentException("inputIV should be byte[4]");

            int v1 = buffer[0] << 24;
            int v2 = buffer[1] << 16;
            int v3 = buffer[2] << 8;
            int v = v1 | v2 | v3 | buffer[3];
            return (float)v / (float)Math.Pow(10, persition);
        }
    }
}