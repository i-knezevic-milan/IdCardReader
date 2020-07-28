using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Text;
using IdCardReaderApi.Controllers;
using IdCardReaderApi.Models;
using IdCardReaderApi.Models.Responses;
using System.Text.RegularExpressions;

namespace IdCardReaderApi.EID
{
    public class IdCardReaderWrapper : IIdCardReaderWrapper
    {
        private readonly ILogger<IdCardReaderWrapper> _logger;

        public IdCardReaderWrapper(ILogger<IdCardReaderWrapper> logger)
        {
            _logger = logger;
        }

        public ActionResult getIdCardData(IdCardReaderController controller)
        {
            try
            {
                int status;

                int pnCardVersion = 0;

                status = IdCardReader.EidBeginRead("", ref pnCardVersion);

                if (status != IdCardReader.EID_OK)
                {
                    throw new IdCardReaderException() { Method = "EidBeginRead", Status = status, StatusMessage = IdCardReader.EidMessage(status), DisplayMessage = (status == IdCardReader.EID_E_GENERAL_ERROR ? "ID card not inserted." : IdCardReader.EidMessage(status) + ".") + " Please try again." };
                }

                DocumentDataEID documentDataEID = new DocumentDataEID()
                {
                    DocRegNo = new byte[IdCardReader.EID_MAX_DocRegNo],
                    DocumentType = new byte[IdCardReader.EID_MAX_DocumentType],
                    IssuingDate = new byte[IdCardReader.EID_MAX_IssuingDate],
                    ExpiryDate = new byte[IdCardReader.EID_MAX_ExpiryDate],
                    IssuingAuthority = new byte[IdCardReader.EID_MAX_IssuingAuthority],
                    DocumentSerialNumber = new byte[IdCardReader.EID_MAX_DocumentSerialNumber],
                    ChipSerialNumber = new byte[IdCardReader.EID_MAX_ChipSerialNumber]
                };

                status = IdCardReader.EidReadDocumentData(ref documentDataEID);

                if (status != IdCardReader.EID_OK)
                {
                    throw new IdCardReaderException() { Method = "EidReadDocumentData", Status = status, StatusMessage = IdCardReader.EidMessage(status), DisplayMessage = "ID card not inserted. Please try again." };
                }

                DocumentData documentData = new DocumentData();

                Map(documentDataEID, documentData);


                if (!string.IsNullOrEmpty(documentData.IssuingAuthority))
                {
                    documentData.IssuingAuthority = this.TranslitCL(documentData.IssuingAuthority);
                }


                FixedPersonalDataEID fixedPersonalDataEID = new FixedPersonalDataEID()
                {
                    PersonalNumber = new byte[IdCardReader.EID_MAX_PersonalNumber],
                    Surname = new byte[IdCardReader.EID_MAX_Surname],
                    GivenName = new byte[IdCardReader.EID_MAX_GivenName],
                    ParentGivenName = new byte[IdCardReader.EID_MAX_ParentGivenName],
                    Sex = new byte[IdCardReader.EID_MAX_Sex],
                    PlaceOfBirth = new byte[IdCardReader.EID_MAX_PlaceOfBirth],
                    StateOfBirth = new byte[IdCardReader.EID_MAX_StateOfBirth],
                    DateOfBirth = new byte[IdCardReader.EID_MAX_DateOfBirth],
                    CommunityOfBirth = new byte[IdCardReader.EID_MAX_CommunityOfBirth],
                    StatusOfForeigner = new byte[IdCardReader.EID_MAX_StatusOfForeigner],
                    NationalityFull = new byte[IdCardReader.EID_MAX_NationalityFull]
                };
                status = IdCardReader.EidReadFixedPersonalData(ref fixedPersonalDataEID);

                if (status != IdCardReader.EID_OK)
                {
                    throw new IdCardReaderException() { Method = "EidReadFixedPersonalData", Status = 3, DisplayMessage = "ID card not inserted. Please try again." };
                }

                FixedPersonalData fixedPersonalData = new FixedPersonalData();

                Map(fixedPersonalDataEID, fixedPersonalData);

                if (fixedPersonalData.GivenName != null && fixedPersonalData.GivenName != "")
                {
                    fixedPersonalData.GivenNameLatin = this.TranslitCL(fixedPersonalData.GivenName);
                }
                if (fixedPersonalData.Surname != null && fixedPersonalData.Surname != "")
                {
                    fixedPersonalData.SurnameLatin = this.TranslitCL(fixedPersonalData.Surname);
                }
                if (fixedPersonalData.ParentGivenName != null && fixedPersonalData.ParentGivenName != "")
                {
                    fixedPersonalData.ParentGivenNameLatin = this.TranslitCL(fixedPersonalData.ParentGivenName);
                }
                VariablePersonalDataEID variablePersonalDataEID = new VariablePersonalDataEID()
                {
                    State = new byte[IdCardReader.EID_MAX_State],
                    Community = new byte[IdCardReader.EID_MAX_Community],
                    Place = new byte[IdCardReader.EID_MAX_Place],
                    Street = new byte[IdCardReader.EID_MAX_Street],
                    HouseNumber = new byte[IdCardReader.EID_MAX_HouseNumber],
                    HouseLetter = new byte[IdCardReader.EID_MAX_HouseLetter],
                    Entrance = new byte[IdCardReader.EID_MAX_Entrance],
                    Floor = new byte[IdCardReader.EID_MAX_Floor],
                    ApartmentNumber = new byte[IdCardReader.EID_MAX_ApartmentNumber],
                    AddressDate = new byte[IdCardReader.EID_MAX_AddressDate],
                    AddressLabel = new byte[IdCardReader.EID_MAX_AddressLabel],
                };

                status = IdCardReader.EidReadVariablePersonalData(ref variablePersonalDataEID);

                if (status != IdCardReader.EID_OK)
                {
                    throw new IdCardReaderException() { Method = "EidReadVariablePersonalData", Status = status, StatusMessage = IdCardReader.EidMessage(status), DisplayMessage = "ID card not inserted. Please try again." };
                }

                VariablePersonalData variablePersonalData = new VariablePersonalData();

                Map(variablePersonalDataEID, variablePersonalData);

                PortraitEID portraitEID = new PortraitEID()
                {
                    PortraitData = new byte[IdCardReader.EID_MAX_Portrait]
                };

                status = IdCardReader.EidReadPortrait(ref portraitEID);

                if (status != IdCardReader.EID_OK)
                {
                    throw new IdCardReaderException() { Method = "EidReadPortrait", Status = status, StatusMessage = IdCardReader.EidMessage(status), DisplayMessage = "ID card not inserted. Please try again." };
                }

                Portrait portrait = new Portrait()
                {
                    PortraitData = "data:image; base64," + Convert.ToBase64String(portraitEID.PortraitData)
                };

                return controller.Ok(
                    new IdCardDataResponse()
                    {
                        IdCardData = new IdCardData()
                        {
                            DocumentData = documentData,
                            FixedPersonalData = fixedPersonalData,
                            VariablePersonalData = variablePersonalData,
                            Portrait = portrait
                        }
                    }
                );
            }
            catch (IdCardReaderException e)
            {
                _logger.LogError(e, e.StatusMessage);
                return controller.StatusCode(
                    440,
                    new IdCardDataResponse()
                    {
                        Message = e.DisplayMessage
                    }
                );
            }
            finally
            {
                IdCardReader.EidEndRead();
            }

        }

