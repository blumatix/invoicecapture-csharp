using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace InvoiceCapture
{
  class Program
  {
    static void Main(string[] args)
    {
      string invoiceFolder;
      string apiKey;

      var options = new Options();
      var parser = new CommandLine.Parser(s =>
      {
        s.CaseSensitive = true;
        s.HelpWriter = Console.Out;
      });


      if (parser.ParseArguments(args, options))
      {
        invoiceFolder = options.InvoiceFolder;
        apiKey = options.Key;

        Console.WriteLine($"Invoice folder: {invoiceFolder}");
        Console.WriteLine($"ApiKey: {apiKey}");
      }
      else
      {
        return;
      }


      if (!Directory.Exists(invoiceFolder))
      {
        Console.WriteLine($"InvoiceFolder {invoiceFolder} is invalid");
        return;
      }

      const InvoiceDetailType invoiceDetails = 
        InvoiceDetailType.DocumentType
        | InvoiceDetailType.NetTotalAmount
        | InvoiceDetailType.VatAmount
        | InvoiceDetailType.Iban
        | InvoiceDetailType.GrandTotalAmount
        | InvoiceDetailType.InvoiceId
        | InvoiceDetailType.InvoiceDate;

      var invoices = Directory.GetFiles(invoiceFolder);

      foreach (var invoice in invoices)
      {
        RequestInvoiceDetails(apiKey, invoice, invoiceDetails);
      }

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }

    private static void RequestInvoiceDetails(string apiKey, string filename, InvoiceDetailType invoiceFeatures)
    {
      const string urlString = "http://blumatixcapturesdk-v1-2.azurewebsites.net/v1-2/invoicedetail/detect";

      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("X-ApiKey", apiKey);

        var request = CreateRequest(filename, invoiceFeatures);

        try
        {
          var response = client.PostAsync(urlString, request).Result;
          var resultString = response.Content.ReadAsStringAsync().Result;

          if (response.IsSuccessStatusCode)
          {
            var invoiceDetailResponse = JsonConvert.DeserializeObject<InvoiceDetailsResponse>(resultString);
            Console.WriteLine();
            Console.WriteLine(invoiceDetailResponse);            
          }
          else
          {
            Console.WriteLine(response.StatusCode + "\n" + response.Content + "\n" + response.ReasonPhrase +"\n" + resultString);
          }
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }
      }
    }

    private static StringContent CreateRequest(string filename, InvoiceDetailType invoiceFeatures)
    {
      byte[] buffer;

      using (var reader = new StreamReader(filename))
      {
        buffer = new byte[reader.BaseStream.Length];
        reader.BaseStream.Read(buffer, 0, (int)reader.BaseStream.Length);
      }

      var request = new InvoiceDetailRequest
      {
        Flags = invoiceFeatures,
        Invoice = Convert.ToBase64String(buffer),
      };

      var stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

      return stringContent;
    }
  }
}
