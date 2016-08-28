using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK.Graphics.OpenGL;

namespace _0x001.Dev.sanderele1
{
    public static class Error
    {

        /// <summary>
        /// Will print an error to console with the specified error code.
        /// NOTE: Will not print anything if the errorcode is <see cref="ErrorCode.NoError"/>! 
        /// </summary>
        /// <param name="code"></param>
        public static void Print(ErrorCode code)
        {
            if (code == ErrorCode.NoError)
                return;

            Console.WriteLine("An error occured with the code: " + Enum.GetName(typeof(ErrorCode), code) + ", stacktrace: ");
            Console.WriteLine(Environment.StackTrace);
        }

    }
}
