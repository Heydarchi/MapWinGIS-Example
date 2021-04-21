using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace GenerealClass
{
    public class ErrorLogClass
    {

        public ErrorLogClass()
        {
           // NotificationMsgQueue = new List<MyMessageClass>();

        }

        public static void LogError(Exception _excep, StackTrace SF)
        {
            try
            {
                List<string> lst = trace(_excep, SF);
                string _str = lst[0] + " " + lst[1] + " " + lst[2];            
                LogErrorAsXml(lst);
            }
            catch (Exception e1)
            {
                 List<string> lst = trace(e1, new StackTrace(true));
                 string _str = lst[0] + " " + lst[1] + " " + lst[2];
                 LogErrorAsXml(lst);
            }
        }

        public static bool WriteTextFile(string _path,string _txt)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(_path))
                {
                    writer.Write(_txt);
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true));
                return false;
            }
        }

        private static void LogErrorAsXml(List<string> msg)
        {
            string file_src = "";
            if(msg[1]!= null && msg[1].Trim()!="")
                file_src=Path.GetFileName(msg[1]);
            else if (msg[2] != null && msg[2].Trim() != "")
                file_src=Path.GetFileName(msg[2]);
            //return;
            try
            {
                //   id++;
                string name = "Log_Error_" + file_src + "_" + DateTime.Now.ToShortDateString().Replace('/', '_') + ".xml";
                if (!File.Exists(name))
                {
                    System.Xml.XmlWriterSettings settings = new XmlWriterSettings();
                    //settings.Async = true;

                    using (XmlWriter writer = XmlWriter.Create(name))
                    {
                        writer.WriteStartDocument();
                        writer.WriteStartElement("Error");

                        writer.WriteStartElement("MSG");

                        writer.WriteElementString("Function", msg[0]);
                        writer.WriteElementString("FileName", msg[1]);
                        writer.WriteElementString("FileName2", msg[2]);
                        writer.WriteElementString("LineNumber", msg[3]);
                        writer.WriteElementString("ColumnNumber", msg[4]);
                        writer.WriteElementString("Message", msg[5]);
                        writer.WriteElementString("DateTime", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

                        writer.WriteEndElement();
                    }
                }
                else
                {
                    XDocument xDocument = XDocument.Load(name);
                    XElement root = xDocument.Element("Error");
                    root.Add(new XElement("MSG",
                    new XElement("Function", msg[0]),
                    new XElement("FileName", msg[1]),
                    new XElement("FileName2", msg[2]),
                    new XElement("LineNumber", msg[3]),
                    new XElement("ColumnNumber", msg[4]),
                    new XElement("Message", msg[5]),
                    new XElement("DateTime", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString())));
                    xDocument.Save(name);
                }

            }
            catch (Exception ex) { ErrorLogClass.LogError(ex, new System.Diagnostics.StackTrace(true)); }

        }


        private static List<string> trace(Exception e1,StackTrace ST)
        {
            List<string> errLst = new List<string>();
            try
            {

                string fn = ST.GetFrame(0).GetFileName();
                string ln2 = ST.GetFrame(0).GetFileLineNumber().ToString();
                var st = new StackTrace(e1, true);
                int cnt = st.FrameCount;
                var frame1 = st.GetFrame(0);
                var line = frame1.GetFileLineNumber();

                errLst.Add(frame1.GetMethod().ToString() + " [ " + ST.GetFrame(0).GetMethod() +" ] ");
                errLst.Add(frame1.GetFileName());
                errLst.Add(fn);
                errLst.Add(line.ToString() + " [ " + ln2 +" ] ");
                errLst.Add(frame1.GetFileColumnNumber().ToString());
                errLst.Add(e1.Message.ToString());
                return errLst;
            }
            catch (Exception e2)
            {
                errLst.Add("trace");
                errLst.Add("?");
                errLst.Add("?");
                errLst.Add("?");
                errLst.Add("?");
                errLst.Add("trace Erro : " + e2.Message);
                return errLst;
            }
        }


    }
}
