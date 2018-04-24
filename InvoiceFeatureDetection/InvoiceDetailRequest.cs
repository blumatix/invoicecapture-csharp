using Newtonsoft.Json;

namespace InvoiceCapture
{
  public class InvoiceDetailRequest
  {
    public InvoiceDetailType Flags { get; set; }

    public string Invoice { get; set; }

    public string ToJson()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
