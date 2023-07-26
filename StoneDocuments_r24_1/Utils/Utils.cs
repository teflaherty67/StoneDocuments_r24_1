using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneDocuments_r24_1
{
    internal static class Utils
    {
        #region Elements

        internal static List<ElementId> GetElementIdsFromList(Document doc, List<Element> elemList)
        {
            List<ElementId> returnList = new List<ElementId>();

            foreach (Element curElem in elemList)
                returnList.Add(curElem.Id);

            return returnList;
        }

        internal static List<Element> GetElementsFromSchedule(Document doc, ViewSchedule curView)
        {
            IList<Element> elements = new List<Element>();
            FilteredElementCollector finalCollector = new FilteredElementCollector(doc, curView.Id);

            List<BuiltInCategory> builtInCats = new List<BuiltInCategory>();
            builtInCats.Add(BuiltInCategory.OST_Parts);
            builtInCats.Add(BuiltInCategory.OST_GenericModel);

            ElementMulticategoryFilter filter1 = new ElementMulticategoryFilter(builtInCats);
            finalCollector.WherePasses(filter1);

            return finalCollector.ToElements() as List<Element>;

        }

        internal static List<Element> GetElementsFromView(Document doc, View curView)
        {
            IList<Element> elements = new List<Element>();
            FilteredElementCollector finalCollector = new FilteredElementCollector(doc, curView.Id);

            List<BuiltInCategory> builtInCats = new List<BuiltInCategory>();
            builtInCats.Add(BuiltInCategory.OST_Parts);
            builtInCats.Add(BuiltInCategory.OST_GenericModel);

            ElementMulticategoryFilter filter1 = new ElementMulticategoryFilter(builtInCats);
            finalCollector.WherePasses(filter1);
            elements = finalCollector.ToElements();

            return finalCollector.ToElements() as List<Element>;
        }

        internal static FillPatternElement GetFillPatternByName(Document doc, string name)
        {
            FillPatternElement curFPE = null;

            curFPE = FillPatternElement.GetFillPatternElementByName(doc, FillPatternTarget.Drafting, name);

            return curFPE;
        }

        #endregion

        #region Ribbon

        internal static RibbonPanel CreateRibbonPanel(UIControlledApplication app, string tabName, string panelName)
        {
            RibbonPanel currentPanel = GetRibbonPanelByName(app, tabName, panelName);

            if (currentPanel == null)
                currentPanel = app.CreateRibbonPanel(tabName, panelName);

            return currentPanel;
        }

        internal static RibbonPanel GetRibbonPanelByName(UIControlledApplication app, string tabName, string panelName)
        {
            foreach (RibbonPanel tmpPanel in app.GetRibbonPanels(tabName))
            {
                if (tmpPanel.Name == panelName)
                    return tmpPanel;
            }

            return null;
        }

        #endregion

        #region Schedules

        internal static List<ViewSchedule> GetAllSchedulesOnSheet(Document curDoc, ViewSheet curSheet)
        {
            List<ViewSchedule> schedList = new List<ViewSchedule>();

            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc, curSheet.Id);
            curCollector.OfClass(typeof(ScheduleSheetInstance));

            //loop through views and check if schedule - if so then put into schedule list
            foreach (ScheduleSheetInstance curView in curCollector)
            {
                ViewSchedule curSched = curDoc.GetElement(curView.ScheduleId) as ViewSchedule;
                schedList.Add(curSched);
            }

            return schedList;
        }

        #endregion

        #region Views

        internal static List<View> GetAllViews(Document curDoc)
        {
            FilteredElementCollector m_colviews = new FilteredElementCollector(curDoc);
            m_colviews.OfCategory(BuiltInCategory.OST_Views);

            List<View> m_views = new List<View>();
            foreach (View x in m_colviews.ToElements())
            {
                m_views.Add(x);
            }

            return m_views;
        }

        internal static View GetViewByName(Document curDoc, string viewName)
        {
            List<View> viewList = GetAllViews(curDoc);

            //loop through views in the collector
            foreach (View curView in viewList)
            {
                if (curView.Name == viewName && curView.IsTemplate == false)
                {
                    return curView;
                }
            }

            return null;
        }

        internal static List<View> GetViews(Document curDoc)
        {
            List<View> m_returnList = new List<View>();

            FilteredElementCollector m_viewCollector = new FilteredElementCollector(curDoc)
                .OfCategory(BuiltInCategory.OST_Views);

            FilteredElementCollector m_sheetCollector = new FilteredElementCollector(curDoc)
                .OfCategory(BuiltInCategory.OST_Sheets);

            foreach (View curView in m_viewCollector)
            {
                if (curView.IsTemplate == false)
                {
                    if (Viewport.CanAddViewToSheet(curDoc, m_sheetCollector.FirstElementId(), curView.Id) == true)
                        m_returnList.Add(curView);
                }
            }

            return m_returnList;
        }

        #endregion

        #region Schedules

        internal static List<ViewSchedule> GetSchedules(Document curDoc)
        {
            List<ViewSchedule> m_schedList = new List<ViewSchedule>();

            FilteredElementCollector m_curCollector = new FilteredElementCollector(curDoc)
                .OfClass(typeof(ViewSchedule))
                .WhereElementIsNotElementType();

            //loop through views and check if schedule - if so then put into schedule list
            foreach (ViewSchedule curView in m_curCollector)
            {
                if (curView.ViewType == ViewType.Schedule)
                {
                    if (curView.IsTemplate == false)

                    m_schedList.Add((ViewSchedule)curView);
                }
            }

            return m_schedList;
        }

        #endregion

    }
}
