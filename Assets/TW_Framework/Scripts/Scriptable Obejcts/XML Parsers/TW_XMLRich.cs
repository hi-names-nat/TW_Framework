using System.Collections.Generic;
using System.Xml;

namespace TriggerWarning.XML.Parsers
{
    public class TW_XMLRich : TW_XMLBase
    {
        public uint size;
        public string[] keys;
        public TriggerResponses[] values;
    }
}