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
      bool requestAllDetails;

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
        requestAllDetails = options.All;
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

      // If set to None than we request all possible invoice details.
      var invoiceDetails = InvoiceDetailType.None;

      if (!requestAllDetails)
      {
        invoiceDetails =
          InvoiceDetailType.DocumentType
          | InvoiceDetailType.Iban
          | InvoiceDetailType.GrandTotalAmount
          | InvoiceDetailType.InvoiceId
          | InvoiceDetailType.InvoiceDate;
      }

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
      const string urlString = "http://blumatixcapturesdk-v1-3.azurewebsites.net/invoicedetail/detect";

      using (var client = new HttpClient())
      {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("X-ApiKey", apiKey);

        // The current version must be provided in the request!!!
        const string version = "v1-3";
        var request = CreateRequest(filename, invoiceFeatures, version);

        try
        {
          var response = client.PostAsync(urlString, request).Result;
          var resultString = response.Content.ReadAsStringAsync().Result;

          if (response.IsSuccessStatusCode)
          {
            var invoiceDetailResponse = JsonConvert.DeserializeObject<DetectInvoiceResponse>(resultString, new JsonSerializerSettings
            {
              Formatting = Formatting.Indented,
              TypeNameHandling = TypeNameHandling.Auto
            });

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

    private static StringContent CreateRequest(string filename, InvoiceDetailType invoiceFeatures, string version)
    {
      byte[] buffer;

      using (var reader = new StreamReader(filename))
      {
        buffer = new byte[reader.BaseStream.Length];
        reader.BaseStream.Read(buffer, 0, (int)reader.BaseStream.Length);
      }

      var request = new InvoiceDetailRequest
      {
        Filter = invoiceFeatures,
        Invoice = Convert.ToBase64String(buffer),
        Version = version
      };

      var stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

      return stringContent;
    }
  }
}
