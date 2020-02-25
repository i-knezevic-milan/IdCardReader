using System;
using System.Runtime.InteropServices;
using System.Text;

namespace IdCardReaderApi.EID
{
    public class IdCardReader
    {

        //
        // Constants
        //

        // DLL

        public const string DLL = "CelikApi.dll";

        // Size of all UTF-8 and binary fields in bytes

        // DocumentData

        public const int EID_MAX_DocRegNo = 9;
        public const int EID_MAX_DocumentType = 2;
        public const int EID_MAX_IssuingDate = 10;
        public const int EID_MAX_ExpiryDate = 10;
        public const int EID_MAX_IssuingAuthority = 100;
        public const int EID_MAX_DocumentSerialNumber = 10;
        public const int EID_MAX_ChipSerialNumber = 14;

        // FixedPersonalData

        public const int EID_MAX_PersonalNumber = 14;
        public const int EID_MAX_Surname = 200;
        public const int EID_MAX_GivenName = 200;
        public const int EID_MAX_ParentGivenName = 200;
        public const int EID_MAX_Sex = 2;
        public const int EID_MAX_PlaceOfBirth = 200;
        public const int EID_MAX_StateOfBirth = 200;
        public const int EID_MAX_DateOfBirth = 12;
        public const int EID_MAX_CommunityOfBirth = 200;
        public const int EID_MAX_StatusOfForeigner = 200;
        public const int EID_MAX_NationalityFull = 200;

        // VariablePersonalData

        public const int EID_MAX_State = 100;
        public const int EID_MAX_Community = 200;
        public const int EID_MAX_Place = 200;
        public const int EID_MAX_Street = 200;
        public const int EID_MAX_HouseNumber = 20;
        public const int EID_MAX_HouseLetter = 8;
        public const int EID_MAX_Entrance = 10;
        public const int EID_MAX_Floor = 6;
        public const int EID_MAX_ApartmentNumber = 12;
        public const int EID_MAX_AddressDate = 10;
        public const int EID_MAX_AddressLabel = 60;

        // Portrait

        public const int EID_MAX_Portrait = 7700;

        // Certificate

        public const int EID_MAX_Certificate = 2048;

        //
        // Option identifiers, used in function EidSetOption
        //

        public const int EID_O_KEEP_CARD_CLOSED = 1;

        //
        // API Version, used in function EidStartup. Currently only version 3 is supported
        //

        public const int EID_N_API_VERSION = 3;

        //
        // Card types, used in function EidBeginRead
        //

        public const int EID_CARD_ID2008 = 1;
        public const int EID_CARD_ID2014 = 2;
        public const int EID_CARD_IF2020 = 3; // ID for foreigners

        //
        // Certificate types, used in function EidReadCertificate
        //

        public const int EID_Cert_MoiIntermediateCA = 1;
        public const int EID_Cert_User1 = 2;
        public const int EID_Cert_User2 = 3;

        //
        // Block types, used in function EidVerifySignature
        //

        public const int EID_SIG_CARD = 1;
        public const int EID_SIG_FIXED = 2;
        public const int EID_SIG_VARIABLE = 3;
        public const int EID_SIG_PORTRAIT = 4;

        // For new card version EidVerifySignature function will return EID_E_UNABLE_TO_EXECUTE for
        // parameter EID_SIG_PORTRAIT. Portrait is in new cards part of EID_SIG_FIXED. To determine
        // the card version use second parameter of function EidBeginRead

        //
        // Function return values
        //

        public const int EID_OK = 0;
        public const int EID_E_GENERAL_ERROR = -1;
        public const int EID_E_INVALID_PARAMETER = -2;
        public const int EID_E_VERSION_NOT_SUPPORTED = -3;
        public const int EID_E_NOT_INITIALIZED = -4;
        public const int EID_E_UNABLE_TO_EXECUTE = -5;
        public const int EID_E_READER_ERROR = -6;
        public const int EID_E_CARD_MISSING = -7;
        public const int EID_E_CARD_UNKNOWN = -8;
        public const int EID_E_CARD_MISMATCH = -9;
        public const int EID_E_UNABLE_TO_OPEN_SESSION = -10;
        public const int EID_E_DATA_MISSING = -11;
        public const int EID_E_CARD_SECFORMAT_CHECK_ERROR = -12;
        public const int EID_E_SECFORMAT_CHECK_CERT_ERROR = -13;
        public const int EID_E_INVALID_PASSWORD = -14;
        public const int EID_E_PIN_BLOCKED = -15;

        //
        // Functions
        //

        [DllImport(DLL)] public static extern int EidSetOption(int nOptionID, ref UInt32 nOptionValue);

        [DllImport(DLL)] public static extern int EidStartup(int nApiVersion);
        
        [DllImport(DLL)] public static extern int EidCleanup();
        
        [DllImport(DLL)] public static extern int EidBeginRead(string szReader, ref int pnCardVersion);
        
        [DllImport(DLL)] public static extern int EidEndRead();

        [DllImport(DLL, CharSet = CharSet.Ansi)] public static extern int EidReadDocumentData(ref DocumentDataEID documentDataEID);

        [DllImport(DLL, CharSet = CharSet.Ansi)] public static extern int EidReadFixedPersonalData(ref FixedPersonalDataEID fixedPersonalDataEID);

        [DllImport(DLL, CharSet = CharSet.Ansi)] public static extern int EidReadVariablePersonalData(ref VariablePersonalDataEID variablePersonalDataEID);

        [DllImport(DLL)] public static extern int EidReadPortrait(ref PortraitEID portraitEID);

        [DllImport(DLL)] public static extern int EidChangePassword(string szOldPassword, string szNewPassword, ref int pnTriesLeft);

        [DllImport(DLL)] public static extern int EidVerifySignature(UInt32 nSignatureID);

        public static string EidDecode(byte[] byteArray, int byteArraySize)
        {
            return Encoding.UTF8.GetString(byteArray, 0, byteArraySize);
        }

        public static string EidMessage(int status)
        {
            return status switch
            {
                EID_OK => "OK",
                EID_E_GENERAL_ERROR => "General error",
                EID_E_INVALID_PARAMETER => "Invalid parameter",
                EID_E_VERSION_NOT_SUPPORTED => "Version not supported",
                EID_E_NOT_INITIALIZED => "Not initialized",
                EID_E_UNABLE_TO_EXECUTE => "Unable to execute",
                EID_E_READER_ERROR => "Reader error",
                EID_E_CARD_MISSING => "Card missing",
                EID_E_CARD_UNKNOWN => "Card unknown",
                EID_E_CARD_MISMATCH => "Card mismatch",
                EID_E_UNABLE_TO_OPEN_SESSION => "Unable to open session",
                EID_E_DATA_MISSING => "Data missing",
                EID_E_CARD_SECFORMAT_CHECK_ERROR => "Card secformat check error",
                EID_E_SECFORMAT_CHECK_CERT_ERROR => "Secformat check cert error",
                EID_E_INVALID_PASSWORD => "Invalid password",
                EID_E_PIN_BLOCKED => "Pin blocked",
                _ => "Unknown error",
            };
        }

    }
}
