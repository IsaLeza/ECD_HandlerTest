using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ECD_Handler
{
    class Program
    {
        // Utility to print multiple strings with a custom separator and ending
        static void print_msgs(string[]? txt_msgs = null, string sep = "\n", string end = "\n")
        {
            if (txt_msgs is null)
            {
                return;
            }

            foreach (string txt in txt_msgs.Take(txt_msgs.Length - 1))
            {
                Console.Write(txt + sep);
            }
            Console.Write(txt_msgs[txt_msgs.Length - 1] + end);
        }

        // Process each XML file and compute total per invoice
        static void process_files_dir(string[] files)
        {
            var output = new StringBuilder();

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                decimal total = ProcessECDFile(file);

                string result = $"File: {fileName} - Total monto_total: ${total:N2}";
                Console.WriteLine(result + "\n" + new string('-', 50) + "\n");

                output.AppendLine(result);
            }

            // Save the results into a text file
            File.WriteAllText("results.txt", output.ToString());
            Console.WriteLine("✅ Results saved in 'results.txt'");
        }

        // Function to parse and process a single XML ECD file
        static decimal ProcessECDFile(string filePath)
        {
            try
            {
                XDocument doc = XDocument.Load(filePath);

                var conceptos = doc.Descendants("liquidacion")
                    .Where(l => (string?)l.Attribute("num_liq") == "0")
                    .Descendants("factura")
                    .Descendants("concepto");

                decimal total = conceptos
                    .Select(c => (string?)c.Element("monto_total"))
                    .Where(m => !string.IsNullOrEmpty(m))
                    .Sum(m => decimal.TryParse(m, out var val) ? val : 0);

                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                return 0;
            }
        }

        // Main entry point
        static void Main(string[] args)
        {
            // XML files should be in a folder named 'xml_repo' located in the user's Documents directory
            string work_dir = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "xml_repo"
            );

            string[] files = Directory.GetFiles(work_dir, "*.xml"); // Only XML files

            string[] welcome_msg = {
                "   # ECD Handler Program",
                " # Files will be loaded from: " + work_dir
            };

            print_msgs(txt_msgs: welcome_msg, sep: new string('\n', 2));

            if (files.Length == 0)
            {
                Console.WriteLine("⚠️ No XML files were found in the directory.");
                return;
            }

            process_files_dir(files);
        }
    }
}
