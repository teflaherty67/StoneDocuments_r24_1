#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using StoneDocuments_r24_1.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;

#endregion

namespace StoneDocuments_r24_1
{
    [Transaction(TransactionMode.Manual)]
    public class cmdScheduleSwap : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document curDoc = uidoc.Document;

            // put any code needed for the form here

            // create a list of all the schedules in the project
            List<string> namesSchedules = Utils.GetAllScheduleNames(curDoc);
            List<ViewSchedule> schedNames = Utils.GetSchedulesToUse(curDoc, namesSchedules);            

            // check current view - make sure it's a sheet
            ViewSheet curSheet;
            if (curDoc.ActiveView is ViewSheet)
            {
                curSheet = curDoc.ActiveView as ViewSheet;
            }
            else
            {
                TaskDialog.Show("Error", "Please make the active view a sheet");
                return Result.Failed;
            }

            // get schedule from sheet
            List<ViewSchedule> schedList = Utils.GetAllSchedulesOnSheet(curDoc, curSheet);

            // check if sheet has schedule
            if (schedList.Count == 0)
            {
                TaskDialog.Show("Error", "The current sheet does not have a schedule. Please select another sheet.");
                return Result.Failed;
            }

            // open form
            frmScheduleSwap curForm = new frmScheduleSwap(schedNames)
            {
                Width = 800,
                Height = 450,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // set some variables
            ElementId curSheetId = curSheet.Id;
            //ViewSchedule newSched = newSched.Id;
            
            // get the current schedule & it's location
            ScheduleSheetInstance curSched = Utils.GetScheduleOnSheet(curDoc, curSheet);

            XYZ schedLoc = curSched.Point;

            // delete the current schedule
            curDoc.Delete(curSched.Id);

            // add the new schedule at the same location point
            //ScheduleSheetInstance newSSI = ScheduleSheetInstance.Create(curDoc, curSheetId, newSched.Id, schedLoc);


            return Result.Succeeded;
        }
    }
    
}
