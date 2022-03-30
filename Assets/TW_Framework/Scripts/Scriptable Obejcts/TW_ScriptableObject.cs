
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

namespace TriggerWarning.XML
{
    using Parsers;
    
    public enum TriggerResponses
    {
        ignore = 0,
        warn = 1,
        skip = 2
    }
    
    public class TW_ScriptableObject : ScriptableObject
    {
        private Dictionary<string, TriggerResponses> Triggers;

        public bool isSimple = false;
        
        XmlSerializer ParsingXML;

        private string directory;
        private string fileName;
        private char directorySeperator;

        private void OnEnable()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform ==
                                                                      RuntimePlatform.WindowsEditor
                                                                      || Application.platform ==
                                                                      RuntimePlatform.XboxOne ||
                                                                      Application.platform ==
                                                                      RuntimePlatform.GameCoreXboxSeries)
            {
                directorySeperator = '\\';
            }
            else directorySeperator = '/';
            if (isSimple) ParseXMLSimple();
            else ParseXMLRich();
        }

        public void OnQuit()
        {
            if (isSimple) SaveXMLSimple();
            else SaveXMLRich();
        }
        
        private void ParseXMLSimple()
        {
            TW_XMLSimple s;
            ParsingXML = new XmlSerializer(typeof(TW_XMLSimple));
            StreamWriter writer = new StreamWriter(directory);
            s = (TW_XMLSimple)ParsingXML.Deserialize(writer.BaseStream);

            for (int i = 0; i < s.size; i++)
            {
                Triggers.Add(s.triggers[i], TriggerResponses.ignore);
            }
        }
        
        private void SaveXMLSimple()
        {
            string[] keys = Triggers.Keys.ToArray();
            
            TW_XMLSimple s = new TW_XMLSimple
            {
                triggers = keys
            };
            
            ParsingXML = new XmlSerializer(typeof(TW_XMLSimple));
            StreamWriter writer = new StreamWriter(directory + directorySeperator + fileName);
            ParsingXML.Serialize(writer.BaseStream, s);
        }

        private void ParseXMLRich()
        {
            TW_XMLRich s;
            ParsingXML = new XmlSerializer(typeof(TW_XMLRich));
            StreamWriter writer = new StreamWriter(directory);
            s = (TW_XMLRich)ParsingXML.Deserialize(writer.BaseStream);

            for (int i = 0; i < s.size; i++)
            {
                Triggers.Add(s.keys[i], s.values[i]);
            }

        }
        
        private void SaveXMLRich()
        {
            string[] ks = Triggers.Keys.ToArray();
            TriggerResponses[] responses = Triggers.Values.ToArray();
            
            TW_XMLRich r = new TW_XMLRich
            {
                keys = ks, values = responses
            };
            
            ParsingXML = new XmlSerializer(typeof(TW_XMLRich));
            StreamWriter writer = new StreamWriter(directory);
            ParsingXML.Serialize(writer.BaseStream, r);
        }
    }
}