using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace LMSweb.Service
{
    public class TextIO
    {
        
        public bool SaveFile(string FileName, string content)
        {
            string discPath = WebConfigurationManager.AppSettings["CodePath"];
            string PathRoot = HttpContext.Current.Server.MapPath(discPath);
            string FileType = ".txt";
            string PathTarget = PathRoot + FileName + FileType;

            if (content != null && FileName != null)
            {
                File.WriteAllText(PathTarget, content, Encoding.UTF8);
                return true;
            }

            return false;

        }
        public string readCodeText(string path)
        {
            string content;
            try
            {
                StreamReader streamReader = new StreamReader(path);
                content = streamReader.ReadToEnd();
                streamReader.Close();
                return content;
            }
            catch (IOException)
            {
                return "";
            }
        }
    }
}