using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Resources;
using System.Collections;
using System.Windows.Forms;

namespace Dynamic_Localization
{
    /// <summary>
    /// Describe your class quickly here.
    /// </summary>
    /// <remarks>
    /// Add more details here.
    /// </remarks>
    public class Language
    {
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }

        protected static string Path = HttpContext.Current.Server.MapPath("~/App_LocalResources/");

        private string fileName { get; set; }

        public Language(string name, string code)
        {
            this.LanguageName = name;
            this.LanguageCode = code;
            this.fileName = Path + LanguageCode + ".resx";
            CreateLangFile();
        }

        private void CreateLangFile()
        {
            if (!File.Exists(fileName))
            {
                (new FileInfo(Path)).Directory.Create();
                using (ResXResourceWriter rw = new ResXResourceWriter(fileName))
                {
                    rw.Generate();
                    rw.Close();
                }

            }
        }
        public void AddString (string Key, string Value)
        {
            if (!Exists(Key))
            {
                Hashtable resourceEntries = GetAllVals();
                resourceEntries.Add(Key, Value);
                ResXResourceWriter resourceWriter = new ResXResourceWriter(fileName);
                foreach (String key in resourceEntries.Keys)
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
                resourceWriter.Close();
            }
        }
        public void EditString (string Key, string Value)
        {
            if (Exists(Key))
            {
                Hashtable resourceEntries = GetAllVals();
                resourceEntries[Key] = Value;
                ResXResourceWriter resourceWriter = new ResXResourceWriter(fileName);
                foreach (String key in resourceEntries.Keys)
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
                resourceWriter.Close();
            }
        }
        public void DeleteString(string Key)
        {
            if (Exists(Key))
            {
                Hashtable resourceEntries = GetAllVals();
                resourceEntries.Remove(Key);
                ResXResourceWriter resourceWriter = new ResXResourceWriter(fileName);
                foreach (String key in resourceEntries.Keys)
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
                resourceWriter.Close();
            }
        }

        public static void AddString(string FilePath, string Key, string Value)
        {
            if (!Exists(FilePath, Key))
            {
                Hashtable resourceEntries =  GetAllVals(FilePath);
                resourceEntries.Add(Key, Value);
                ResXResourceWriter resourceWriter = new ResXResourceWriter(FilePath);
                foreach (String key in resourceEntries.Keys)
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
                resourceWriter.Close();
            }
        }
        public static void EditString(string FilePath, string Key, string Value)
        {
            if (Exists(FilePath, Key))
            {
                Hashtable resourceEntries = GetAllVals(FilePath);
                resourceEntries[Key] = Value;
                ResXResourceWriter resourceWriter = new ResXResourceWriter(FilePath);
                foreach (String key in resourceEntries.Keys)
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
                resourceWriter.Close();
            }
        }
        public static void DeleteString(string FilePath, string Key)
        {
            if (Exists(FilePath, Key))
            {
                Hashtable resourceEntries = GetAllVals(FilePath);
                resourceEntries.Remove(Key);
                ResXResourceWriter resourceWriter = new ResXResourceWriter(FilePath);
                foreach (String key in resourceEntries.Keys)
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
                resourceWriter.Close();
            }
        }
        public bool Exists(string Key)
        {
            //ResourceReader reader = new ResourceReader(LangCode);
            Hashtable resourceEntries = GetAllVals();
            return (resourceEntries.ContainsKey(Key));
        }
        public static bool Exists (string FilePath, string Key)
        {
            //ResourceReader reader = new ResourceReader(LangCode);
            Hashtable resourceEntries = GetAllVals(FilePath);
            return (resourceEntries.ContainsKey(Key));
        }

        public Hashtable GetAllVals()
        {

            ResXResourceReader reader = new ResXResourceReader(fileName);
            Hashtable resourceEntries = new Hashtable();

            if (reader != null)
            {
                //IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Value == null)
                        resourceEntries.Add(d.Key.ToString(), "");
                    else
                        resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                }
                reader.Close();
            }
            return resourceEntries;
        }

        public static Hashtable GetAllVals(string FilePath)
        {
            
            ResXResourceReader reader = new ResXResourceReader(FilePath);
            Hashtable resourceEntries = new Hashtable();

            if (reader != null)
            {
                //IDictionaryEnumerator id = reader.GetEnumerator();
                foreach (DictionaryEntry d in reader)
                {
                    if (d.Value == null)
                        resourceEntries.Add(d.Key.ToString(), "");
                    else
                        resourceEntries.Add(d.Key.ToString(), d.Value.ToString());
                }
                reader.Close();
            }
            return resourceEntries;
        }



    }
}