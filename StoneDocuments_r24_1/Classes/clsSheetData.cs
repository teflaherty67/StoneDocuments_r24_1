﻿using Autodesk.Revit.DB;
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
        public View SelectedView { get; set; }
        public ViewSchedule SelectedSchedule { get; set; }       
    }
}
