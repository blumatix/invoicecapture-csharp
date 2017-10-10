using System;

namespace InvoiceFeatureDetection
{
  public class DetectInvoiceResponseAsync
  {
    public Guid InvoiceToken { get; set; }

    public override string ToString()
    {
      return $"Invoice access token {InvoiceToken}";
    }
  }
}
