using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneDocuments_r24_1
{
    internal class clsWrapperTBlockType
    {
        public FamilySymbol TitleblockType { get; set; }
        public string FamilyAndType { get; set; }

        public clsWrapperTBlockType(FamilySymbol titleblocktype)
        {
            TitleblockType = titleblocktype;
            FamilyAndType = TitleblockType.FamilyName + " : " + TitleblockType.Name;
        }
    }
}
