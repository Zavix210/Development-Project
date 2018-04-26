using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System;
using System.Collections.Generic;
using System.Linq;
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
                using (ShellObject shell = ShellObject.FromParsingName(filePath))
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
                Xceed.Wpf.Toolkit.MessageBox.Show("This file doesn't appear to have a file length!");
            }
            return length;
        }
    }
}
