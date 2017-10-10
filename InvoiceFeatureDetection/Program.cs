using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InvoiceFeatureDetection
{
  class Program
  {
    static void Main(string[] args)
    {
      string invoiceFolder;
      string hostname;
      string apiKey;
      bool isAsync;


      var options = new Options();
      var parser = new CommandLine.Parser(s =>
      {
        s.CaseSensitive = true;
        s.HelpWriter = Console.Out;
      });


      if (parser.ParseArguments(args, options))
      {
        invoiceFolder = options.InvoiceFolder;
        hostname = options.Hostname;
        apiKey = options.Key;
        isAsync = options.IsAsync;

        Console.WriteLine($"Invoice folder: {invoiceFolder}");
        Console.WriteLine($"Hostname: {hostname}");
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

      const InvoiceDetailType invoiceFeatures = 
        InvoiceDetailType.DocumentType
        | InvoiceDetailType.Iban
        | InvoiceDetailType.GrandTotalAmount
        | InvoiceDetailType.InvoiceId
        | InvoiceDetailType.InvoiceDate;

      var invoices = Directory.GetFiles(invoiceFolder);


      foreach (var invoice in invoices)
      {
        RequestInvoiceDetails(hostname, apiKey, invoice, invoiceFeatures);
      }

      Console.WriteLine("Press any key to exit...");
      Console.ReadKey();
    }

    private static void RequestInvoiceDetails(string hostname, string apiKey, string filename, InvoiceDetailType invoiceFeatures)
    {
      using (var client = new HttpClient())
      {
        var urlString = $"http://{hostname}/v1-2/invoicedetail/detect";

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

    private static StringContent CreateRequest(string filename, InvoiceDetailType invoiceFeatures, bool isAsync=false)
    {
      byte[] buffer;

      using (var reader = new StreamReader(filename))
      {
        buffer = new byte[reader.BaseStream.Length];
        reader.BaseStream.Read(buffer, 0, (int)reader.BaseStream.Length);
      }

      if (!isAsync)
      {
        var request = new InvoiceDetailRequest
        {
          Flags = invoiceFeatures,
          Invoice = Convert.ToBase64String(buffer),
        };

        var stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

        return stringContent;
      }
      else
      {
        var request = new DetectInvoiceRequestAsync
        {
          Flags = invoiceFeatures,
          Invoice = Convert.ToBase64String(buffer),
        };

        var stringContent = new StringContent(request.ToJson(), Encoding.UTF8, "application/json");

        return stringContent;
      }
    }
  }
}
