using FtpServerUI.AppCode.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpServerUI.AppCode.Controllers
{
    public class BaseController
    {
        protected JsonRequest JsonRequest { get; set; }
        public JsonResponse JsonResponse { get; set; }
        public string MessageResult { get; set; }
        public string Username { get; set; }
        public BaseController()
        {
            JsonResponse = new JsonResponse();
        }
        public void SetJsonRequest(JsonRequest jsonRequest)
        {
            JsonRequest = jsonRequest;
        }

    }
}
