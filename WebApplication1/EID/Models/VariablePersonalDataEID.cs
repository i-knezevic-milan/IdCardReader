using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace WebApplication1.EID
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct VariablePersonalDataEID
	{
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_State)]
		public byte[] State;

		public int StateSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_Community)]
		public byte[] Community;

		public int CommunitySize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_Place)]
		public byte[] Place;

		public int PlaceSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_Street)]
		public byte[] Street;

		public int StreetSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_HouseNumber)]
		public byte[] HouseNumber;

		public int HouseNumberSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_HouseLetter)]
		public byte[] HouseLetter;

		public int HouseLetterSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_Entrance)]
		public byte[] Entrance;

		public int EntranceSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_Floor)]
		public byte[] Floor;

		public int FloorSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_ApartmentNumber)]
		public byte[] ApartmentNumber;

		public int ApartmentNumberSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_AddressDate)]
		public byte[] AddressDate;

		public int AddressDateSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = CardReader.EID_MAX_AddressLabel)]
		public byte[] AddressLabel;

		public int AddressLabelSize;
	}
}
