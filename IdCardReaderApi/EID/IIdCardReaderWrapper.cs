using Microsoft.AspNetCore.Mvc;
using IdCardReaderApi.Controllers;

namespace IdCardReaderApi.EID
{
    public interface IIdCardReaderWrapper
    {
        public ActionResult getIdCardData(IdCardReaderController controller);
        public ActionResult getDocumentData(IdCardReaderController controller);
        public ActionResult getFixedPersonalData(IdCardReaderController controller);
        public ActionResult getVariablePersonalData(IdCardReaderController controller);
        public ActionResult getPortrait(IdCardReaderController controller);
    }
}
