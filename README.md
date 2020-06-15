# invoicecapture-csharp
A csharp example application which demonstrates how our invoice capture REST API can be used to retrieve information about specific invoice details.

## Usage
A single invoice is sent to our Invoice Capture service in the following example. It takes as arguments:
- Your API key
- The path to an invoice
- The URL to our invoice capture service

The result is printed to the console output.
```csharp
.\InvoiceCapture.exe -k YOUR_API_KEY -i PATH_TO_AN_INVOICE -u https://blumatixcapturesdk-v1-10.azurewebsites.net
```

## Help
```csharp
PS> .\InvoiceCapture.exe -help
InvoiceFeatureDetection 1.0.0.0
Copyright c  2017

  -f, --invoiceFolder    (Default: ) Folder which contains invoice that shall
                         be processed

  -i, --invoice          (Default: ) Path to a single invoice

  -k, --key              Required. Your API key token

  -s, --version          (Default: v1-10) The version of the actual BLU DELTA
                         release

  -u, --baseUrl          (Default: http://blumatixcapturesdk-v1-10.azurewebsites.net) Base url of your
                         capturesdk version

  -w, --writeFile        (Default: False) If set then the response will be
                         written to a json file. The name will be the same as
                         the provided invoice filename
                         
  -p, --useProxy        (Default: False) Use a proxy server. Proxy url,
                         username and password must be set in app.config

  --help                 Display this help screen.
```
