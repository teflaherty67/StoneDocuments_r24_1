﻿#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
#endregion

namespace StoneDocuments_r24_1
{
    [Transaction(TransactionMode.Manual)]
    public class cmdReset : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            string userName = uiapp.Application.Username;

            // set current view to 3D view
            View curView;

            if (doc.IsWorkshared == true)
                curView = Utils.GetViewByName(doc, "{3D - " + userName + "}");
            else
                curView = Utils.GetViewByName(doc, "{3D}");

            // get all elements in view
            List<Element> viewElements = Utils.GetElementsFromView(doc, curView);

            // set override settings
            OverrideGraphicSettings colSet = new OverrideGraphicSettings();

            // update element overrides in view
            using (Transaction t = new Transaction(doc))
            {
                t.Start("Reset elements");

                foreach (Element curElem in viewElements)
                {
                    doc.ActiveView.SetElementOverrides(curElem.Id, colSet);
                }

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
