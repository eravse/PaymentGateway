using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EravsePaymentGateway;

namespace EravsePaymentGateway
{
    public class EravseResponse
    {
        public PaymentEnums.EravseReturnCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }

}
