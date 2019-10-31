using Newtonsoft.Json;

namespace InvoiceCapture
{
    public class InvoiceDetailRequest
    {
        public InvoiceDetailType Filter { get; set; }

        public string Invoice { get; set; }

        public string Version { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
