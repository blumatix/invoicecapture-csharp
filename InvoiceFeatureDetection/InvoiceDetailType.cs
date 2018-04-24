using System;

namespace InvoiceCapture
{
  [Flags]
  public enum InvoiceDetailType
  {
    None = 0,
    Unspecified = 1,
    GrandTotalAmount = 16,
    InvoiceDate = 64,
    NetTotalAmount = 256,
    InvoiceId = 1024,
    DocumentTitle = 4096,
    DocumentType = 8192,
    VatAmount = 131072,
    Iban = 16384
  }
}