        public ActionResult getDocumentData(IdCardReaderController controller)
        {
            int status;

            int pnCardVersion = 0;

            do
            {
                status = IdCardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != IdCardReader.EID_OK);

            DocumentDataEID documentDataEID = new DocumentDataEID()
            {
                DocRegNo = new byte[IdCardReader.EID_MAX_DocRegNo],
                DocumentType = new byte[IdCardReader.EID_MAX_DocumentType],
                IssuingDate = new byte[IdCardReader.EID_MAX_IssuingDate],
                ExpiryDate = new byte[IdCardReader.EID_MAX_ExpiryDate],
                IssuingAuthority = new byte[IdCardReader.EID_MAX_IssuingAuthority],
                DocumentSerialNumber = new byte[IdCardReader.EID_MAX_DocumentSerialNumber],
                ChipSerialNumber = new byte[IdCardReader.EID_MAX_ChipSerialNumber]
            };

            do
            {
                status = IdCardReader.EidReadDocumentData(ref documentDataEID);
            }
            while (status != IdCardReader.EID_OK);

            IdCardReader.EidEndRead();

            DocumentData documentData = new DocumentData();

            Map(documentDataEID, documentData);

            if (!string.IsNullOrEmpty(documentData.IssuingAuthority))
            {
                documentData.IssuingAuthority = this.TranslitCL(documentData.IssuingAuthority);
            }

            return controller.Ok(documentData);
        }

        public ActionResult getFixedPersonalData(IdCardReaderController controller)
        {
            int status;

            int pnCardVersion = 0;

            do
            {
                status = IdCardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != IdCardReader.EID_OK);

            FixedPersonalDataEID fixedPersonalDataEID = new FixedPersonalDataEID()
            {
                PersonalNumber = new byte[IdCardReader.EID_MAX_PersonalNumber],
                Surname = new byte[IdCardReader.EID_MAX_Surname],
                GivenName = new byte[IdCardReader.EID_MAX_GivenName],
                ParentGivenName = new byte[IdCardReader.EID_MAX_ParentGivenName],
                Sex = new byte[IdCardReader.EID_MAX_Sex],
                PlaceOfBirth = new byte[IdCardReader.EID_MAX_PlaceOfBirth],
                StateOfBirth = new byte[IdCardReader.EID_MAX_StateOfBirth],
                DateOfBirth = new byte[IdCardReader.EID_MAX_DateOfBirth],
                CommunityOfBirth = new byte[IdCardReader.EID_MAX_CommunityOfBirth],
                StatusOfForeigner = new byte[IdCardReader.EID_MAX_StatusOfForeigner],
                NationalityFull = new byte[IdCardReader.EID_MAX_NationalityFull]
            };

            do
            {
                status = IdCardReader.EidReadFixedPersonalData(ref fixedPersonalDataEID);
            }
            while (status != IdCardReader.EID_OK);

            IdCardReader.EidEndRead();

            FixedPersonalData fixedPersonalData = new FixedPersonalData();

            Map(fixedPersonalDataEID, fixedPersonalData);

            return controller.Ok(fixedPersonalData);
        }

