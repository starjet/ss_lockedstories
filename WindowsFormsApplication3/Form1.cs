using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MsgPack;
using LitJson;
using CsvHelper;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            string filename = "msgpackdata"; //application/x-msgpack data in response to POST /load/index
            byte[] data = File.ReadAllBytes(filename);
            DerestageClasses.Certification.encodedUdid = File.ReadAllText("encodedudid.txt"); //string in request header "UDID"
            string decodedResponse = derestageDecodeResponse(data);
            JsonData jd = JsonMapper.ToObject(decodedResponse);
            foreach (JsonData jd2 in jd["data"]["user_chara_list"])
            {
                dictionary.Add(jd2["chara_id"].ToString(), (int)jd2["fan"]);
            }
            TextReader tr = new StringReader(File.ReadAllText("story_detail.csv")); //story_detail.csv in master_story.bdb.lz4
            CsvReader cr = new CsvReader(tr);
            string stories_to_unlock = "";
            while (cr.Read())
            {
                if (cr.GetField<string>("sub_title").Contains("とのメモリアル") && cr.GetField<string>("is_release").Equals("1"))
                {
                    string[] s = cr.GetField<string>("sub_title").Split("とのメモリアル".ToCharArray());
                    int required_fans = 0;
                    switch (s.Last())
                    {
                        case "2":
                            required_fans = 2000;
                            break;
                        case "3":
                            required_fans = 10000;
                            break;
                        case "4":
                            required_fans = 20000;
                            break;
                        case "5":
                            required_fans = 30000;
                            break;
                        case "6":
                            required_fans = 40000;
                            break;
                        case "7":
                            required_fans = 50000;
                            break;
                        case "8":
                            required_fans = 60000;
                            break;
                        case "9":
                            required_fans = 70000;
                            break;
                        case "10":
                            required_fans = 80000;
                            break;
                    }
                    try
                    {
                        if (dictionary[cr.GetField<string>("open_chara_id")] < required_fans)
                        {
                            stories_to_unlock += cr.GetField<string>("sub_title") + " Required fans:" + required_fans.ToString() + " Available fans:" + dictionary[cr.GetField<string>("open_chara_id")].ToString() + "\r\n";
                        }
                    }
                    catch { }
                }
            }
            File.WriteAllText("stories_to_unlock_" + (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + ".txt", stories_to_unlock);
            Environment.Exit(0);
        }

        private string derestageDecodeResponse(byte[] data)
        {
            string encryptedString = Encoding.UTF8.GetString(data);
            string decryptedString = DerestageClasses.CryptAES.decrypt(encryptedString);
            byte[] msgpackData = Convert.FromBase64String(decryptedString);
            var u = Unpacking.UnpackObject(msgpackData);
            string plainText = u.Value.ToString();
            return plainText;
        }
    }
}
