using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public interface IFormatConvert
    {
        bool ConvertFormat(Scene scenario, string targetlocation, string targetFileName);

        Scene ConvertFormat(string sourceFileName);
    }
}
