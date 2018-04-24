using System.Collections.Generic;
using System.Linq;

namespace InvoiceCapture
{
  public class InvoiceDetailsResponse
  {
    public List<InvoiceDetailDetectionResponse> InvoiceDetailTypePredictions { get; set; }

    public override string ToString()
    {
      return InvoiceDetailTypePredictions.Select(i => i.ToString()).Aggregate((a, b) => a + "\n" + b);
    }
  }
}
