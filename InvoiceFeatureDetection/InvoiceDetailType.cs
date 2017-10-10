using System;

namespace InvoiceFeatureDetection
{
  [Flags]
  public enum InvoiceDetailType
  {
    None = 0,
    Unspecified = 1,
    GrandTotalAmount = 16,
    InvoiceDate = 64,
    InvoiceId = 1024,
    DocumentTitle = 4096,
    DocumentType = 8192,
    Iban = 16384
  }
}
