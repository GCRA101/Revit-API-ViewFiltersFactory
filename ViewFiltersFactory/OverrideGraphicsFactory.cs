/* IMPORT LIBRARIES */
// Libraries for Revit
using Autodesk.Revit.DB;


namespace ViewFiltersFactory
{
    public class OverrideGraphicsFactory
    {

        /* ATTRIBUTES */
        //Private instance - SINGLETON PATTERN
        private static OverrideGraphicsFactory instance;

        /* CONSTRUCTORS */
        //Private Default Constructor - SINGLETON PATTERN
        private OverrideGraphicsFactory() { }

        /* METHODS */

        //getInstance Method - SINGLETON PATTERN
        public static OverrideGraphicsFactory getInstance()
        {
            if (instance == null)
            { return new OverrideGraphicsFactory(); }
            return instance;
        }

        //create Method - FACTORY PATTERN
        public OverrideGraphicSettings create(ElementId patternId, Autodesk.Revit.DB.Color color)
        {
            //Create the OverrideGraphics by Properties
            OverrideGraphicSettings overrideGraphicsSettings = new OverrideGraphicSettings();
            Autodesk.Revit.DB.Color black = new Autodesk.Revit.DB.Color(0, 0, 0);
            overrideGraphicsSettings.SetCutLineColor(black);
            overrideGraphicsSettings.SetProjectionLineColor(black);
            overrideGraphicsSettings.SetCutBackgroundPatternId(patternId);
            overrideGraphicsSettings.SetCutBackgroundPatternColor(color);
            overrideGraphicsSettings.SetCutForegroundPatternId(patternId);
            overrideGraphicsSettings.SetCutForegroundPatternColor(color);
            overrideGraphicsSettings.SetSurfaceBackgroundPatternId(patternId);
            overrideGraphicsSettings.SetSurfaceBackgroundPatternColor(color);
            overrideGraphicsSettings.SetSurfaceForegroundPatternId(patternId);
            overrideGraphicsSettings.SetSurfaceForegroundPatternColor(color);

            return overrideGraphicsSettings;
        }

    }


}
