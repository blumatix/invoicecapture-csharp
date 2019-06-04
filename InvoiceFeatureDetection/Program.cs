using System;
using System.Configuration;
using System.IO;
using System.Net;
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
      string singleInvoice;
      string apiKey;
      string sdkVersion;
      bool writeFile;
      string baseUrl;
      bool useProxy;

      var options = new Options();
      var parser = new CommandLine.Parser(s =>
      {
        s.CaseSensitive = true;
        s.HelpWriter = Console.Out;
      });

      if (parser.ParseArguments(args, options))
      {
        invoiceFolder = options.InvoiceFolder;
        singleInvoice = options.Invoice;
        apiKey = options.Key;
        sdkVersion = options.SdkVersion;
        writeFile = options.WriteFile;
        baseUrl = options.BaseUrl;
        useProxy = options.UseProxy;
      }
      else
      {
        return;
      }

      // If set to None than we request all possible invoice details.
      var invoiceDetails = InvoiceDetailType.None;

      IWebProxy proxy = useProxy ? CreateProxyFromSettings() : null;

      if (Directory.Exists(invoiceFolder))
      {
        var invoices = Directory.GetFiles(invoiceFolder);

        foreach (var invoice in invoices)
        {
          Console.WriteLine($"Send request for: {invoice}");
          RequestInvoiceDetails(apiKey, invoice, invoiceDetails, baseUrl, sdkVersion, writeFile, proxy);
        }
      }

      if (File.Exists(singleInvoice))
      {
        Console.WriteLine($"Send request for: {singleInvoice}");
        RequestInvoiceDetails(apiKey, singleInvoice, invoiceDetails, baseUrl, sdkVersion, writeFile, proxy);
      }
    }

    private static void RequestInvoiceDetails(string apiKey, string filename, InvoiceDetailType invoiceFeatures, string baseUrl, string sdkVersion, bool writeFile, IWebProxy proxy = null)
    {
      var urlString = $"{baseUrl}/invoicedetail/detect";

      // Use Proxy
      HttpClientHandler httpClientHandler = null;
      if (proxy != null)
      {
        httpClientHandler = new HttpClientHandler
        {
          Proxy = proxy,
          UseProxy = true
        };
      }

      using (var client = httpClientHandler != null ? new HttpClient(httpClientHandler) : new HttpClient())
      {
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.Add("X-ApiKey", apiKey);

        // The current version must be provided in the request!!!
        var version = sdkVersion;
        var request = CreateRequest(filename, invoiceFeatures, version);

        try
        {
          var response = client.PostAsync(urlString, request).Result;
          var resultString = response.Content.ReadAsStringAsync().Result;

          if (response.IsSuccessStatusCode)
          {
            Console.WriteLine($"Received response for {filename}");

            dynamic parsedJson = JsonConvert.DeserializeObject(resultString);
            var formattedResultString = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);

            if (writeFile)
            {
              File.WriteAllText(filename + ".json", formattedResultString);
            }
            else
            {
              Console.WriteLine();
              Console.WriteLine(formattedResultString);
            }
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

    private static IWebProxy CreateProxyFromSettings()
    {
      var proxyUrl = ConfigurationManager.AppSettings["ProxyUrl"];
      var proxyUsername = ConfigurationManager.AppSettings["ProxyUsername"];
      var proxyPassword = ConfigurationManager.AppSettings["ProxyPassword"];

      var proxy = new WebProxy(new Uri(proxyUrl))
      {
        Credentials = new NetworkCredential(proxyUsername, proxyPassword)
      };

      return proxy;
    }
  }
}
