using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
/// <summary>
/// 프로그램내에서 쓰는 설정 값들(계정 정보, 토큰 등등)을 불러오는 클래스.
/// </summary>
namespace WemadeQA.Common
{
    static class ConfigData
    {
        // 저장된 설정 정보들
        public static Dictionary<string, string> programSettingData = new Dictionary<string, string>();

        // 초기화할때 설정 정보들을 넣어준다.
        public static void SetConfigData()
        {
            string josnText = ByteToString(GDF_DATA.Properties.Resources.ProgramSettingData);
            programSettingData = JsonConvert.DeserializeObject<Dictionary<string, string>>(josnText);
        }

        public static void SetConfigData(string jsonPath)
        {
            string currentPath = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + jsonPath;
            using (StreamReader file = File.OpenText(currentPath))
            {
                string josnText = file.ReadToEnd();
                programSettingData = JsonConvert.DeserializeObject<Dictionary<string, string>>(josnText);
            }
        }

        // 바이트 배열을 String으로 변환 
        private static string ByteToString(byte[] strByte)
        {
            string str = Encoding.Default.GetString(strByte); return str;
        }
        // String을 바이트 배열로 변환
        private static byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }
    }
}
