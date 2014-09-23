using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace EravsePaymentGateway
{
    /// <summary>
    /// GENEL ENUM PARAMETRELERI
    /// </summary>
    public class PaymentEnums
    {

        /// <summary>
        /// KARTI TIPLERI
        /// </summary>
        public enum CardType
        {
            Visa = 0,
            Master = 1,
            Amex = 2,
            BankaKarti = 3
        }

        /// <summary>
        /// BANKA TANIMLARI
        /// </summary>
        public enum Banks
        {
            DefaultBank = 0,
            VakifBank = 1,
            AkBank = 2,
            GarantiBank = 3,
            FinansBank = 4,
            IsBank = 5,
            
        }

        public enum Currency
        {
            YTL = 0,
            USD = 1,
            EUR = 2,
            GBP = 3
        }

        /// <summary>
        /// GENEL 3D MODELLERİ
        /// </summary>
        public enum TreeDModels
        {
            _3D = 0,
            _3D_PAY = 1,
            _3D_FULL = 2,
            _3D_HALF = 3,
            _3D_OOS_PAY = 4,
            _3D_OOS_FULL = 5,
            _3D_OOS_HALF = 6,
            _OOS_PAY = 7

        }

        public enum EravseApiAuthCodes
        {
            Successed,
            NotValidUser,
            InputParameterError
            
        }

        public enum EravseReturnCode
        {
            ApiCodeNotValid = 9999,
            UserDoesNotExist = 9998,
            BankKodeNotValid = 9997
        }


        #region + + + Vakifbank Enums + + + 

        public enum VakifBankIslem
        {
            PRO = 0,//Provizyon,
            OPR = 1, //Onprovizyon,
            OPK = 2,//Onprovizyon kapama,
            IPT = 3,//İptal,
            IAD = 4,//İade,
            PSR = 5,//Puan Sorgu,
            PHR = 6,//Puan Harcama
            BUL = 7,//Kayıt Ara
            KKT = 8,// Kredi Kartı Test
            ICM = 9,// icmal
            SON = 10,// Günsonu
            REV = 11,// Reversal
            HRG = 12// Günlik Hareketler

        }
        public enum VakifBankIslemYeri
        {
            I = 0,//Internet,
            T = 1, //Telefon,
            W = 2, //WAP,
            S = 3, //Kurum Muhasebe,
            D = 4  // Diğer,

        }
        public enum VakifBankStatusCodes
        {
            _01
        }
        public enum VakifBankResponseCodes
        {

            _00,
            _02,
            _40,
            _42,
            _69,
            _68,
            _67,
            _66,
            _65,
            _64,
            _63,
            _62,
            _61,
            _60,
            _59,
            _90,
            _91,
            _92,
            _96,
            _97,
            _98,
            _99,
            _F2,
            _G0,
            _G5,
            _70,
            _71,
            _72,
            _73,
            _76,
            _75,
            _74,
            _80,
            _81,
            _83,
            _85,
            _86,
            _87,
            _88,
            _89
        }
        public enum VakifBankBkmCodes
        {
            _00,
            _01,
            _02,
            _03,
            _04,
            _05,
            _06,
            _07,
            _08,
            _11,
            _12,
            _13,
            _14,
            _15,
            _19,
            _21,
            _24,
            _25,
            _26,
            _27,
            _28,
            _30,
            _32,
            _33,
            _34,
            _38,
            _41,
            _43,
            _51,
            _52,
            _53,
            _54,
            _55,
            _57,
            _58,
            _61,
            _62,
            _65,
            _75,
            _76,
            _82,
            _91,
            _92,
            _96,
            _TO,
            _GP,
            _TB,
            _UP,
            _IP,
            _CS,
            _BG,
            _NA,
            _OI,
            _NI,
            _NS
        }

        #endregion


    }


    public class EravsePaymentParameters {


        public Guid CompanyCode { get; set; }
        public string Password { get; set; }
        public PaymentEnums.Banks Bank { get; set; }
        public string Pnr { get; set; }
        public PaymentEnums.Currency CurrencyCode { get; set; }
        public string TotalAmount { get; set; }
        public string CardNo { get; set; }
        public string Cvc { get; set; }
        public string LastValidYear { get; set; }
        public string LastValidMount { get; set; }
        public string CardOwner { get; set; }
        public int Installment { get; set; }
        public PaymentEnums.CardType CardType { get; set; }
        public string ClientIp { get; set; }


    }




    public class ReturnedValues
    {
        public string ReferanceNo { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }

    }

    public class CustomerInformation
    {
        public Guid CustomerId { get; set; }
        public string MerchandId { get; set; }

        public string BankStoreKey { get; set; }
        public string BankPassword { get; set; }

    }

    public class RequstParamater
    {
        /// <summary>
        /// Eravse Odeme Sistemi Firma Kodudur Eravsedan Alınır
        /// </summary>
        public Guid FirmaKod { get; set; }
        public string Password { get; set; }

        public PaymentEnums.Banks BankaKodu { get; set; }
        public string Pnr { get; set; }
        public PaymentEnums.Currency ParaBirimi { get; set; }
        public string ToplamTurar { get; set; }
        public int KartNo { get; set; }
        public int Cvc { get; set; }
        public string SonKullanma_Yili { get; set; }
        public string SonKullanma_Ayi { get; set; }
        public string KartSahibi { get; set; }
        public int? Taksit { get; set; }
        public PaymentEnums.CardType KartTipi { get; set; }
    }


    ///// <summary>
    ///// AKBANK ODEME BASE
    ///// </summary>
    //public class AkbankOdeme : IDisposable
    //{
    //    /// <summary>
    //    /// ODEMEYI GERCEKLESTİREN METHOD
    //    /// </summary>
    //    /// <param name="Params"></param>
    //    /// <returns></returns>
    //    public ReturnedValues OdemeYap(AkBankParamterBuilder Params)
    //    {

    //        return new ReturnedValues { };

    //    }

    //    /// <summary>
    //    /// ODEMEYI IPTAL EDEN METHOD 
    //    /// </summary>
    //    /// <param name="Params"></param>
    //    /// <returns></returns>
    //    public ReturnedValues OdemeIptalEt(AkBankParamterBuilder Params)
    //    {
    //        return new ReturnedValues { };
    //    }


    //    public void Dispose()
    //    {
    //        GC.SuppressFinalize(this);
    //    }
    //}

    //public class AkBankParamterBuilder
    //{
    //    public CustomerInformation CustomerInformation { get; set; }
    //}

    ////public class AkBankCancelParameterBuidler
    ////{
    ////}

    //public class AkBankBuilder : IDisposable
    //{

    //    public AkBankParamterBuilder Builder { get; set; }
    //      public AkbankOdeme Odeme { get; set; }
    //    public RequstParamater rParam { get; set; }


    //    public AkBankBuilder(AkBankParamterBuilder b, RequstParamater requestParams)
    //    {
         
    //        this.Builder = b;
    //        this.rParam = requestParams;

    //    }

    //    public ReturnedValues AkbankOdemeYap()
    //    {
    //        using (AkbankOdeme Odeme = new AkbankOdeme())
    //        {
    //            return Odeme.OdemeYap(Builder);
    //        }



    //    }
        
    //    public void Dispose()
    //    {
    //        GC.SuppressFinalize(this);
    //    }
    //}
    
    //public class GarantiBankParamterBuilder
    //{
    //}
    //public class FinansBankParamterBuilder
    //{
    //}
    //public class IsBankParamterBuilder
    //{
    //}
    [Serializable]
    public class GarantiBankasiResponse
    {
        public int Kod { get; set; }

        public string message { get; set; }
    }

    public class GarantiBankOdeme : IDisposable
    {
        public GarantiBankasiParameterBuidler _param { get; set; }
        public GarantiBankasiResponse _response { get; set; }

        public GarantiBankOdeme(GarantiBankasiParameterBuidler param)
        {

            

            this._param = param;
            this._param.SecurityData = SerializeProccess.GetSHA1(param.strProvisionPassword + param._strTerminalID).ToUpper();
            this._param.HashData = SerializeProccess.GetSHA1(param.strTerminalID + param.strOrderID + param.strAmount + param.strSuccessURL + param.strErrorURL + param.strType + param.strInstallmentCount + param.strStoreKey + param.SecurityData).ToUpper();
            this._param.strInstallmentCount = "";
            this._param.cardnumber = "4355084355084358";
            this._param.cardexpiredatemonth="01";
            this._param.cardexpiredateyear="16";
            this._param.cardcvv2 = "123";
            this._param.secure3dsecuritylevel = "3D";
    


          
        }

        public GarantiBankasiResponse GarantiOdemeYap()
        {
            GarantiBankasiResponse _response = new GarantiBankasiResponse();

           // string data = OdemeOlusutur();

            byte[] b = new byte[1500];

            string provizyonMesaji ="http://www.garantipos.com.tr/Admin/post.asp?"+OdemeOlusutur();
            b.Initialize();
          
            b = Encoding.UTF8.GetBytes(provizyonMesaji);

            WebRequest h1 = (WebRequest)HttpWebRequest.Create(provizyonMesaji);
            h1.Method = "POST";
  h1.ContentLength = b.Length;
            Stream dataStream = h1.GetRequestStream ();
            dataStream.Write(b, 0, b.Length);
            WebResponse response = h1.GetResponse();

            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, System.Text.Encoding.UTF8);
            string responseFromServer = reader.ReadToEnd().Replace("\n", "").Replace("\"", @"#");


            
           
            _response.message = responseFromServer.Contains("PARes") == true ?"Pares Mesajı":"";
           

            return _response;


        }

        public string OdemeOlusutur()
        {
            string _odeme_url = "";


            Dictionary<string, object> Dictionary = new Dictionary<string, object>();

            Dictionary = _param.GetType()
             .GetProperties(BindingFlags.Instance | BindingFlags.Public)
             .ToDictionary(prop => prop.Name, prop => prop.GetValue(_param, null));

           
            foreach (var item in Dictionary)
            {
                
                    _odeme_url += item.Key.ToString() + "=" + item.Value + "&";
             
            }



            _odeme_url = _odeme_url.TrimStart('&');
            _odeme_url = _odeme_url.TrimEnd('&');
            return _odeme_url;

        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class VakifBankOdeme : IDisposable
    {
        public VakifBankParamterBuilder _param { get; set; }
        public VakifBankResponse _response { get; set; }


        public VakifBankOdeme(VakifBankParamterBuilder param)
        {
            this._param = param;

        }





        public VakifBankResponse VakifOdemeYap()
        {
            byte[] b = new byte[1500];

            string provizyonMesaji = OdemeOlusutur();
            b.Initialize();

            b = Encoding.UTF8.GetBytes(provizyonMesaji);

            WebRequest h1 = (WebRequest)HttpWebRequest.Create(provizyonMesaji);
            h1.Method = "GET";

            Stream dataStream;
            WebResponse response = h1.GetResponse();

            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream, System.Text.Encoding.UTF8);
            string responseFromServer = reader.ReadToEnd().Replace("\n", "").Replace("\"", @"#");
            responseFromServer = responseFromServer.Replace('#', '"');




            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(responseFromServer));

            DataSet dt = new DataSet();
            dt.ReadXml(stream);


            DataTable dt1 = dt.Tables[0];
            VakifBankResponse _response = GetResponseFromCode(dt1.Rows[0]["Kod"].ToString(), dt1);

            reader.Close();
            dataStream.Close();
            response.Close();






            return _response;


        }

        private VakifBankResponse GetResponseFromCode(string p, DataTable dt1)
        {
            VakifBankResponse _response = new VakifBankResponse();
            if (p == "00")
            {
                _response.BKMKod = dt1.Rows[0]["BKMKod"].ToString();
                _response.Mesaj = BKMKoduDondur((PaymentEnums.VakifBankBkmCodes)Enum.Parse(typeof(PaymentEnums.VakifBankBkmCodes), "_" + dt1.Rows[0]["BKMKod"].ToString()));
                _response.Status = dt1.Rows[0]["Status"].ToString();
                _response.Tutar = dt1.Rows[0]["Tutar"].ToString();
                _response.ProvNo = dt1.Rows[0]["ProvNo"].ToString();
                _response.VBRef = dt1.Rows[0]["VBRef"].ToString();
                _response.Tarih = dt1.Rows[0]["Tarih"].ToString();

            }
            else 
            {
                _response.Kod = dt1.Rows[0]["Kod"].ToString();
                _response.Mesaj = ResponseKoduDondur((PaymentEnums.VakifBankResponseCodes)Enum.Parse(typeof(PaymentEnums.VakifBankResponseCodes), "_" + dt1.Rows[0]["Kod"].ToString()));
               // _response.VBRef = dt1.Rows[0]["VBRef"].ToString();
            }


            return _response;

        }

           

        public string OdemeOlusutur()
        {
            string _odeme_url = "https://subesiz.vakifbank.com.tr/vpos724v3/?";


            Dictionary<string, object> Dictionary = new Dictionary<string, object>();

            Dictionary = _param.GetType()
             .GetProperties(BindingFlags.Instance | BindingFlags.Public)
             .ToDictionary(prop => prop.Name, prop => prop.GetValue(_param, null));

        
            foreach (var item in Dictionary)
            {
                if (item.Key == "islem")
                {
                    _odeme_url += item.Key.ToString() + "=" + IslemTipiDondur((PaymentEnums.VakifBankIslem)item.Value).ToString()+"&";
                }
                else if (item.Key == "tutar")
                {
                    _odeme_url += item.Key.ToString() + "=" + item.Value+"&";
                }

                else if (item.Key == "islemyeri")
                {
                    _odeme_url +=  item.Key.ToString() + "=" + IslemyeriDondur((PaymentEnums.VakifBankIslemYeri)item.Value).ToString()+"&";

                }
                else if (item.Key == "ucaf")
                {
                    if (item.Value.ToString() != "")
                    {
                        _odeme_url += item.Key.ToString() + "=" + IslemyeriDondur((PaymentEnums.VakifBankIslemYeri)item.Value).ToString()+"&";
                    }
                }
                else
                {

                    _odeme_url += item.Key.ToString() + "=" + item.Value + "&";
                }
            }



            _odeme_url = _odeme_url.TrimStart('&');
            _odeme_url = _odeme_url.TrimEnd('&');
            return _odeme_url;

        }

        public string IslemyeriDondur(PaymentEnums.VakifBankIslemYeri IslemYeri)
        {
            string _islem_yeri = "";
            switch (IslemYeri)
            {
                case PaymentEnums.VakifBankIslemYeri.I:
                    _islem_yeri = "I";
                    break;
                case PaymentEnums.VakifBankIslemYeri.T:
                    _islem_yeri = "T";
                    break;
                case PaymentEnums.VakifBankIslemYeri.W:
                    _islem_yeri = "W";
                    break;
                case PaymentEnums.VakifBankIslemYeri.S:
                    _islem_yeri = "S";
                    break;
                case PaymentEnums.VakifBankIslemYeri.D:
                    _islem_yeri = "D";
                    break;
                default:
                    break;
            }
            return _islem_yeri;
        }

        public string IslemTipiDondur(PaymentEnums.VakifBankIslem IslemTipi)
        {
            string _islem_tipi = "";

            switch (IslemTipi)
            {
                case PaymentEnums.VakifBankIslem.PRO:
                    _islem_tipi = "PRO";
                    break;
                case PaymentEnums.VakifBankIslem.OPR:
                    _islem_tipi = "OPR";
                    break;
                case PaymentEnums.VakifBankIslem.OPK:
                    _islem_tipi = "OPK";
                    break;
                case PaymentEnums.VakifBankIslem.IPT:
                    _islem_tipi = "IPT";
                    break;
                case PaymentEnums.VakifBankIslem.IAD:
                    _islem_tipi = "IAD";
                    break;
                case PaymentEnums.VakifBankIslem.PSR:
                    _islem_tipi = "PSR";
                    break;
                case PaymentEnums.VakifBankIslem.PHR:
                    _islem_tipi = "PHR";
                    break;
                case PaymentEnums.VakifBankIslem.BUL:
                    _islem_tipi = "BUL";
                    break;
                case PaymentEnums.VakifBankIslem.KKT:
                    _islem_tipi = "KKT";
                    break;
                case PaymentEnums.VakifBankIslem.ICM:
                    _islem_tipi = "ICM";
                    break;
                case PaymentEnums.VakifBankIslem.SON:
                    _islem_tipi = "SON";
                    break;
                case PaymentEnums.VakifBankIslem.REV:
                    _islem_tipi = "REV";
                    break;
                case PaymentEnums.VakifBankIslem.HRG:
                    _islem_tipi = "HRG";
                    break;
                default:
                    break;
            }

            return _islem_tipi;

        }

        public string BKMKoduDondur(PaymentEnums.VakifBankBkmCodes BkmKodu)
        {
            string _bkm_kodu = "";

                        switch (BkmKodu)
            {
                case PaymentEnums.VakifBankBkmCodes._00:
                    _bkm_kodu = "ONAYLANDI";
                    break;
                case PaymentEnums.VakifBankBkmCodes._01:
                    _bkm_kodu = "BANKASINI ARAYINIZ";
                    break;
                case PaymentEnums.VakifBankBkmCodes._02:
                    _bkm_kodu = "KATEGORI YOK";
                    break;
                case PaymentEnums.VakifBankBkmCodes._03:
                    _bkm_kodu = "UYE KODU HATALI /TANIMSIZ";
                    break;
                case PaymentEnums.VakifBankBkmCodes._04:
                    _bkm_kodu = "KARTA EL KOYUNUZ / SAKINCALI";
                    break;
                case PaymentEnums.VakifBankBkmCodes._05:
                    _bkm_kodu = "RED / ONAYLANMADI/CVV HATALI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._06:
                    _bkm_kodu = "HATALI ISLEM";

                    break;
                case PaymentEnums.VakifBankBkmCodes._07:
                    _bkm_kodu = "KARTA EL KOYUNUZ";

                    break;
                case PaymentEnums.VakifBankBkmCodes._08:
                    _bkm_kodu = "KIMLIK KONTROLU / ONAYLANDI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._11:
                    _bkm_kodu = "V.I.P KODU / ONAYLANDI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._12:
                    _bkm_kodu = "HATALI ISLEM / RED";

                    break;
                case PaymentEnums.VakifBankBkmCodes._13:
                    _bkm_kodu = "HATALI MIKTAR / RED";

                    break;
                case PaymentEnums.VakifBankBkmCodes._14:
                    _bkm_kodu = "KART-HESAP NO HATALI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._15:
                    _bkm_kodu = "MUSTERI YOK";

                    break;
                case PaymentEnums.VakifBankBkmCodes._19:
                    _bkm_kodu = "ISLEMI TEKRAR GIR";

                    break;
                case PaymentEnums.VakifBankBkmCodes._21:
                    _bkm_kodu = "ISLEM YAPILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._24:
                    _bkm_kodu = "DOSYASINA ULASILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._25:
                    _bkm_kodu = "DOSYASINA ULASILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._26:
                    _bkm_kodu = "DOSYASINA ULASILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._27:
                    _bkm_kodu = "DOSYASINA ULASILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._28:
                    _bkm_kodu = "DOSYASINA ULASILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._30:
                    _bkm_kodu = "FORMAT HATASI (UYEISYERI)";

                    break;
                case PaymentEnums.VakifBankBkmCodes._32:
                    _bkm_kodu = "DOSYASINA ULASILAMADI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._33:
                    _bkm_kodu = "SURESI BITMIS/IPTAL KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._34:
                    _bkm_kodu = "SAHTE KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._38:
                    _bkm_kodu = "ŞIFRE AŞIMI / ELKOY";

                    break;
                case PaymentEnums.VakifBankBkmCodes._41:
                    _bkm_kodu = "KAYIP KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._43:
                    _bkm_kodu = "CALINTI KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._51:
                    _bkm_kodu = "YETERSIZ HESAP/DEBIT KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._52:
                    _bkm_kodu = "HESAP NO YU KONTROL EDIN";

                    break;
                case PaymentEnums.VakifBankBkmCodes._53:
                    _bkm_kodu = "HESAP YOK";

                    break;
                case PaymentEnums.VakifBankBkmCodes._54:
                    _bkm_kodu = "SURESI BITMIS KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._55:
                    _bkm_kodu = "SIFRE HATALI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._57:
                    _bkm_kodu = "HARCAMA RED/BLOKELI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._58:
                    _bkm_kodu = "TERM.TRANSEC. YOK";

                    break;
                case PaymentEnums.VakifBankBkmCodes._61:
                    _bkm_kodu = "CEKME LIMIT ASIMI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._62:
                    _bkm_kodu = "YASAKLANMIS KART";

                    break;
                case PaymentEnums.VakifBankBkmCodes._65:
                    _bkm_kodu = "LIMIT ASIMI/BORC BAKIYE VAR";

                    break;
                case PaymentEnums.VakifBankBkmCodes._75:
                    _bkm_kodu = "SIFRE TEKRAR ASIMI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._76:
                    _bkm_kodu = "KEY SYN. HATASI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._82:
                    _bkm_kodu = "CVV HATALI / RED";

                    break;
                case PaymentEnums.VakifBankBkmCodes._91:
                    _bkm_kodu = "BANKASININ SWICI ARIZALI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._92:
                    _bkm_kodu = "BANKASI BILINMIYOR";

                    break;
                case PaymentEnums.VakifBankBkmCodes._96:
                    _bkm_kodu = "BANKASININ SISTEMI ARIZALI";

                    break;
                case PaymentEnums.VakifBankBkmCodes._TO:
                    _bkm_kodu = "TIME OUT";

                    break;
                case PaymentEnums.VakifBankBkmCodes._GP:
                    _bkm_kodu = "GECERSIZ POS";

                    break;
                case PaymentEnums.VakifBankBkmCodes._TB:
                    _bkm_kodu = "TUTARI BÖLÜNÜZ";

                    break;
                case PaymentEnums.VakifBankBkmCodes._UP:
                    _bkm_kodu = "UYUMSUZ POS";

                    break;
                case PaymentEnums.VakifBankBkmCodes._IP:
                    _bkm_kodu = "IPTAL POS";

                    break;
                case PaymentEnums.VakifBankBkmCodes._CS:
                    _bkm_kodu = "CICS SORUNU";

                    break;
                case PaymentEnums.VakifBankBkmCodes._BG:
                    _bkm_kodu = "BİLGİ GİTMEDİ";

                    break;
                case PaymentEnums.VakifBankBkmCodes._NA:
                    _bkm_kodu = "NO AMEX";

                    break;
                case PaymentEnums.VakifBankBkmCodes._OI:
                    _bkm_kodu = "OKEY İPTAL OTOR.";

                    break;
                case PaymentEnums.VakifBankBkmCodes._NI:
                    _bkm_kodu = "İPTAL İPTAL EDİLEMEDİ";

                    break;
                case PaymentEnums.VakifBankBkmCodes._NS:
                    _bkm_kodu = "NO SESION(HAT YOK)";
                    break;
                default:
                    break;
            }


            return _bkm_kodu;

        }

        public string StatusKoduDondur(PaymentEnums.VakifBankStatusCodes StatusKodu)
        {
            string _status_code = "";
            switch (StatusKodu)
            {
                case PaymentEnums.VakifBankStatusCodes._01:
                    _status_code = " Eski kayıt. - Daha önceden aynı UYEREF ile gönderilmiş olan mesajı içeriyor ";
                    break;
                default:
                    break;
            }

            return _status_code;
        }

        public string ResponseKoduDondur(PaymentEnums.VakifBankResponseCodes ResponseKodu)
        {

            string _response_code = "";

            switch (ResponseKodu)
            {
                case PaymentEnums.VakifBankResponseCodes._00:
                    _response_code = "İşlem başarılı olarak gerçekleştirildi.";
                    break;
                case PaymentEnums.VakifBankResponseCodes._02:
                    _response_code = "Kartla ilgili problem";
                    break;
                case PaymentEnums.VakifBankResponseCodes._40:
                    _response_code = "İadesi denenen işlemin orijinali yok.";
                    break;
                case PaymentEnums.VakifBankResponseCodes._42:
                    _response_code = "Günlük iade limiti aşıldı.";
                    break;
                case PaymentEnums.VakifBankResponseCodes._69:
                    _response_code = "Eksik Parametre";
                    break;
                case PaymentEnums.VakifBankResponseCodes._68:
                    _response_code = "Hatalı İşlem Tipi";
                    break;
                case PaymentEnums.VakifBankResponseCodes._67:
                    _response_code = "Parametre uzunluklarında uyuşmazlık";
                    break;
                case PaymentEnums.VakifBankResponseCodes._66:
                    _response_code = "Numeric deger hatası";
                    break;
                case PaymentEnums.VakifBankResponseCodes._65:
                    _response_code = "Hatalı tutar / amount";
                    break;
                case PaymentEnums.VakifBankResponseCodes._64:
                    _response_code = "İşlem tipi taksit e uygun değil";
                    break;
                case PaymentEnums.VakifBankResponseCodes._63:
                    _response_code = "Request mesajinda illegal karakter var.";

                    break;
                case PaymentEnums.VakifBankResponseCodes._62:
                    _response_code = "Yetkisiz ya da tanımsız kullanıcı";

                    break;
                case PaymentEnums.VakifBankResponseCodes._61:
                    _response_code = "Hatalı Tarih";

                    break;
                case PaymentEnums.VakifBankResponseCodes._60:
                    _response_code = "Hareket Bulunamadi";

                    break;
                case PaymentEnums.VakifBankResponseCodes._59:
                    _response_code = "Gunsonu yapilacak hareket yok/GS Yapilmis";

                    break;
                case PaymentEnums.VakifBankResponseCodes._90:
                    _response_code = "Kayıt bulunamadı";

                    break;
                case PaymentEnums.VakifBankResponseCodes._91:
                    _response_code = "Begin Transaction error";

                    break;
                case PaymentEnums.VakifBankResponseCodes._92:
                    _response_code = "Insert Update Error";

                    break;
                case PaymentEnums.VakifBankResponseCodes._96:
                    _response_code = "DLL registration error";

                    break;
                case PaymentEnums.VakifBankResponseCodes._97:
                    _response_code = "IP Hatası";

                    break;
                case PaymentEnums.VakifBankResponseCodes._98:
                    _response_code = "H. Iletisim hatası";

                    break;
                case PaymentEnums.VakifBankResponseCodes._99:
                    _response_code = "DB Baglantı hatası";

                    break;
                case PaymentEnums.VakifBankResponseCodes._F2:
                    _response_code = "Terminal kapalı";

                    break;
                case PaymentEnums.VakifBankResponseCodes._G0:
                    _response_code = "02124736060`I ARAYINIZ";

                    break;
                case PaymentEnums.VakifBankResponseCodes._G5:
                    _response_code = "Terminal izni yok";

                    break;
                case PaymentEnums.VakifBankResponseCodes._70:
                    _response_code = "XCIP hatalı";

                    break;
                case PaymentEnums.VakifBankResponseCodes._71:
                    _response_code = "Üye İşyeri blokeli ya da tanımsız";

                    break;
                case PaymentEnums.VakifBankResponseCodes._72:
                    _response_code = "Tanımsız POS";

                    break;
                case PaymentEnums.VakifBankResponseCodes._73:
                    _response_code = "POS table update error";

                    break;
                case PaymentEnums.VakifBankResponseCodes._76:
                    _response_code = "Taksite kapalı";

                    break;
                case PaymentEnums.VakifBankResponseCodes._75:
                    _response_code = "Illegal State";

                    break;
                case PaymentEnums.VakifBankResponseCodes._74:
                    _response_code = "Hatalı taksit sayısı";

                    break;
                case PaymentEnums.VakifBankResponseCodes._80:
                    _response_code = "CAVV bilgisi hatalı";
                    break;
                case PaymentEnums.VakifBankResponseCodes._81:
                    _response_code = "Eksik güvenlik Bilgisi";

                    break;
                case PaymentEnums.VakifBankResponseCodes._83:
                    _response_code = "İptali deneyiniz";


                    break;
                case PaymentEnums.VakifBankResponseCodes._85:
                    _response_code = "Kayit Reversal Durumda";

                    break;
                case PaymentEnums.VakifBankResponseCodes._86:
                    _response_code = "Kayit Degistirilemez";

                    break;
                case PaymentEnums.VakifBankResponseCodes._87:
                    _response_code = "Kayit Iade Durumda";

                    break;
                case PaymentEnums.VakifBankResponseCodes._88:
                    _response_code = "Kayit Iptal Durumda";
                    break;
                case PaymentEnums.VakifBankResponseCodes._89:
                    _response_code = "Geçersiz kayıt";


                    break;
                default:
                    break;
            }


            return _response_code;

        }



        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }


    [Serializable]
    public class GarantiBankasiParameterBuidler
    {

        public string strMode{get;set;} 
        public string strApiVersion{get;set;} 
        public string strTerminalProvUserID {get;set;}
        public string strType {get;set;}
        public string strAmount {get;set;} //İşlem Tutarı 1.00 TL için 100 gönderilmeli
        public string strCurrencyCode {get;set;}
        public string strInstallmentCount {get;set;} //Taksit Sayısı. Boş gönderilirse taksit yapılmaz
        public string strTerminalUserID {get;set;}
        public string strOrderID {get;set;}
        public string strCustomeripaddress{get;set;} //Kullanıcının IP adresini alır
        public string strcustomeremailaddress {get;set;}
        public string strTerminalID {get;set;}//8 Haneli TerminalID yazılmalı.
        public string _strTerminalID {get;set;}
        public string strTerminalMerchantID{get;set;}  //Üye İşyeri Numarası
        public string strStoreKey {get;set;} //3D Secure şifresi
        public string strProvisionPassword{get;set;}  //TerminalProvUserID şifresi
        public string strSuccessURL{get;set;}
        public string strErrorURL{get;set;} 
        public string SecurityData{get;set;} 
        public string HashData{get;set;}

        public string cardnumber{get;set;}
        public string    cardexpiredatemonth{get;set;}
        public string    cardexpiredateyear{get;set;}
        public string   cardcvv2{get;set;}


        public string secure3dsecuritylevel { get; set; }
    }

    [Serializable]
