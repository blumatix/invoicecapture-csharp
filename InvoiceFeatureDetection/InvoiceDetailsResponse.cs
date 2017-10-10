using System.Collections.Generic;
using System.Linq;

namespace InvoiceFeatureDetection
{
  public class InvoiceDetailsResponse
  {
    public List<InvoiceFeatureDetectionResponse> InvoiceDetailTypePredictions { get; set; }

    public override string ToString()
    {
      return InvoiceDetailTypePredictions.Select(i => i.ToString()).Aggregate((a, b) => a + "\n" + b);
    }
  }
}
