namespace SerialPortChating
{
    public class SerialCommand
    {
        public byte[] Message { get; protected set; }

        public SerialCommand(byte[] protocolFields, int bytesAnswerCount) {
            Message = protocolFields;
            BytesAnswerCount = bytesAnswerCount;
        }

        public int BytesAnswerCount { get; protected set; }
    }
}