using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using LOGManagementLib;

namespace WindowsFormLib
{
    public class CReadIniFile
    {
        #region API函数声明
        //返回0表示失败，非0为成功
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);

        //返回取得字符串缓冲区的长度  def-默认返回值
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion

        private readonly static string filePath = AppDomain.CurrentDomain.BaseDirectory + "clconfig.ini";
        /// <summary>
        /// 读取配置值
        /// </summary>
        /// <param name="section">节</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string ReadSectionData(string section, string key)
        {
            if (File.Exists(filePath))
            {
                StringBuilder retvalue = new StringBuilder(50);
                GetPrivateProfileString(section, key, "", retvalue, 50, filePath);
                return retvalue.ToString();
            }
            else
            {
                CLOGException.Trace("找不到配置文件-clconfig.ini，其路径：" + filePath);
            }
            return "";
        }
    }
}
