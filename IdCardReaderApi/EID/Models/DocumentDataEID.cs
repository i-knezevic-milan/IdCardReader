using System.Runtime.InteropServices;

namespace IdCardReaderApi.EID
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
	public struct DocumentDataEID
	{
		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_DocRegNo)]
		public byte[] DocRegNo;

		public int DocRegNoSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_DocumentType)]
		public byte[] DocumentType;

		public int DocumentTypeSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_IssuingDate)]
		public byte[] IssuingDate;

		public int IssuingDateSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_ExpiryDate)]
		public byte[] ExpiryDate;

		public int ExpiryDateSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_IssuingAuthority)]
		public byte[] IssuingAuthority;

		public int IssuingAuthoritySize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_DocumentSerialNumber)]
		public byte[] DocumentSerialNumber;

		public int DocumentSerialNumberSize;

		[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U8, SizeConst = IdCardReader.EID_MAX_ChipSerialNumber)]
		public byte[] ChipSerialNumber;

		public int ChipSerialNumberSize;
	}
}
