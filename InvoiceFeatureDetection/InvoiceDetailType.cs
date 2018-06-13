using System;

namespace InvoiceCapture
{
  [Flags]
  public enum InvoiceDetailType
  {
    None = 0,
    Unspecified = 1,
    DeliveryDate = 8,
    GrandTotalAmount = 16,
    InvoiceDate = 64,
    InvoiceId = 1024,
    DocumentType = 8192,
    Iban = 16384,
    InvoiceCurrency = 524288,
    CustomerId = 2097152,
    UId = 8388608,
    SenderOrderId = 16777216,
    ReceiverOrderId = 33554432,
    SenderOrderDate = 67108864,
    ReceiverOrderDate = 134217728,
    VatGroup = 536870912
  }
}
