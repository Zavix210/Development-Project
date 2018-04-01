using SceneBuilderWpf.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public interface InputParse
    {
         Scene ParseInput(string input);
    }
}
