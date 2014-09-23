using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using EravsePaymentGateway;

namespace EravsePaymentGateway
{
    /// <summary>
    /// Summary description for EravseAkbankGateway
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EravseAkbankGateway : System.Web.Services.WebService
    {

        //[WebMethod]
        //public ReturnedValues AkbankMakePayment(RequstParamater RequestParams)
        //{
        ////    AkBankParamterBuilder _akbankParamBuilder = new AkBankParamterBuilder();


        ////    _akbankParamBuilder.CustomerInformation = GetEravseCustomerInfo(RequestParams);
        ////    //new CustomerInformation { CustomerId = 8, MerchandId = "532" };



        ////    AkBankBuilder builder = new AkBankBuilder(_akbankParamBuilder,RequestParams, null);

        //    return builder.Odeme.OdemeYap(_akbankParamBuilder) ;
        //}

        //private CustomerInformation GetEravseCustomerInfo(RequstParamater RequestParams)
        //{

        //    Guid customerId = RequestParams.FirmaKod;
        //    string Password = RequestParams.Password;

        //    CustomerInformation cus = new CustomerInformation { 
                
        //        MerchandId ="xxxx",
        //        CustomerId = Guid.NewGuid(),
        //        BankStoreKey ="kjjjjj",
        //        BankPassword = "465465"
            
        //    };

        //    return cus;

        //}

        //[WebMethod]
        //public ReturnedValues AkbankCancelPayment(RequstParamater RequestParams,AkBankCancelParameterBuidler CancelParameters)
        //{
        //    return new ReturnedValues { };
        //}
    }
}
