using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace LocalRunClient
{
    class ClientComm
    {
        private HttpWebRequest _req;
        private HttpWebResponse _resp;
        private string _url;
        public ClientComm(string url)
        {

            _url = url;
        }
        public void send(string opt,string sdata)
        {
            try
            {
                _req = WebRequest.Create(_url) as HttpWebRequest;
                _req.Method = "POST";
                _req.ContentType = "application/x-www-form-urlencoded";
                byte[] data = Encoding.ASCII.GetBytes(
                    "opt=" + opt + "&data=" + sdata
                    );
                Stream stream = _req.GetRequestStream();
                stream.Write(data, 0, data.Length);
                _resp = _req.GetResponse() as HttpWebResponse;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        public string recv()
        {
            try
            {
                StreamReader reader = new StreamReader(_resp.GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }
        }
    }
}
