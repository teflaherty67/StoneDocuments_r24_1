using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneDocuments_r24_1
{
    public class clsTBlockData
    {       
        public FamilySymbol SelectedTitleBlock { get; set; }        
        public string SelectedCategory { get; set; }
        public string GroupName { get; set; }
    }
}
