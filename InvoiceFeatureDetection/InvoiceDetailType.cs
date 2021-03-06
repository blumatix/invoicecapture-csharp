﻿using System;

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
        IsrReference = 34359738368,
        DiscountDate = 68719476736,
        DiscountStart = 137438953472,
        DiscountDuration = 274877906944,
        DiscountPercent = 549755813888,
        DiscountGroup = 1099511627776,
        DueDateDate = 2199023255552,
        DueDateStart = 4398046511104,
        DueDateDuration = 8796093022208,
        DueDateGroup = 17592186044416,
        IsrSubscriber = 35184372088832,
        KId = 70368744177664,
        CompanyRegistrationNumber = 140737488355328,
        Contacts = 281474976710656
    }
}
