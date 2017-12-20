using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library
{
    public struct DesignPage
    {
        public uint width;
        public uint height;
        public EnumConstraint constraintWidth;
        public EnumConstraint constraintHeight;
        public CodeCSS cssPart;
        public CodeJavaScript javascriptPart;
        public CodeJavaScript onload;
        public bool cssOnFile;
        public string cssFile;
        public bool javascriptOnFile;
        public string javascriptFile;
        public List<string> javascriptFiles;
        public List<HorizontalZone> zones;
        public bool includeContainer;
        public List<HTMLObject> subObjects;
    }
}
