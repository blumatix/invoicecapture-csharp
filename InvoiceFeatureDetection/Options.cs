using CommandLine;
using CommandLine.Text;

namespace InvoiceFeatureDetection
{
  public class Options
  {
    [Option('i', "invoiceFolder", Required = true, HelpText = "Folder which contains invoice that shall be processed")]
    public string InvoiceFolder { get; set; }

    [Option('H', "hostname", Required = false, DefaultValue = "blumatixcapturesdk-v1-2.azurewebsites.net", HelpText = "Uri to the BlumatixCaptureSdk service")]
    public string Hostname { get; set; }

    [Option('k', "key", Required = true, HelpText = "Your API key token")]
    public string Key { get; set; }

    [Option('a', "async", Required = false, DefaultValue = false, HelpText = "Calls non-blocking REST api")]
    public bool IsAsync { get; set; }

    [HelpOption]
    public string GetUsage()
    {
      return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
    }
  }
}
