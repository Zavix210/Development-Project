using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public interface IDataRetriver
    {
        string GetData(string filename); // Retrives  a single file
        string[] GetDirectorysData(string Directory); // Get the data of a particular directory.
    }

}
