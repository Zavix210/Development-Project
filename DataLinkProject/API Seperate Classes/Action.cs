using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilderWpf.DataModels
{
    public class Action
    {
        public Actions ActionEnum { get; }

        /// <summary>
        /// 0 to 100 would be relative fire/smoke intestinty could increase based on time etc.. 
        /// </summary>
        public float Intensity { get; }
        /// <summary>
        /// Could be left as 0.0 and that could signfy entire time. 
        /// </summary>
        public float Time { get; }
    }
}
