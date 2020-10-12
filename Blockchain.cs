using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FloSDK.Methods;
using FloSDK.Exceptions;
using System.Configuration;
using Newtonsoft.Json.Linq;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace Certificate_generation
{
    class Blockchain 
    {

        Response res = new Response();
        private static string address = "oNBJc2UYGKif3YN42LyPLUmWFRamLK2FoS";
        private string transtrackurl = "https://testnet.flocha.in/tx/";

        static string username = ConfigurationManager.AppSettings.Get("rpcusername");
        static string passcode = ConfigurationManager.AppSettings.Get("rpcpassword");
        static string url = ConfigurationManager.AppSettings.Get("wallet_url");
        static string port = ConfigurationManager.AppSettings.Get("wallet_port");

        string FileName = "D:\\Certificate_generation\\Certificate_generation\\Temporary QR\\temp.jpeg";
        RpcMethods RpcObj = new RpcMethods(username, passcode , url, port);

        internal Response MakeTransaction(Candidate candidate)
        {
            Response res = new Response();
        string message = "This is to certify that '" + candidate.name + "' has successfully completed the Web Design certification course at LearnUp Labs, Ranchi from 20th December 2019 to 24th December 2019";

            try
            {
                JObject jobj = JObject.Parse(RpcObj.SendToAddress(address, 0.00001M, "BLOCKCHAIN SUMMIT 2019 RANCHI", "BLOCKCHAIN SUMMIT 2019 RANCHI", false, false, 1, "UNSET", message));
                if (string.IsNullOrEmpty(jobj["error"].ToString()))
                {
                    transtrackurl += jobj["result"].ToString();
                    res.resp = true;
                    res.type = "Successful";
                    res.transID = jobj["result"].ToString();

                    MessagingToolkit.QRCode.Codec.QRCodeEncoder encoder = new MessagingToolkit.QRCode.Codec.QRCodeEncoder();
                    encoder.QRCodeScale = 8;
                    Bitmap bmp = encoder.Encode(transtrackurl);

                    bmp.Save(FileName, ImageFormat.Jpeg);



                }
                else
                {
                    res.resp = false;
                    res.type = "Response Error";
                }
            }
            catch (RpcInternalServerErrorException rpc_exp)
            {
                res.resp = false;
                res.type = "Rpc Server Error Exception";
            }
            catch (Exception exp)
            {
                res.resp = false;
                res.type = "General Exception";
            }

            return res;
        }
    }
}
