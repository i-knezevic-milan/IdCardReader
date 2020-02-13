using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebApplication1.EID
{
    public struct PortraitEID
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_Portrait)]
        public byte[] PortraitData;
        
        public int PortraitSize;
    }
}
