#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#endregion

namespace StoneDocuments_r24_1
{
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

            // set current view to 3D view
            View curView;
            curView = Utils.GetViewByName(doc, "{3D}");
            uidoc.ActiveView = curView;

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
    }
}
