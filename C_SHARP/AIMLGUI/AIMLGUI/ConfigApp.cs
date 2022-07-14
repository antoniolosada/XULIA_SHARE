using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using XULIA;

namespace AIMLGUI
{
    public class ConfigApp
    {
        const bool CFG_XML = true;
        private ConfigXml mCfg;
        public const int MAX_CLAVES_APP_SETTINGS = 50;
        public struct Claves
        {
            public string key;
            public string Value;
        };

        //public List<T> RecuperarSecciones()
        //{
        //    var result = (from configKey in ConfigurationManager.AppSettings.Keys.Cast<string>()
        //                  let configValue = ConfigurationManager.AppSettings[configKey]
        //                  select new
        //                  {
        //                      key = configKey,
        //                      value = configValue
        //                  }).ToList();


        //    return result;
        //}
        List<string> Log = new List<string>();

        public void IniCfg()
        {
            string fic;
            // Usar el path del ejecutable
            fic = Application.StartupPath + @"\XULIA.exe.config";
            // La clase debemos instanciarla indicando el path a usar
            // y opcionalmente si se guarda cada vez que se asigne un valor.
            mCfg = new ConfigXml(fic, true);
        }
        public Claves[] ReadAppSettings()
        {
            Claves Clave;
            Claves[] ArrayClaves = new Claves[MAX_CLAVES_APP_SETTINGS];
            int i = 0;

            foreach (string key in ConfigurationManager.AppSettings.Keys)
            {
                Clave = new Claves();
                Clave.key = key;
                Clave.Value = ConfigurationManager.AppSettings[key];

                ArrayClaves[i++] = Clave;
            }

            return ArrayClaves;
        }
        public List<string> ReadAppSettingsColectionKeys()
        {
            return (List<string>) ConfigurationManager.AppSettings.Keys.Cast<string>() ;
        }
        public string ReadAppSettingsKey(string key)
        {
            if (CFG_XML)
                return mCfg.GetValue("appSettings", key);
            else
                return ConfigurationManager.AppSettings[key];
        }

        public string ReadSectionKey(string section, string key)
        {
            try
            {
                if (CFG_XML)
                    return mCfg.GetValue(section, key);
                else
                    return ((Hashtable)ConfigurationManager.GetSection(section))[key].ToString();
            }
            catch (Exception e)
            {
                string error = "Error recuperando valor '" + key + "' en sección '" + section + "'";
                Log.Add(error);
                return "";
            }
        }

        public string ReadSectionKeySinError(string section, string key)
        {
            try
            {
                if (CFG_XML)
                    return mCfg.GetValue(section, key);
                else
                    return ((Hashtable)ConfigurationManager.GetSection(section))[key].ToString();
            }
            catch 
            {
                return "";
            }
        }

        public string ReadSectionValue(string section, string Value)
        {
            return ((Hashtable)ConfigurationManager.GetSection(section))[Value].ToString();
        }

        public Hashtable ReadSection(string section)
        {
            if (CFG_XML)
            {
                return mCfg.Claves(section);
            }
            else
                return (Hashtable)ConfigurationManager.GetSection(section);
        }
        public Hashtable ReadSection1(string seccion)
        {
            Hashtable h = new Hashtable();

            foreach (string key in ConfigurationManager.AppSettings.Keys)
            {
                if (Strings.InStr(key, seccion+".") == 1)
                    h.Add(key, ConfigurationManager.AppSettings[key]);
            }

            return h;
        }

        //public Claves[] RecuperarSeccion(string NombreSeccion)
        //{
        //    Claves Clave;
        //    Claves[] ArrayClaves = { };
        //    int i = 0;

        //    IDictionary valores;

        //    foreach (string key in ConfigurationManager.GetSection(NombreSeccion))
        //    {
        //        Clave = new Claves();
        //        Clave.key = key;
        //        Clave.Value = ConfigurationManager.AppSettings[key];

        //        ArrayClaves[i++] = Clave;
        //    }

        //    return ArrayClaves;
        //}
    }
}
