using System.Globalization;
using EravsePaymentGateway;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace EravsePaymentGateway
{
    /// <summary>
    /// Summary description for EravseVakifBankGateway
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EravsePaymentGateway : System.Web.Services.WebService
    {

        [WebMethod(Description = "Ödeme yap banka parametresine göre işlem yapmaktadır. Lütfen apide tanımlı bankalarınızdan birini gönderiniz.")]
        public VakifBankResponse VakifBankOdemeYap(EravsePaymentParameters eravseParameter)
        {

            VakifBankResponse response = null;
            if (eravseParameter.Bank != PaymentEnums.Banks.DefaultBank)
            {
                if (eravseParameter.Bank == PaymentEnums.Banks.VakifBank)
                {

                    response = new VakifBankResponse();

                }
              

                using (Datasets.GatewayContextDataContext db = new Datasets.GatewayContextDataContext())
                {

                    var User = db.PaymentCustomers.Where(r => r.UserGuid == eravseParameter.CompanyCode && r.Password == eravseParameter.Password);
                    if (User.Count() > 0)
                    {


                       
                            #region + + +  VAKIFBANK + + +


                            if (eravseParameter.Bank == PaymentEnums.Banks.VakifBank)
                            {
                                VakifBankParamterBuilder _builder = new VakifBankParamterBuilder();

                                Datasets.CustomerParameter customerParameter = db.CustomerParameters.First(r => r.CustomerRowId == User.First().UserGuid && r.Bank == (int)eravseParameter.Bank);
                                byte[] b = customerParameter.Parameters;

                                _builder =  (VakifBankParamterBuilder)SerializeProccess.Deserialize(b);

                                _builder.kkno = eravseParameter.CardNo;
                                _builder.gectar = eravseParameter.LastValidYear + eravseParameter.LastValidMount;
                                _builder.cvc = eravseParameter.Cvc.ToString(CultureInfo.InvariantCulture);
                                _builder.tutar = "000000000100";
                                _builder.khip = eravseParameter.ClientIp;
                                _builder.taksits = "0" + eravseParameter.Installment.ToString();

                                using (VakifBankOdeme odeme = new VakifBankOdeme(_builder))
                                {
                                    response = odeme.VakifOdemeYap();

                                }
                            }
                            #endregion
                           




                    }
                }
            }
            else
            {


                response = new VakifBankResponse
                {
                    Kod = PaymentEnums.EravseReturnCode.BankKodeNotValid.ToString(),
                    Mesaj = GetEravseResponseMessageFromReturnCode(PaymentEnums.EravseReturnCode.BankKodeNotValid)
                };


            }





            // LOGIN KONTROL 
            // EGER DB BAGLANTISI YAPACAKSANIZ ORNEK OLARAK VERILMISTIR.


            //Classes.VakifBankParamterBuilder builerder = new VakifBankParamterBuilder{

            //kullanici ="0001",
            //sifre ="00000000",
            //islem = PaymentEnums.VakifBankIslem.PRO,
            //uyeno = "000000000",
            //posno="00000000",
            //provno="000000",
            //islemyeri = PaymentEnums.VakifBankIslemYeri.I,
            //uyeref="200501011234567890",
            //vbref ="6527BB1815F9AB1DE864A488E5198663002D0000",
            //xcip ="ABABABABAB",
            //ucaf=""
            //};


            //builerder.kkno = EravseParameter.CardNo.ToString();
            //builerder.gectar = EravseParameter.LastValidYear.ToString() + EravseParameter.LastValidMount.ToString();
            //builerder.cvc = EravseParameter.CVC.ToString();
            //builerder.tutar = "000000000100";
            //builerder.khip = EravseParameter.ClientIP;

            //using (Datasets.GatewayContextDataContext db  = new Datasets.GatewayContextDataContext())
            //{
            //    Datasets.CustomerParameter cp = new Datasets.CustomerParameter { 
            //        Id = Guid.NewGuid(),
            //    CustomerRowId = Guid.Parse("46dd9004-39e2-4b15-aa55-3c85a4ce279a"),
            //    Bank = 1,
            //    Parameters =  Classes.SerializeProccess.Serialize(builerder)
            //    };

            //    db.CustomerParameters.InsertOnSubmit(cp);
            //    db.SubmitChanges();
            //}






            //using (Classes.VakifBankOdeme Odeme = new Classes.VakifBankOdeme(Param))
            //{
            //    return Odeme.VakifOdemeYap();

            //}


            return response;

        }

          [WebMethod]
        public GarantiBankasiResponse GarnatiBankOdemeYap(EravsePaymentParameters eravseParameter)
        {

            GarantiBankasiResponse _response = null;
            if (eravseParameter.Bank != PaymentEnums.Banks.DefaultBank)
            {
                if (eravseParameter.Bank == PaymentEnums.Banks.GarantiBank)
                {

                    _response = new GarantiBankasiResponse();

                }


                using (Datasets.GatewayContextDataContext db = new Datasets.GatewayContextDataContext())
                {

                    var User = db.PaymentCustomers.Where(r => r.UserGuid == eravseParameter.CompanyCode && r.Password == eravseParameter.Password);
                  //  if (User.Count() > 0)
                    //{



                      
                        #region + + +  GRANTİ BANKASI + + +

                       if (eravseParameter.Bank == PaymentEnums.Banks.GarantiBank)
                        {

                            GarantiBankasiParameterBuidler _builder = new GarantiBankasiParameterBuidler();
                            _response = new GarantiBankasiResponse();
                            _builder.strMode = "PROD";
                            _builder.strApiVersion = "v0.01";
                            _builder.strTerminalProvUserID = "PROVAUT";
                            _builder.strType = "sales";
                            _builder.strAmount = "100"; //İşlem Tutarı 1.00 TL için 100 gönderilmeli
                            _builder.strCurrencyCode = "949";
                            _builder.strInstallmentCount = ""; //Taksit Sayısı. Boş gönderilirse taksit yapılmaz
                            _builder.strTerminalUserID = "";
                            _builder.strOrderID = "deneme";
                            _builder.strCustomeripaddress = "";// Request.UserHostAddress; //Kullanıcının IP adresini alır
                            _builder.strcustomeremailaddress = "eravse@gmail.com";
                            _builder.strTerminalID = ""; //8 Haneli TerminalID yazılmalı.
                            _builder._strTerminalID = "";// + strTerminalID;
                            _builder.strTerminalMerchantID = ""; //Üye İşyeri Numarası
                            _builder.strStoreKey = ""; //3D Secure şifresi
                            _builder.strProvisionPassword = ""; //TerminalProvUserID şifresi
                            _builder.strSuccessURL = "~/Garanti3dResponse.aspx";
                            _builder.strErrorURL = "~/Garanti3dResponse.aspx";


                            using (GarantiBankOdeme Odeme = new GarantiBankOdeme(_builder))
                            {
                                _response = Odeme.GarantiOdemeYap();

                            }


                        }

                        #endregion

                        else if (eravseParameter.Bank == PaymentEnums.Banks.IsBank)
                        { }






                        // }

                        //else
                        //{

                        //    _response = new EravseResponse
                        //    {
                        //        ResponseCode = PaymentEnums.EravseReturnCode.UserDoesNotExist,
                        //        ResponseMessage = GetEravseResponseMessageFromReturnCode(PaymentEnums.EravseReturnCode.UserDoesNotExist)
                        //    };


                    //}
                }
            }
            else
            {


                _response = new GarantiBankasiResponse
                {
                    Kod = (int)PaymentEnums.EravseReturnCode.BankKodeNotValid,
                    message = GetEravseResponseMessageFromReturnCode(PaymentEnums.EravseReturnCode.BankKodeNotValid)
                };


            }






            return _response;

        }




        private string GetEravseResponseMessageFromReturnCode(PaymentEnums.EravseReturnCode eravseReturnCode)
        {
            string returnMessage = "";
            switch (eravseReturnCode)
            {
                case PaymentEnums.EravseReturnCode.ApiCodeNotValid:
                    returnMessage = "Api Code Not Valid";
                    break;
                case PaymentEnums.EravseReturnCode.UserDoesNotExist:
                    returnMessage = "User Not Find";
                    break;
                case PaymentEnums.EravseReturnCode.BankKodeNotValid:

                    returnMessage = "Bank Code Not Valid";
                    break;
                default:
                    break;
            }
            return returnMessage;

        }


    }
}
