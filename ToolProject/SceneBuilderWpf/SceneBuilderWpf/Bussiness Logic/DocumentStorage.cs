using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.Bussiness_Logic
{
    public abstract class DocumentStorage : IDataRetriver, IPersit
    {
        public abstract string GetData(string filename);

        public abstract string[] GetDirectorysData(string Directory);

        public abstract void PersistDocument(string serializedScene, string locationfile , string targetFileName);
    }

}
