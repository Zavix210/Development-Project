using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneBuilder.ViewModels
{
    public class ScenePageViewModel: BaseViewModel
    {
        private string _title = "Scene Page";

        public string Title
        {
            get => _title;
            set => _title = value;
        }
    }
}
