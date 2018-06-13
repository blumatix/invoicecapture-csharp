using System.Collections.Generic;
using System.Text;

namespace InvoiceCapture
{
  public class DetectionResponse
  {
    public InvoiceDetailType Type { get; set; }

    public string TypeName { get; set; }

    public string Value { get; set; }

    public double Score { get; set; }

    public int X { get; set; }

    public int Y { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public override string ToString()
    {
      return $"InvoiceDetail : {TypeName}, Value : {Value}, Score : {Score}, [ X : {X}, Y : {Y}, Width : {Width}, Height : {Height}]";
    }
  }

  public class DetectionGroupResponse
  {
    public InvoiceDetailType Type { get; set; }

    public string TypeName { get; set; }

    public IList<DetectionResponse> InvoiceDetailTypePredictions { get; set; }

    public override string ToString()
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendLine($"GroupType: {TypeName}");

      foreach (var invoiceDetailPrediction in InvoiceDetailTypePredictions)
      {
        stringBuilder.AppendLine("\t" + invoiceDetailPrediction);
      }

      return stringBuilder.ToString();
    }
  }

  public class DetectInvoiceResponse
  {
    public int DocumentResolution { get; set; }

    public IList<DetectionResponse> InvoiceDetailTypePredictions { get; set; }

    public IList<DetectionGroupResponse> PredictionGroups { get; set; }

    public string FormattedResult { get; set; } = null;

    public bool IsQualityOk { get; set; } = true;

    public override string ToString()
    {
      var stringBuilder = new StringBuilder();

      stringBuilder.AppendLine($"DocumentResolution: {DocumentResolution}, IsQualityOk: {IsQualityOk}");

      foreach (var invoiceDetailPrediction in InvoiceDetailTypePredictions)
      {
        stringBuilder.AppendLine(invoiceDetailPrediction.ToString());
      }

      foreach (var group in PredictionGroups)
      {
        stringBuilder.AppendLine(group.ToString());
      }

      return stringBuilder.ToString();
    }
  }
}
