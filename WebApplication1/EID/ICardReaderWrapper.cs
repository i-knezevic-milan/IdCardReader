using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Controllers;

namespace WebApplication1.EID
{
    public interface ICardReaderWrapper
    {
        public ActionResult getDocumentData(CardReaderController controller);
        public ActionResult getFixedPersonalData(CardReaderController controller);
        public ActionResult getVariablePersonalData(CardReaderController controller);
        public ActionResult getPortrait(CardReaderController controller);
    }
}
