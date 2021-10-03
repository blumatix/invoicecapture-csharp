using CommandLine;
using CommandLine.Text;

namespace InvoiceCapture
{
    public class Options
    {
        [Option('f', "invoiceFolder", Required = false, DefaultValue = "", HelpText = "Folder which contains invoice that shall be processed")]
        public string InvoiceFolder { get; set; }

        [Option('i', "invoice", Required = false, DefaultValue = "", HelpText = "Path to a single invoice")]
        public string Invoice { get; set; }

        [Option('k', "key", Required = true, HelpText = "Your API key token")]
        public string Key { get; set; }

        [Option('s', "version", Required = false, DefaultValue = "v1-16", HelpText = "The version of the actual BLU DELTA release")]
        public string SdkVersion { get; set; }

        [Option('u', "baseUrl", Required = false, DefaultValue = "https://api.bludelta.ai/v1-16", HelpText = "Base url of your capturesdk version")]
        public string BaseUrl { get; set; }

        [Option('w', "writeFile", Required = false, DefaultValue = false, HelpText = "If set then the response will be written to a json file. The name will be the same as the provided invoice filename")]
        public bool WriteFile { get; set; }

        [Option('p', "useProxy", Required = false, DefaultValue = false, HelpText = "Use a proxy server. Proxy url, username and password must be set in app.config")]
        public bool UseProxy { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
