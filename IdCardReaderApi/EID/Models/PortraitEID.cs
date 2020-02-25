using System.Runtime.InteropServices;

namespace IdCardReaderApi.EID
{
    public struct PortraitEID
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_Portrait)]
        public byte[] PortraitData;
        
        public int PortraitSize;
    }
}
