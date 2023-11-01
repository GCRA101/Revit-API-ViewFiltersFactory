/* IMPORT LIBRARIES */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Libraries for UI Buttons/Panels
using System.Reflection;
using System.Xaml;
// Libraries for Revit
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
// Libraries for ETABS
using ETABSv1;
// Other Libraries
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
using Autodesk.Revit.Creation;
using System.Security.Cryptography;


namespace ViewFiltersFactory
{
    /* COMMAND CLASS ************************************************************ */

    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        /*ATTRIBUTES*/
        private Autodesk.Revit.UI.UIApplication uiApp;
        private Autodesk.Revit.UI.UIDocument uiDoc;
        private Autodesk.Revit.DB.Document doc;


        /*IMPLEMENTED METHODS*/

        // Execute Method from IExternalCommand Interface
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                //1. CREATE DB AND UI DOCUMENT OBJECTS
                //Assignment
                this.uiDoc = commandData.Application.ActiveUIDocument;
                this.doc = uiDoc.Document;
                //2. CALL COMMAND FUNCTION
                createNewFilter();
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                return Result.Failed;
            }
        }

        /* METHODS */

        //createNewFilters()
        private void createNewFilter()
        {
            Transaction revitTransaction = new Transaction(doc, "View Filters Factory");

            try
            {

                //1. GET CATEGORIES LIST

                List<BuiltInCategory> categoriesList = new List<BuiltInCategory> {
                    BuiltInCategory.OST_StructuralColumns,
                    BuiltInCategory.OST_StructuralFraming/*,
                    BuiltInCategory.OST_StructuralFramingOther,
                    BuiltInCategory.OST_StructuralFramingSystem*/};

                List<ElementId> catIdsList = new List<ElementId>();

                catIdsList = categoriesList.
                                Select(builtInCat => new ElementId(builtInCat)).
                                ToList();


                //2. GET MATERIALS LIST

                ElementMulticategoryFilter elMultiCatFilter = new ElementMulticategoryFilter(categoriesList);

                List<ElementId> selectedMaterialIds = new List<ElementId>();

                selectedMaterialIds = new FilteredElementCollector(doc).
                                        OfClass(typeof(Material)).
                                        Where(mat => mat.Name.Contains("Steel")).
                                        Select(mat => mat.Id).
                                        ToList();

                //3. GET FRAME SECTION NAMES

                List<String> sectionNames = new List<String>();

                sectionNames = new FilteredElementCollector(doc).
                                    OfClass(typeof(FamilyInstance)).
                                    WherePasses(elMultiCatFilter).
                                    Where(el => el.GetMaterialIds(false).Intersect(selectedMaterialIds).Count() != 0).
                                    Select(el => el.Name).
                                    ToHashSet().
                                    ToList();

                sectionNames.Sort();


                /*4.CREATE COLORS PALETTE FOR VIEW FILTERS */

                List<Autodesk.Revit.DB.Color> colors = new List<Autodesk.Revit.DB.Color>();
                colors=ColorsFactory.getInstance().create(ColorPalette.RAINBOW, sectionNames.Count());


                /*5. GET THE ACTIVE VIEW */

                Autodesk.Revit.DB.View activeView;
                activeView = doc.ActiveView;


                /*6. CREATE NEW VIEW FILTERS IN ACTIVE VIEW*/

                //Utility List Variables Declaration
                List<FilterRule> filterRules = new List<FilterRule>();
                List<OverrideGraphicSettings> overrideGraphicSettings = new List<OverrideGraphicSettings>();
                List<ParameterFilterElement> filters = new List<ParameterFilterElement>();
            
                //Start New Transaction
                revitTransaction.Start();

                /* REMOVE ALL FILTERS CURRENTLY ASSIGNED TO THE VIEW */
                activeView.GetFilters().ToList().ForEach(filter => activeView.RemoveFilter(filter));

                //Delete all View Filters already assigned to the active view
                //activeView.GetFilters().ToList().ForEach(elId => activeView.RemoveFilter(elId));

                for (int i = 0; i < sectionNames.Count(); i++)
                {
                    List<ElementFilter> elParamFilters = new List<ElementFilter>();
                    String sectionName = sectionNames[i];
                    if (ParameterFilterElement.IsNameUnique(doc, sectionName))
                    {
                        filters.Add(ParameterFilterElement.Create(doc, sectionName, catIdsList));
                        filterRules.Add(ParameterFilterRuleFactory.CreateEqualsRule(new ElementId(BuiltInParameter.ALL_MODEL_TYPE_NAME), sectionName, false));
                        elParamFilters.Add(new ElementParameterFilter(filterRules[i]));
                        filters[i].SetElementFilter(new LogicalAndFilter(elParamFilters));
                        activeView.AddFilter(filters[i].Id);}
                    else
                    {
                        activeView.AddFilter(new FilteredElementCollector(doc).
                            OfClass(typeof(ParameterFilterElement)).
                            ToElements().
                            Where(elFilter => elFilter.Name == sectionName).
                            Select(elFilter => elFilter.Id).
                            First());
                    }
                }

                // Get back the created View Filters
                List<ElementId> viewFilterIds = new List<ElementId>();
                viewFilterIds = (List<ElementId>)activeView.GetFilters();

                // Get the element id of the solid fill pattern
                FillPatternElement fillPattern = new FilteredElementCollector(doc)
                    .OfClass(typeof(FillPatternElement))
                    .Cast<FillPatternElement>()
                    .FirstOrDefault(pattern => pattern.Name.Contains("Solid fill"));

                // Assign OverrideGraphicSettings to all View Filters
                for (int i = 0; i < viewFilterIds.Count(); i++)
                {
                    overrideGraphicSettings.Add(OverrideGraphicsFactory.getInstance().create(fillPattern.Id, colors[i]));
                    activeView.SetFilterOverrides(viewFilterIds[i], overrideGraphicSettings[i]);
                }
                // Close and Dispose Transaction
                revitTransaction.Commit();
                revitTransaction.Dispose();

            } catch (Exception ex) {

                if (revitTransaction!= null)
                {
                    revitTransaction.RollBack();
                }

                TaskDialog.Show("ERROR MESSAGES", ex.Message);

                // Close and Dispose Transaction
                revitTransaction.Commit();
                revitTransaction.Dispose();}

            }

    }


}
