#region Copyright notice
/*
 *      This is a as-is implementation and feel free to use the code (certainly with necessary modifications)
 * to fulfil any custom requrements.
 *      It would be well appreciated if you just keep the copyright notice intact. 
 * 
 *                                                                              Moim Hossain
 *                                                                              Sr. Software Engineer
 *                                                                              Orion Informatics.
 */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AssemblyRegUtil.Supports
{
    /// <summary>
    /// 
    /// </summary>
    public class Logger
    {
        /// <summary>
        ///     Keep log into the file system
        /// </summary>
        /// <param name="p"></param>
        public static void WriteLog(string p)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(@"c:\log.log", true))
                {
                    sw.WriteLine(p);
                }
            }
            catch { }
        }
    }
}
