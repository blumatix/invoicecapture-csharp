using System.Collections.Generic;
using System.Linq;
using System.Resources;

namespace InvoiceFeatureDetection
{
  public enum InvoiceProcessingState
  {
    Idle,
    Running,
    Finished,
    Failed
  }

  public class DetectInvoiceStatusResponse
  {
    public InvoiceProcessingState State { get; set; }

    public IList<InvoiceFeatureDetectionResponse> InvoiceDetailTypePredictions { get; set; }

    public override string ToString()
    {


      return InvoiceDetailTypePredictions != null
        ? $"InvoiceState : {State}\n" + InvoiceDetailTypePredictions.Select(i => i.ToString()).Aggregate((a, b) => a + "\n" + b)
        : $"InvoiceState : {State}";
    }

  }
}
