using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace ComAssembly
{
    [Guid("6ED33382-46E8-41F7-9032-9BC9AB9C483D")]
    [ProgId("ComAssembly.MyCom")]
    public class MyCom
    {
        string local;

        public bool SettingBz(string imgUrl, string path)
        {
            bool isSet = false;
            try
            {
                local = SaveImageFromWeb(imgUrl, path);
                if (!string.IsNullOrEmpty(local))
                {

                    int temp = SystemParametersInfo(20, 0, local, 0);
                    isSet = true;
                }
            }
            catch (Exception)
            {

            }
            return isSet;


        }
        
        #region 保存web图片到本地
        /// <summary>
        /// 保存web图片到本地
        /// </summary>
        /// <param name="imgUrl">web图片路径</param>
        /// <param name="path">保存路径</param>
        /// <param name="fileName">保存文件名</param>
        /// <returns></returns>
        public  string SaveImageFromWeb(string imgUrl, string path)
        {
            if (path.Equals(""))
                throw new Exception("未指定保存文件的路径");
            string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);
            string defaultType = ".jpg";
            string[] imgTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string imgType = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("."));
            string imgPath = "";
            foreach (string it in imgTypes)
            {
                if (imgType.ToLower().Equals(it))
                    break;
                if (it.Equals(".bmp"))
                    imgType = defaultType;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";
            request.Timeout = 3000;

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            if (response.ContentType.ToLower().StartsWith("image/"))
            {
                byte[] arrayByte = new byte[1024];
                int imgLong = (int)response.ContentLength;
                int l = 0;


                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss");

                FileStream fso = new FileStream(path + fileName + imgType, FileMode.Create);
                while (l < imgLong)
                {
                    int i = stream.Read(arrayByte, 0, 1024);
                    fso.Write(arrayByte, 0, i);
                    l += i;
                }

                fso.Close();
                stream.Close();
                response.Close();
                imgPath = fileName + imgType;
                local = path + imgPath;
                return local;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region System Innerface
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
            int uAction,
            int uParam,
            string lpvParam,
            int fuWinIni
         );
        #endregion
    }
}
