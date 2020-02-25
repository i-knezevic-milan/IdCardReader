using System.Runtime.InteropServices;

namespace IdCardReaderApi.EID
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct FixedPersonalDataEID
	{
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_PersonalNumber)]
		public byte[] PersonalNumber;

		public int PersonalNumberSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_Surname)]
		public byte[] Surname;

		public int SurnameSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_GivenName)]
		public byte[] GivenName;

		public int GivenNameSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_ParentGivenName)]
		public byte[] ParentGivenName;

		public int ParentGivenNameSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_Sex)]
		public byte[] Sex;

		public int SexSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_PlaceOfBirth)]
		public byte[] PlaceOfBirth;

		public int PlaceOfBirthSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_StateOfBirth)]
		public byte[] StateOfBirth;

		public int StateOfBirthSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_DateOfBirth)]
		public byte[] DateOfBirth;

		public int DateOfBirthSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_CommunityOfBirth)]
		public byte[] CommunityOfBirth;

		public int CommunityOfBirthSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_StatusOfForeigner)]
		public byte[] StatusOfForeigner;

		public int StatusOfForeignerSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_NationalityFull)]
		public byte[] NationalityFull;

		public int NationalityFullSize;
	}
}
