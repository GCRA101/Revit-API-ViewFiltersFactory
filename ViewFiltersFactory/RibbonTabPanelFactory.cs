/* IMPORT LIBRARIES */
using System;
// Libraries for Revit
using Autodesk.Revit.UI;



namespace ViewFiltersFactory
{
    public class RibbonTabPanelFactory
    {
        /* ATTRIBUTES */
        // Private Static Instance - SINGLETON PATTERN
        private static RibbonTabPanelFactory instance;

        /* CONSTRUCTORS */
        // Default Private - SINGLETON PATTERN
        public RibbonTabPanelFactory() { }

        /* METHODS */

        // Public Static .getInstance() Method - SINGLETON PATTERN
        public static RibbonTabPanelFactory getInstance()
        {
            if (instance == null) { instance = new RibbonTabPanelFactory(); }
            return instance;
        }

        // Public .create Method - FACTORY PATTERN
        public RibbonPanel create(UIControlledApplication application, String tabName, String panelName)
        {
            return application.CreateRibbonPanel(tabName, panelName);
        }
    }


}
