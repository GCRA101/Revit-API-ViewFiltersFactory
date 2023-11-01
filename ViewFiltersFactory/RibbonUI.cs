/* IMPORT LIBRARIES */
using System;
// Libraries for Revit
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using System.IO;
using System.Reflection;

namespace ViewFiltersFactory
{
    [Transaction(TransactionMode.Manual)]
    public class RibbonUI : IExternalApplication
    {
        /* ATTRIBUTES */
        //private String projectFolderPath = System.IO.Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName;
        private String projectFolderPath = "c:\\Users\\galbieri\\OneDrive - Buro Happold\\Structures\\10_Computational\\HOI\\Revit API Plugins\\ViewFiltersFactory\\ViewFiltersFactory\\";

        private RibbonPanel ribbonPanel;


        /* CONSTRUCTOR */
        //Default
        public RibbonUI() {}


        /* METHODS */

        // OnStartup - Implemented from IExternalApplication Interface
        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                //1. Create new RibbonTab
                application.CreateRibbonTab("BH Plugins");
                //2. Create new RibbonTab Panel
                ribbonPanel = RibbonTabPanelFactory.getInstance().create(application, "BH Plugins", "View Filters");
                //3. Buildup Inputs for RibbonItemFactory
                String imagePath = "ViewFiltersFactory.AppLogo64x64.png";
                String largeImagePath = "ViewFiltersFactory.AppLogo96x96.png";
                String toolTipImagePath = "ViewFiltersFactory.AppLogo.png";
                String toolTipText = "View Filters Factory";
                String longDescriptionFilePath = "ViewFiltersFactory.AppLongDescription.txt";
                String longDescription = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(longDescriptionFilePath)).ReadToEnd();
                String assemblyFullPath = projectFolderPath + "bin\\Debug\\ViewFiltersFactory.dll";
                String className = "ViewFiltersFactory.Command";
                //4. Create RibbonItem (PushButton)
                RibbonItemFactory.getInstance().create(ribbonPanel, RibbonItemType.PushButton, "View Filters\n Factory", imagePath,
                                                       largeImagePath, toolTipImagePath, toolTipText, longDescription, assemblyFullPath,
                                                       className);
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                return Result.Failed;
            }
        }



        // OnShutdown - Implemented from IExternalApplication Interface
        public Result OnShutdown(UIControlledApplication application)
        {
            try
            {
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                return Result.Failed;
            }
        }
    }


}
