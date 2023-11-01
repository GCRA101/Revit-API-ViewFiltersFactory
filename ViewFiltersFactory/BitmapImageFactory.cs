using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ViewFiltersFactory
{
    internal class BitmapImageFactory
    {
        /* ATTRIBUTES */
        // Private Static Instance - SINGLETON PATTERN
        private static BitmapImageFactory instance;

        /* CONSTRUCTORS */
        // Default Private - SINGLETON PATTERN
        private BitmapImageFactory() { }


        /* METHODS */

        // Public Static .getInstance() Method - SINGLETON PATTERN
        public static BitmapImageFactory getInstance()
        {
            if (instance == null) { instance =new BitmapImageFactory(); }
            return instance;
        }

        // Public .create Method - FACTORY PATTERN
        public BitmapImage create(string filePath)
        {
            Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filePath);
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = imageStream;
            bitmapImage.EndInit();

            return bitmapImage;
        }


    }
}
