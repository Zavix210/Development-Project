using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public interface IPersit
    {
        void PersistDocument(string serializedScene, string locationfile ,string targetFileName); // Persit the data of a particular item. 
    }
}