        public ActionResult getVariablePersonalData(IdCardReaderController controller)
        {
            int status;

            int pnCardVersion = 0;

            do
            {
                status = IdCardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != IdCardReader.EID_OK);

            VariablePersonalDataEID variablePersonalDataEID = new VariablePersonalDataEID()
            {
                State = new byte[IdCardReader.EID_MAX_State],
                Community = new byte[IdCardReader.EID_MAX_Community],
                Place = new byte[IdCardReader.EID_MAX_Place],
                Street = new byte[IdCardReader.EID_MAX_Street],
                HouseNumber = new byte[IdCardReader.EID_MAX_HouseNumber],
                HouseLetter = new byte[IdCardReader.EID_MAX_HouseLetter],
                Entrance = new byte[IdCardReader.EID_MAX_Entrance],
                Floor = new byte[IdCardReader.EID_MAX_Floor],
                ApartmentNumber = new byte[IdCardReader.EID_MAX_ApartmentNumber],
                AddressDate = new byte[IdCardReader.EID_MAX_AddressDate],
                AddressLabel = new byte[IdCardReader.EID_MAX_AddressLabel],
            };

            do
            {
                status = IdCardReader.EidReadVariablePersonalData(ref variablePersonalDataEID);
            }
            while (status != IdCardReader.EID_OK);

            IdCardReader.EidEndRead();

            VariablePersonalData variablePersonalData = new VariablePersonalData();

            Map(variablePersonalDataEID, variablePersonalData);

            return controller.Ok(variablePersonalData);
        }

        public ActionResult getPortrait(IdCardReaderController controller)
        {
            int status;

            int pnCardVersion = 0;

            do
            {
                status = IdCardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != IdCardReader.EID_OK);

            PortraitEID portraitEID = new PortraitEID()
            {
                PortraitData = new byte[IdCardReader.EID_MAX_Portrait]
            };

            do
            {
                status = IdCardReader.EidReadPortrait(ref portraitEID);
            }
            while (status != IdCardReader.EID_OK);

            IdCardReader.EidEndRead();

            return controller.Ok(new { PortraitData = "data:image;base64," + Convert.ToBase64String(portraitEID.PortraitData) });
        }

        private void Map(object src, object dest)
        {
            foreach (FieldInfo prop in src.GetType().GetFields())
            {
                if (prop.FieldType == typeof(byte[]))
                {
                    dest.GetType().GetProperty(prop.Name).SetValue(dest, Decode((byte[])src.GetType().GetField(prop.Name).GetValue(src), (int)src.GetType().GetField(prop.Name + "Size").GetValue(src)));
                }
            }
        }

        private string Decode(byte[] bajtovi, int vel)
        {
            return Encoding.UTF8.GetString(bajtovi, 0, vel);
        }

        private string TranslitCL(string input)
        {
            var cyrillic = "А_Б_В_Г_Д_Ђ_Е_Ё_Ж_З_И_Й_Ј_К_Л_Љ_М_Н_Њ_О_П_Р_С_Т_Ћ_У_Ф_Х_Ц_Ч_Џ_Ш_Щ_Ъ_Ы_Ь_Э_Ю_Я_а_б_в_г_д_ђ_е_ё_ж_з_и_й_ј_к_л_љ_м_н_њ_о_п_р_с_т_ћ_у_ф_х_ц_ч_џ_ш_щ_ъ_ы_ь_э_ю_я".Split('_');
            var latin = "A_B_V_G_D_Đ_E_Ë_Ž_Z_I_J_J_K_L_Lj_M_N_Nj_O_P_R_S_T_Ć_U_F_H_C_Č_Dž_Š_Ŝ_ʺ_Y_ʹ_È_Û_Â_a_b_v_g_d_đ_e_ë_ž_z_i_j_j_k_l_lj_m_n_nj_o_p_r_s_t_ć_u_f_h_c_č_dž_š_ŝ_ʺ_y_ʹ_è_û_â".Split('_');

            var inputArray = Regex.Split(input, String.Empty);

            for (int i = 0; i < inputArray.Length; i++)
            {
                for (int c = 0; c < cyrillic.Length; c++)
                {
                    if (inputArray[i] == cyrillic[c])
                    {
                        inputArray[i] = latin[c];
                    }
                }
            }

            string result = String.Concat(inputArray);

            return result;
        }

    }
}
