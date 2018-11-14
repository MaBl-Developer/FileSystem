using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWhitFileSystem.FileIO
{
    public class FileSystem
    {
        public static ArrayList item = new ArrayList();
        public static string FilePath;
        Random rd = new Random();

        public FileSystem(string Path)
        {
            FilePath = Path;
        }

        private bool CheckID(int SID)
        {
            if(item.Contains(SID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AddID(int SID)
        {
            if (!item.Contains(SID))
            {
                item.Add(SID);
            }
        }

        private void RemoveID(int SID)
        {
            if(item.Contains(SID))
            {
                item.Remove(SID);
            }
        }

        #region CreatFile
        public int CreatFile()
        {
            int SeasonID = 0;
            string DateiPath = string.Empty;
            for (int i = 0; i < 9; i++)
            {
                SeasonID = CreatRandomID();
                if (!CheckID(SeasonID))
                {
                    AddID(SeasonID);
                    break;
                }
            }
            DateiPath = FilePath + @"\" + SeasonID.ToString();
            Console.WriteLine("Path2: " + DateiPath);
            if (File.Exists(DateiPath) == false)
            {
                FileInfo fileInfo = new FileInfo(DateiPath);
                FileStream fileStream = fileInfo.Create();
                fileStream.Close();

                if (File.Exists(DateiPath) == true)
                {
                    if (SetFile(DateiPath))
                    {
                        return SeasonID;
                    }
                    else
                    {
                        Console.WriteLine("SetFile Fehlgeschlagen");
                        return 0000;
                    }
                }
                else
                {
                    return 0000;
                }
            }
            else
            {
                Console.WriteLine("Datei Bereits vorhanden");
                return 0000;
            }
        }

        private bool SetFile(string DateiPath)
        {
            try
            {
                FileStream fileStream = File.Open(DateiPath, FileMode.Open, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);
                fileWriter.WriteLine("InGameName=");
                fileWriter.WriteLine("PID=");
                fileWriter.WriteLine("Level=");
                fileWriter.WriteLine("XP=");
                fileWriter.WriteLine("Gold=");
                fileWriter.WriteLine("Silber=");
                fileWriter.WriteLine("ThreadID=");
                fileWriter.WriteLine("IP=");
                fileWriter.WriteLine(";" + DateTime.Now.ToString());

                fileWriter.Flush();
                fileWriter.Close();
                fileStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetFile: " + ex.ToString());
                return false;
            }
        }

        private int CreatRandomID()
        {
            int SeasonID = 0;
            for (int i = 0; i < 10; i++)
            {
                SeasonID = SeasonID + rd.Next(100, 99999999);
            }
            return SeasonID;
        }
        #endregion

        #region SetValue
        public bool SetValue(int SID, string Key, string Value)
        {
            try
            {
                List<string> FileStrings = new List<string>();
                string DateiPfad = string.Empty;
                // Speicher in FileStrings alle werte
                if (CheckID(SID))
                {
                    DateiPfad = FilePath + @"\" + SID.ToString();
                    string[] DateiArray = File.ReadAllLines(DateiPfad);

                    foreach (var item in DateiArray)
                    {
                        if (item.Length <= 0) break;

                        FileStrings.Add(item);
                    }
                }

                //Sucht den Wert zum ändern
                int i = 0;
                foreach (var item in FileStrings)
                {
                    i++;
                    string[] SplitString = item.Split('=');
                    if (SplitString[0] == Key)
                    {
                        i--;
                        FileStrings[i] = SplitString[0] + "=" + Value;
                        break;
                    }
                }

                // Updatet die fiel
                FileStream fileStream = File.Open(DateiPfad, FileMode.Open, FileAccess.Write);
                StreamWriter fileWriter = new StreamWriter(fileStream);
                for (int i1 = 0; i1 < FileStrings.Count; i1++)
                {
                    fileWriter.WriteLine(FileStrings[i1]);
                }
                fileWriter.Flush();
                fileWriter.Close();
                fileStream.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetValue: " + ex.ToString());
                return false;
            }
        }
        #endregion

        #region GetValue
        public string GetValue(int SID, string Key)
        {
            try
            {
                List<string> FileStrings = new List<string>();
                string DateiPfad = string.Empty;
                // Speicher in FileStrings alle werte
                if (CheckID(SID))
                {
                    DateiPfad = FilePath + @"\" + SID.ToString();
                    string[] DateiArray = File.ReadAllLines(DateiPfad);

                    foreach (var item in DateiArray)
                    {
                        if (item.Length <= 0) break;

                        FileStrings.Add(item);
                    }
                }

                foreach (var item in FileStrings)
                {
                    string[] SplitString = item.Split('=');
                    if (SplitString[0] == Key)
                    {

                        return SplitString[1];
                    }
                }
                return "error";
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetValue: " + ex.ToString());
                return "error";
            }
        }
        #endregion

        #region Remove File
        public bool RemoveFile(int SID)
        {
            try
            {
                string DateiPath = FilePath + @"\" + SID.ToString();
                FileInfo fileInfo = new FileInfo(DateiPath);
                fileInfo.Delete();

                if (File.Exists(DateiPath) == false)
                {
                    Console.WriteLine("Datei wurde gelöscht: " + SID.ToString());
                    RemoveID(SID);
                    return true;
                }
                else
                {
                    Console.WriteLine("Datei konnte nicht gelöscht werden: " + SID.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RemoveFile: " + ex.ToString());
                return false;
            }
        }
        #endregion
    }
}
