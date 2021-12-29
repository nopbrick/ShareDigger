using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ShareDig
{
    internal class Helper
    {
        public static SecureString ReadPassword()
        {
            SecureString password = new SecureString();
            ConsoleKeyInfo key;

            Console.Write("Enter password: ");
            do
            {
                key = Console.ReadKey(true);

                // Ignore any key out of range.
                if (((int)key.Key) >= 65 && ((int)key.Key <= 90))
                {
                    // Append the character to the password.
                    password.AppendChar(key.KeyChar);
                }
                // Exit if Enter key is pressed.
            } while (key.Key != ConsoleKey.Enter);
            return password;
        }
    }
}
