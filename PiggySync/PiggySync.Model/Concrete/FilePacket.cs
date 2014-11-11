namespace PiggySync.Model.Concrete
{
	public class FilePacket
    {
        public FilePacket() //TODO          
        {
            
        }
        /// <summary>
        /// Firt Byte value must be 0
        /// </summary>

        public byte[] RawData
        {
            get;
            set;
        }

        public byte[] GetPacket()
        {
            return RawData;
        }

    }
}
