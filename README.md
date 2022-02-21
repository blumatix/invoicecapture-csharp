# invoicecapture-csharp
Contains sample code to access our capture client.

### More information and quick test capability: [BLU DELTA AI invoice capture - KI Rechnungserfassung](https://www.bludelta.de)
### Request free API key here: [Capturing - Rechungserfassung Get Started](https://www.bludelta.de/en/get-started/)
### Access URL (latest version): https://api.bludelta.ai/v1-17

## Usage
A single invoice is sent to our Invoice Capture service in the following example. It takes as arguments:
- Your API key
- The path to an invoice
- The URL to our invoice capture service

The result is printed to the console output.
```csharp
.\InvoiceCapture.exe -k YOUR_API_KEY -i PATH_TO_AN_INVOICE -u https://api.bludelta.ai/v1-17
```

## Help
```csharp
PS> .\InvoiceCapture.exe -help
InvoiceFeatureDetection 1.1.0.0
Copyright c  2021

  -f, --invoiceFolder    (Default: ) Folder which contains invoice that shall
                         be processed

  -i, --invoice          (Default: ) Path to a single invoice

  -k, --key              Required. Your API key token

  -s, --version          (Default: v1-17) The version of the actual BLU DELTA
                         release

  -u, --baseUrl          (Default: https://api.bludelta.ai/v1-17) Base url of your
                         capturesdk version

  -w, --writeFile        (Default: False) If set then the response will be
                         written to a json file. The name will be the same as
                         the provided invoice filename
                         
  -p, --useProxy        (Default: False) Use a proxy server. Proxy url,
                         username and password must be set in app.config

  --help                 Display this help screen.
```
### 5x8 Support: bludelta-support@blumatix.com
