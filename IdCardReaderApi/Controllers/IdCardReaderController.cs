using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using IdCardReaderApi.EID;

namespace IdCardReaderApi.Controllers
{
    [Route("v1/id-card-reader")]
    public class IdCardReaderController : ControllerBase
    {
        /*
         * Omogucava da se radnje koje koriste citac kartica zastite od istovremenog pristupa 2 klijenta preko monitora.
         */
        private readonly IIdCardReaderWrapper _cardReaderWrapper;

        public IdCardReaderController(IIdCardReaderWrapper cardReaderWrapper)
        {
            
            
            _cardReaderWrapper = cardReaderWrapper ?? throw new ArgumentNullException(nameof(cardReaderWrapper));
        }

        /*
         * Vraca sve podatke sa licne karte. Ostale metode vracaju pojedinacne sekcije.
         */
        [Route("id-card-data")]
        public ActionResult getIdCardData()
        {
            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getIdCardData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("document-data")]
        public ActionResult getDocumentData()
        {
            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getDocumentData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("fixed-personal-data")]
        public ActionResult getFixedPersonalData()
        {
            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getFixedPersonalData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("variable-personal-data")]
        public ActionResult getVariablePersonalData()
        {
            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getVariablePersonalData(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }

        [Route("portrait")]
        public ActionResult getPortrait()
        {
            Monitor.Enter(_cardReaderWrapper);
            ActionResult result = _cardReaderWrapper.getPortrait(this);
            Monitor.Exit(_cardReaderWrapper);
            return result;
        }
    }
}