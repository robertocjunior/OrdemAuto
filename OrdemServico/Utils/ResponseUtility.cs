using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using System.Net;

namespace StockService.Utils
{
    public class ResponseUtility
    {
        //public static IActionResult CreateResponse(ControllerBase oController, HttpStatusCode enumStatusCode, enumSituationResponse enumSituationResponse, string sMessage, bool bFlExceptionCustom = false)
        //{
        //    return CreateResponse(oController, enumStatusCode, enumSituationResponse, sMessage, null, bFlExceptionCustom);
        //}
        //public static IActionResult CreateResponse (ControllerBase oController, HttpStatusCode enumStatusCode, enumSituationResponse enumSituationResponse,  string sMessage, object Id, bool bFlExceptionCustom = false)
        //{
        //    #if DEBUG
        //    return oController.StatusCode((int)enumStatusCode, new DTOResponse { Status = enumSituationResponse, Message = sMessage, Id = Id});
        //    #endif
        //    return oController.StatusCode((int)enumStatusCode, new DTOResponse { Status = enumSituationResponse, Message = bFlExceptionCustom ? sMessage : "One or more error ocorred." , Id = Id });
        //}
    }
}
