using System;

namespace InvoiceFeatureDetection
{
  public class InvoiceFeatureDetectionResponse
  {
    public int Type { get; set; }

    public string  Value { get; set; }

    public double Score { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public override string ToString()
    {
      return $"{(InvoiceDetailType) Type},Value:{Value},Score:{Score},[X:{X},Y:{Y},Width:{Width},Height:{Height}]";
    }
  }
}
