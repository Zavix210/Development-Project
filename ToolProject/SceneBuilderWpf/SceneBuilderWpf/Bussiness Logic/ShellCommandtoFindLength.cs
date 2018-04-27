using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public static class ShellCommandtoFindLength
    {
        public static double Filelength(string filePath)
        {
            string duration = "";
            double length = 0;
            try
            {
                string exactPath = Path.GetFullPath(filePath);
                using (ShellObject shell = ShellObject.FromParsingName(exactPath))
                {
                    // alternatively: shell.Properties.GetProperty("System.Media.Duration");
                    IShellProperty prop = shell.Properties.System.Media.Duration;
                    // Duration will be formatted as 00:44:08
                    duration = prop.FormatForDisplay(PropertyDescriptionFormatOptions.None);
                    DateTime.TryParse(duration, out DateTime dateTime);
                    length = dateTime.TimeOfDay.TotalMilliseconds;
                }
            }
            catch (Exception ex)
            {
                Xceed.Wpf.Toolkit.MessageBox.Show("This file doesn't appear to have a file length," +
                    " please make sure the file in the json is in the unitity project location.");
            }
            return length;
        }
    }
}
