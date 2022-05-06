using System;
using System.Collections.Generic;

namespace SerialPortChating
{
    public class SerialAnswerF03 : SerialAnswer
    {
        public byte N_Slave { get; set; }
        public byte N_Fonction { get; set; }
        public byte NbOfBytesRead { get; set; }
        public float VBatt { get; set; }
        public float IBatt { get; set; }
        public float IMD_A { get; set; }
        public float IMR_A { get; set; }
        public Int16 Alarm1 { get; set; }
        public Int16 Alarm2 { get; set; }
        public Int16 Alarm3 { get; set; }
        public byte ChargeMode { get; set; }
        public byte BatteryMode { get; set; }
        public byte SOH { get; set; }
        public byte IMRF { get; set; }
        public byte MinAvailableAh { get; set; }
        public byte Spare { get; set; }
        public byte SOCMin { get; set; }
        public sbyte TBatt { get; set; }
        public bool MajorError { get; set; }
        public bool MinorError { get; set; }

        public Dictionary<byte, string> AlarmDic { get; set; }
        public Dictionary<byte, string> ChargeModeDic { get; set; }

        public SerialAnswerF03(byte[] buffer) : base(buffer)
        {
            AlarmDic = new Dictionary<byte, string>();
            AlarmDic.Add(1, "Activated");
            AlarmDic.Add(0, "Not activated");
            ChargeModeDic = new Dictionary<byte, string>();
            ChargeModeDic.Add(0, "Connected");
            ChargeModeDic.Add(1, "Disconnected");
            ChargeModeDic.Add(2, "Forbidden Charge");
            ChargeModeDic.Add(3, "Regulated Charge");
            ChargeModeDic.Add(4, "Charge");
            ChargeModeDic.Add(5, "Floating");
            ChargeModeDic.Add(6, "Discharge");

            //Response: Device side to host side after treatment: highlighted blue
            //MemoryStream mStream = new MemoryStream(buffer);
            //BinaryReader reader = new BinaryReader(mStream);            
            //try
            //{

            //N_Slave = reader.ReadByte();
            //N_Fonction = reader.ReadByte();
            //NbOfBytesRead = reader.ReadByte();
            //VBatt = reader.ReadSingle();
            //IBatt = reader.ReadSingle();
            //IMD_A = reader.ReadSingle();
            //IMR_A = reader.ReadSingle();
            //Alarm1 = reader.ReadInt16();
            //Alarm2 = reader.ReadInt16();
            //Alarm3 = reader.ReadInt16();
            //ChargeMode = reader.ReadByte();
            //BatteryMode = reader.ReadByte();
            //SOH = reader.ReadByte();
            //IMRF = reader.ReadByte();
            //MinAvailableAh = reader.ReadByte();
            //Spare = reader.ReadByte();
            //SOCMin = reader.ReadByte();
            //TBatt = reader.ReadSByte();
            //MajorError = reader.ReadBoolean();
            //MinorError = reader.ReadBoolean();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //Response: Device side to host side before treatment: highlighted orange
            try
            {

                N_Slave = buffer[0];
                N_Fonction = buffer[1];
                NbOfBytesRead = buffer[2];
                //VBatt = BitConverter.ToSingle(new byte[] { buffer[5], buffer[6], buffer[3], buffer[4] },0);
                VBatt = (float)((1024 * buffer[5] + 512 * buffer[6] + 256 * buffer[3] + buffer[4]) / 10.0);
                //IBatt = BitConverter.ToSingle(new byte[] { buffer[9], buffer[10], buffer[7], buffer[8] },0);
                IBatt = (float)((1024 * buffer[9] + 512 * buffer[10] + 256 * buffer[7] + buffer[8]) / 10.0);
                //IMD_A = BitConverter.ToSingle(new byte[] { buffer[13], buffer[14], buffer[11], buffer[12] },0);
                IMD_A = (float)((1024 * buffer[13] + 512 * buffer[14] + 256 * buffer[11] + buffer[12]) / 10.0);
                //IMR_A = BitConverter.ToSingle(new byte[] { buffer[17], buffer[18], buffer[15], buffer[16] },0);
                IMR_A = (float)((1024 * buffer[17] + 512 * buffer[18] + 256 * buffer[15] + buffer[16]) / 10.0);
                Alarm1 = BitConverter.ToInt16(new byte[] { buffer[19], buffer[20] }, 0);
                Alarm2 = BitConverter.ToInt16(new byte[] { buffer[21], buffer[22] }, 0);
                Alarm3 = BitConverter.ToInt16(new byte[] { buffer[23], buffer[24] }, 0);
                ChargeMode = buffer[26];
                BatteryMode = buffer[25];
                SOH = buffer[28];
                IMRF = buffer[27];
                MinAvailableAh = buffer[30];
                Spare = buffer[29];
                SOCMin = buffer[32];
                TBatt = Convert.ToSByte(buffer[31]);
                MajorError = BitConverter.ToBoolean(new byte[] { buffer[34] }, 0);
                MinorError = BitConverter.ToBoolean(new byte[] { buffer[33], }, 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string BatteryModeDic(byte index)
        {
            switch (index)
            {
                case 3:
                    return "Nominal";
                case 6:
                    return "Safe";
                default:
                    return "Not defined";
            }
        }



    }
}
