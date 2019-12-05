using System;
using System.Collections; // The Hashtable is in the collections class
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; // The keys are in the Forms Class

namespace SnakeGame
{
    internal class Input
    {
        private static Hashtable keyTable = new Hashtable();

        // creating a new instance of Hashtable class
        // this class is used to optimize the keys inserted in it

        public static bool KeyPressed (Keys key)
        {
            // this function will return a key back to the class
            if (keyTable[key] == null)
            {
                // if the hashtable is empty then we return false
                return false;
            }
            // if the hashtable is not empty then we return true
            return (bool) keyTable[key];
        }

        // Detect if a keyboard button is pressed
        public static void ChangeState (Keys key, bool state)
        {
            keyTable[key] = state;
        }

    }
}
