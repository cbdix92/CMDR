
namespace CMDR
{
    public struct BoolArray
    {
        #region PRIVATE_MEMBERS
        
        private byte[] _data;
        
        private int _size;
        
        #endregion
        public bool this[int index]
        {
            get
            {
                int i = index % 8;
                byte b = _data[index / 8];
                return ((b << i) == 0x01);
            }
        }
        public BoolArray(int initialSize)
        {
            _size = initialSize;
            _data = new byte[initialSize / 8];
        }
    }
}
