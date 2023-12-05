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
using Forms = System.Windows.Forms;


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

            // get all the schedules in the project
            List<ViewSchedule> schedNames = Utils.GetAllSchedules(curDoc);            

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

            // get all schedules on sheet
            List<ViewSchedule> schedList = Utils.GetAllSchedulesOnSheet(curDoc, curSheet);

            // check if sheet has schedule
            if (schedList.Count == 0)
            {
                TaskDialog.Show("Error", "The current sheet does not have a schedule. Please select another sheet.");
                return Result.Failed;
            }

            // open form
            frmScheduleSwap curForm = new frmScheduleSwap(schedNames, schedList)
            {
                Width = 450,
                Height = 150,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };           

            if (curForm.ShowDialog() == false)
            {
                return Result.Failed;
            }

            // set some variables
            ElementId curSheetId = curSheet.Id;
            ViewSchedule curSched = curForm.cmbCurSchedules.SelectedItem as ViewSchedule;
            ViewSchedule newSched = curForm.cmbNewSchedules.SelectedItem as ViewSchedule;

            // get the current schedule & it's location
            ScheduleSheetInstance curSchedule = Utils.GetScheduleOnSheetByName(curDoc, curSheet, curSched);
            XYZ schedLoc = curSchedule.Point;

            // create & start a transaction
            using (Transaction t = new Transaction(curDoc))
            {
                t.Start("Swap Schedules");

                // delete the current schedule
                curDoc.Delete(curSched.Id);

                // add the new schedule at the same location point
                ScheduleSheetInstance newSSI = ScheduleSheetInstance.Create(curDoc, curSheetId, newSched.Id, schedLoc);

                t.Commit();
            }           

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }    
}
