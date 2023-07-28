using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneDocuments_r24_1
{
    public class clsSheetData
    {
        public string SheetNumber { get; set; }
        public string SheetName { get; set; }
        public Element Titleblock { get; set; }
        public View SelectedView { get; set; }
        public ViewSchedule SelectedSchedule { get; set; }
        public string SelectedCategory { get; set; }
        public string GroupName { get; set; }
    }
}
