using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryApp.ViewModel
{
    public class StatusViewModel : BaseModel
    {
        private string _status;
        
        public string Status
        {
            get { return _status; }
            set { 
                if (_status != value)
                {
                    _status = value;
                    RaisePropertyChanged(nameof(Status));
                }
            }
        }
    }
}
