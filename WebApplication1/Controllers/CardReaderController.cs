using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EID;
using WebApplication1.EID.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("v1/card-reader-api")]
    public class CardReaderController : ControllerBase
    {
        [Route("document-data")]
        public ActionResult getDocumentData()
        {
            int status;

            int pnCardVersion = 0;

            do
            {
                status = CardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != CardReader.EID_OK);

            DocumentDataEID documentDataEID = new DocumentDataEID()
            {
                DocRegNo = new byte[CardReader.EID_MAX_DocRegNo],
                DocumentType = new byte[CardReader.EID_MAX_DocumentType],
                IssuingDate = new byte[CardReader.EID_MAX_IssuingDate],
                ExpiryDate = new byte[CardReader.EID_MAX_ExpiryDate],
                IssuingAuthority = new byte[CardReader.EID_MAX_IssuingAuthority],
                DocumentSerialNumber = new byte[CardReader.EID_MAX_DocumentSerialNumber],
                ChipSerialNumber = new byte[CardReader.EID_MAX_ChipSerialNumber]
            };

            do
            {
                status = CardReader.EidReadDocumentData(ref documentDataEID);
            }
            while (status != CardReader.EID_OK);

            CardReader.EidEndRead();

            DocumentData documentData = new DocumentData();

            map(documentDataEID, documentData);

            return Ok(documentData);
        }

        [Route("fixed-personal-data")]
        public ActionResult getFixedPersonalData()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            int status;

            int pnCardVersion = 0;

            do
            {
                status = CardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != CardReader.EID_OK);

            FixedPersonalDataEID fixedPersonalDataEID = new FixedPersonalDataEID()
            {
                PersonalNumber = new byte[CardReader.EID_MAX_PersonalNumber],
                Surname = new byte[CardReader.EID_MAX_Surname],
                GivenName = new byte[CardReader.EID_MAX_GivenName],
                ParentGivenName = new byte[CardReader.EID_MAX_ParentGivenName],
                Sex = new byte[CardReader.EID_MAX_Sex],
                PlaceOfBirth = new byte[CardReader.EID_MAX_PlaceOfBirth],
                StateOfBirth = new byte[CardReader.EID_MAX_StateOfBirth],
                DateOfBirth = new byte[CardReader.EID_MAX_DateOfBirth],
                CommunityOfBirth = new byte[CardReader.EID_MAX_CommunityOfBirth],
                StatusOfForeigner = new byte[CardReader.EID_MAX_StatusOfForeigner],
                NationalityFull = new byte[CardReader.EID_MAX_NationalityFull]
            };

            do
            {
                status = CardReader.EidReadFixedPersonalData(ref fixedPersonalDataEID);
            }
            while (status != CardReader.EID_OK);

            CardReader.EidEndRead();

            FixedPersonalData fixedPersonalData = new FixedPersonalData();

            map(fixedPersonalDataEID, fixedPersonalData);

            return Ok(fixedPersonalData);
        }

        [Route("variable-personal-data")]
        public ActionResult getVariablePersonalData()
        {
            int status;

            int pnCardVersion = 0;

            do
            {
                status = CardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != CardReader.EID_OK);

            VariablePersonalDataEID variablePersonalDataEID = new VariablePersonalDataEID()
            {
                State = new byte[CardReader.EID_MAX_State],
                Community = new byte[CardReader.EID_MAX_Community],
                Place = new byte[CardReader.EID_MAX_Place],
                Street = new byte[CardReader.EID_MAX_Street],
                HouseNumber = new byte[CardReader.EID_MAX_HouseNumber],
                HouseLetter = new byte[CardReader.EID_MAX_HouseLetter],
                Entrance = new byte[CardReader.EID_MAX_Entrance],
                Floor = new byte[CardReader.EID_MAX_Floor],
                ApartmentNumber = new byte[CardReader.EID_MAX_ApartmentNumber],
                AddressDate = new byte[CardReader.EID_MAX_AddressDate],
                AddressLabel = new byte[CardReader.EID_MAX_AddressLabel],
            };

            do
            {
                status = CardReader.EidReadVariablePersonalData(ref variablePersonalDataEID);
            }
            while (status != CardReader.EID_OK);

            CardReader.EidEndRead();

            VariablePersonalData variablePersonalData = new VariablePersonalData();

            map(variablePersonalDataEID, variablePersonalData);

            return Ok(variablePersonalData);
        }

        [Route("portrait")]
        public ActionResult getPortrait()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            int status;

            int pnCardVersion = 0;

            do
            {
                status = CardReader.EidBeginRead("", ref pnCardVersion);
            }
            while (status != CardReader.EID_OK);

            PortraitEID portraitEID = new PortraitEID()
            {
                PortraitData = new byte[CardReader.EID_MAX_Portrait]
            };

            do
            {
                status = CardReader.EidReadPortrait(ref portraitEID);
            }
            while (status != CardReader.EID_OK);

            CardReader.EidEndRead();

            return Ok(new { PortraitData = "data:image;base64," + Convert.ToBase64String(portraitEID.PortraitData) });
        }

        private void map(object src, object dest)
        {
            foreach(FieldInfo prop in src.GetType().GetFields())
            {
                if (prop.FieldType == typeof(byte[]))
                {
                    dest.GetType().GetProperty(prop.Name).SetValue(dest, decode((byte[])src.GetType().GetField(prop.Name).GetValue(src), (int)src.GetType().GetField(prop.Name + "Size").GetValue(src)));
                }
            }
        }

        private string decode(byte[] bajtovi, int vel)
        {
            return Encoding.UTF8.GetString(bajtovi, 0, vel);
        }
    }
}