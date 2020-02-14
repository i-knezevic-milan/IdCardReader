using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.EID;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("v1/card-reader-api")]
    public class CardReaderController : ControllerBase
    {
        private readonly ICardReaderWrapper _cardReaderWrapper;

        public CardReaderController(ICardReaderWrapper cardReaderWrapper)
        {
            _cardReaderWrapper = cardReaderWrapper ?? throw new ArgumentNullException(nameof(cardReaderWrapper));
        }

        [Route("document-data")]
        public ActionResult getDocumentData()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getDocumentData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("fixed-personal-data")]
        public ActionResult getFixedPersonalData()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getFixedPersonalData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("variable-personal-data")]
        public ActionResult getVariablePersonalData()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getVariablePersonalData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("portrait")]
        public ActionResult getPortrait()
        {
            HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getPortrait(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }
    }
}