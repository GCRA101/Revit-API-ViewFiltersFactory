/* IMPORT LIBRARIES */
using System;
using System.Collections.Generic;


namespace ViewFiltersFactory
{
    public class ColorsFactory
    {
        /* ATTRIBUTES */
        // Private static instance - SINGLETON PATTERN
        private static ColorsFactory instance;

        /* CONSTRUCTORS */
        // Private Default Constructor - SINGLETON PATTERN
        private ColorsFactory() { }


        /* METHODS */

        // Public Static getInstance() Method - SINGLETON PATTERN
        public static ColorsFactory getInstance()
        {
            if (instance == null) { return new ColorsFactory(); }
            return instance;
        }

        //Public create Method - FACTORY PATTERN
        public List<Autodesk.Revit.DB.Color> create(ColorPalette colorPalette, int colorsNum)
        {
            switch (colorPalette)
            {
                case ColorPalette.RAINBOW: return this.rainbowColors(colorsNum);
                case ColorPalette.RANDOM: return this.randomColors(colorsNum);
                default: return null;
            }
        }

        /*Private rainbowColors Method
          Private as it doesn't need to be visible to the client
          Create a list of Revit DB color objects in a rainbow type pattern*/
        private List<Autodesk.Revit.DB.Color> rainbowColors(int colorsNum)
        {
            List<Autodesk.Revit.DB.Color>colors = new List<Autodesk.Revit.DB.Color> ();
            
            for (int i = 0; i < colorsNum; i++){
                byte red = (byte)(127 * Math.Sin(0.3 * i) + 128);
                byte green = (byte)(127 * Math.Sin(0.3 * i + 2) + 128);
                byte blue = (byte)(127 * Math.Sin(0.3 * i + 4) + 128);
                colors.Add(new Autodesk.Revit.DB.Color(red, green, blue));}
        
            return colors;
        }

        /*Private randomColors Method
          Private as it doesn't need to be visible to the client
          Create a list of Revit DB color objects in a random type pattern*/
        private List<Autodesk.Revit.DB.Color> randomColors(int colorsNum)
        {
            List<Autodesk.Revit.DB.Color> colors = new List<Autodesk.Revit.DB.Color>();
            Random random = new Random();

            for (int i = 0; i < colorsNum; i++)
            {
                byte red = (byte)random.Next(0,256);
                byte green = (byte)random.Next(0, 256);
                byte blue = (byte)random.Next(0, 256);
                colors.Add(new Autodesk.Revit.DB.Color(red, green, blue));
            }

            return colors;
        }
    }


}
