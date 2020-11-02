using System;

namespace InvoiceCapture
{
    [Flags]
    public enum InvoiceDetailType : long
    {
        None = 0,
        Sender = 2,
        DeliveryDate = 8,
        GrandTotalAmount = 16,
        VatRate = 32,
        InvoiceDate = 64,
        Receiver = 128,
        NetTotalAmount = 256,
        InvoiceId = 1024,
        DocumentType = 8192,
        Iban = 16384,
        Bic = 32768,
        LineItem = 65536,
        VatAmount = 131072,
        InvoiceCurrency = 524288,
        DeliveryNoteId = 1048576,
        CustomerId = 2097152,
        TaxNumber = 4194304,
        UId = 8388608,
        SenderOrderId = 16777216,
        ReceiverOrderId = 33554432,
        SenderOrderDate = 67108864,
        ReceiverOrderDate = 134217728,
        NetAmount = 268435456,
        VatGroup = 536870912,
        VatTotalAmount = 1073741824,
        BankCode = 4294967296,
        BankAccount = 8589934592,
        BankGroup = 17179869184,
        IsrNumber = 34359738368
    }
}
