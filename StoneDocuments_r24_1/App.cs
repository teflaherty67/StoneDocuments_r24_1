#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Markup;

#endregion

namespace StoneDocuments_r24_1
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            // 1. Create ribbon tab
            try
            {
                app.CreateRibbonTab("Stone Documents");
            }
            catch (Exception)
            {
                Debug.Print("Tab already exists.");
            }

            // 2. Create ribbon panel 
            RibbonPanel panel01 = Utils.CreateRibbonPanel(app, "Stone Documents", "Parts");
            RibbonPanel panel02 = Utils.CreateRibbonPanel(app, "Stone Documents", "Sheets");

            // 3. Create button data instances
            clsButtonDataClass btnCheck = new clsButtonDataClass("toolCheck",
                "Check\rParts", cmdCheck.GetMethod(), Properties.Resources.Check_32,
                Properties.Resources.Check_16, "Check parts by schedule and override surface foreground color.");
            clsButtonDataClass btnReset = new clsButtonDataClass("toolReset",
                "Reset\rParts", cmdReset.GetMethod(), Properties.Resources.Clear_32,
                Properties.Resources.Clear_16, "Clears surface foreground color override.");
            clsButtonDataClass btnSchedSwap = new clsButtonDataClass("toolSscheduleSwapper",
                "Schedule\rSwapper", cmdScheduleSwap.GetMethod(), Properties.Resources.ScheduleSwap_32,
                Properties.Resources.ScheduleSwap_16, "Schedule Swapper.");
            clsButtonDataClass btnSheetMakr = new clsButtonDataClass("toolSheetMaker",
                "Sheet\rMaker", cmdSheetMaker.GetMethod(), Properties.Resources.SheetMaker_32,
                Properties.Resources.SheetMaker_16, "Batch sheet creator.");

            // 4. Create buttons
            PushButton btnCheckParts = panel01.AddItem(btnCheck.Data) as PushButton;
            PushButton btnResetParts = panel01.AddItem(btnReset.Data) as PushButton;
            PushButton btnScheduleSwap = panel02.AddItem(btnSchedSwap.Data) as PushButton;
            PushButton btnSheetMaker = panel02.AddItem(btnSheetMakr.Data) as PushButton;
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }


    }
}
