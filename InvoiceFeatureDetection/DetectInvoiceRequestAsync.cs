using Newtonsoft.Json;

namespace InvoiceFeatureDetection
{
  public class DetectInvoiceRequestAsync
  {
    public InvoiceDetailType Flags { get; set; }

    public string Invoice { get; set; }

    public string ToJson()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}
