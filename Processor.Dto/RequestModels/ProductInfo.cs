using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor.Dto.RequestModels
{
    public class ProductInfo
    {
        public int Id { get; set; }
        public string VendorName { get; set; }
        public string Grade { get; set; }
        public string SerialNo { get; set; }
    }
}