public class VakifBankParamterBuilder
    {

        public string kullanici { get; set; }
        public string sifre { get; set; }
        public PaymentEnums.VakifBankIslem islem { get; set; }
        public string uyeno { get; set; }
        public string posno { get; set; }
        public string kkno { get; set; }//kkno
        public string gectar { get; set; } //gectar
        public string cvc { get; set; } //cvc
        public string tutar { get; set; } // 12 hane olacak son ıkı hane kurus  000000001050 10,50
        public string provno { get; set; }
        public string taksits { get; set; }
        public PaymentEnums.VakifBankIslemYeri islemyeri { get; set; }
        public string uyeref { get; set; }
        public string vbref { get; set; }
        public string khip { get; set; }
        public string xcip { get; set; }
        public string ucaf { get; set; }


    }


    [Serializable]
    public class VakifBankResponse
    {
        public string Kod { get; set; }
        public string Status { get; set; }
        public string Tutar { get; set; }
        public string ProvNo { get; set; }
        public string PPuan { get; set; }
        public string PPuanTL { get; set; }
        public string EPuan { get; set; }
        public string EPuanTL { get; set; }
        public string SPuan { get; set; }
        public string SPuanTL { get; set; }
        public string HPuan { get; set; }
        public string HPSatis { get; set; }
        public string HBPuan { get; set; }
        public string TaksitS { get; set; }
        public string Mesaj { get; set; }
        public string VBRef { get; set; }
        public string UyeRef { get; set; }
        public string BKMKod { get; set; }
        public string Tarih { get; set; }
        public string Islem { get; set; }



    }



    public static class SerializeProccess
    {
        public static string GetSHA1(string SHA1Data)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            string HashedPassword = SHA1Data;
            byte[] hashbytes = Encoding.GetEncoding("ISO-8859-9").GetBytes(HashedPassword);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            return GetHexaDecimal(inputbytes);
        }

        public static string GetHexaDecimal(byte[] bytes)
        {
            StringBuilder s = new StringBuilder();
            int length = bytes.Length;
            for (int n = 0; n <= length - 1; n++)
            {
                s.Append(String.Format("{0,2:x}", bytes[n]).Replace(" ", "0"));
            }
            return s.ToString();
        }

        /// <summary>
        /// Function to get object from byte array
        /// </summary>
        /// <param name="_ByteArray">byte array to get object</param>
        /// <returns>object</returns>
        public static object Deserialize(byte[] _ByteArray)
        {
            try
            {
                // convert byte array to memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream(_ByteArray);

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // set memory stream position to starting point
                _MemoryStream.Position = 0;

                // Deserializes a stream into an object graph and return as a object.
                return _BinaryFormatter.Deserialize(_MemoryStream);
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }



        /// <summary>
        /// Function to get byte array from a object
        /// </summary>
        /// <param name="_Object">object to get byte array</param>
        /// <returns>Byte Array</returns>
        public static byte[] Serialize(object _Object)
        {
            try
            {
                // create new memory stream
                System.IO.MemoryStream _MemoryStream = new System.IO.MemoryStream();

                // create new BinaryFormatter
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _BinaryFormatter
                            = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                // Serializes an object, or graph of connected objects, to the given stream.
                _BinaryFormatter.Serialize(_MemoryStream, _Object);

                // convert stream to byte array and return
                return _MemoryStream.ToArray();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            // Error occured, return null
            return null;
        }


        public static object FromXml(string Xml, System.Type ObjType)
        {

            XmlSerializer ser;
            ser = new XmlSerializer(ObjType);
            StringReader stringReader;
            stringReader = new StringReader(Xml);
            XmlTextReader xmlReader;
            xmlReader = new XmlTextReader(stringReader);
            object obj;
            obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();
            return obj;

        }

        public static string StripIllegalXMLChars(string filePath, string XMLVersion)
        {
            //Remove illegal character sequences
            string tmpContents = filePath; // File.ReadAllText(filePath, Encoding.UTF8);

            string pattern = String.Empty;
            switch (XMLVersion)
            {
                case "1.0":
                    pattern = @"#x((10?|[2-F])FFF[EF]|FDD[0-9A-F]|7F|8[0-46-9A-F]9[0-9A-F])";
                    break;
                case "1.1":
                    pattern = @"#x((10?|[2-F])FFF[EF]|FDD[0-9A-F]|[19][0-9A-F]|7F|8[0-46-9A-F]|0?[1-8BCEF])";
                    break;
                default:
                    throw new Exception("Error: Invalid XML Version!");
            }

            Regex regex = new Regex(pattern, RegexOptions.None);
            if (regex.IsMatch(tmpContents))
            {
                tmpContents = regex.Replace(tmpContents, String.Empty);
                File.WriteAllText(filePath, tmpContents, Encoding.UTF8);

            }
            return tmpContents;

        }

        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }




        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) { _row = row; }

            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }
        }





    }


}