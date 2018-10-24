using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Text;

namespace Server_Kurs
{
    class Authorization
    {
        static string filename = "info.dat";

        static public string GetHashString(string s)
        {
            //переводим строку в байт-массив  
            byte[] bytes = Encoding.UTF8.GetBytes(s);

            //создаем объект для получения средств шифрования  
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;
        }

        static public bool Passed(string nick, string password)
        {
            try
            {
                FileStream fr = new FileStream(filename, FileMode.Open);
                BinaryReader br = new BinaryReader(fr);
                string data = nick + ":" + GetHashString(password);
                string hash;
                try
                {
                    while ((hash = br.ReadString()) != null)
                    {
                        if (hash == data)
                        {
                            br.Close();
                            fr.Close();
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    br.Close();
                    fr.Close();
                    return false;
                }
                br.Close();
                fr.Close();
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }

        static public void Add_User(string nick, string password)
        {
            try
            {
                FileStream fr = new FileStream(filename, FileMode.Append);
                BinaryWriter bw = new BinaryWriter(fr);
                string data = nick + ":" + GetHashString(password);
                bw.Write(data);
                bw.Close();
                fr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        static public void Add_User(string data)
        {
            try
            {
                FileStream fr = new FileStream(filename, FileMode.Append);
                BinaryWriter bw = new BinaryWriter(fr);
                bw.Write(data);
                bw.Close();
                fr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

       /* static public void Delete_User(string nick)
        {
            try
            {
                List<string> notes = new List<string>();
                FileStream fr = new FileStream(filename, FileMode.Open);
                BinaryReader br = new BinaryReader(fr);
                string inf;
                if (fr.Length > 0)
                {
                    try
                    {
                        while ((inf = br.ReadString()) != null)
                        {
                            string[] subinf = inf.Split(':');
                            if (subinf[0] == nick)
                            {
                                notes.Add(inf);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        br.Close();
                        fr.Close();
                    }
                    finally
                    {
                        fr = new FileStream(filename, FileMode.Truncate);
                        fr.Close();
                        foreach (var user in notes)
                        {
                            Add_User(user);
                        }
                    }
                }
                fr.Close();
            }
            catch (Exception)
            {
            }
        }*/

        static public bool Exist(string nick)
        {
            FileStream fr = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fr);
            string inf;
            try
            {
                while ((inf = br.ReadString()) != null)
                {
                    string[] subinf = inf.Split(':');
                    if (subinf[0] ==nick)
                    {
                        br.Close();
                        fr.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                br.Close();
                fr.Close();
                return false;
            }
            br.Close();
            fr.Close();
            return false;
        }
    }
}

