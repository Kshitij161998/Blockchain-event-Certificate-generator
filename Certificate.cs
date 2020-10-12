using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using word = Microsoft.Office.Interop.Word;

namespace Certificate_generation
{
    class Certificate 
    {
        string TemplatePath = "D:\\Certificate_generation\\Certificate_generation\\Certificate Template\\temp.doc";

        internal Response GenerateReport(Candidate candidate)
        {
            object destination = "D:\\Certificate_generation\\Certificate_generation\\Certificates\\word\\Certificate" + Trimmer.trim(candidate.name.Trim()) + candidate.number.Trim() + ".doc";
            string destination_name = "D:\\Certificate_generation\\Certificate_generation\\Certificates\\word\\Certificate" + Trimmer.trim(candidate.name.Trim()) + candidate.number.Trim() + ".doc";

            Response res = new Response();


            System.IO.File.Copy(TemplatePath, destination_name, true);
            try
            {
                word.Application app = new word.Application();
                app.Visible = false;

                word.Document doc = null;

                object missing = Type.Missing;
                doc = app.Documents.Open(destination, missing, missing);
              
                app.Selection.Find.ClearFormatting();
                app.Selection.Find.Replacement.ClearFormatting();

                app.Selection.Find.Execute("<name>", missing, missing, missing, missing, missing, missing, missing, missing, candidate.name, 2);
                //app.Selection.Find.Execute("<email>", missing, missing, missing, missing, missing, missing, missing, missing, candidate.email, 2);
                app.Selection.Find.Execute("<date>", missing, missing, missing, missing, missing, missing, missing, missing, candidate.date.ToString(), 2);
                var image=app.Selection.InlineShapes.AddPicture("D:\\Certificate_generation\\Certificate_generation\\Temporary QR\\temp.jpeg");

                image.Height = 75;
                image.Width = 75;
                


                doc.ExportAsFixedFormat("D:\\Certificate_generation\\Certificate_generation\\Certificates\\pdf\\Certificate_" + Trimmer.trim(candidate.name.Trim()) +candidate.number.Trim() + ".pdf", word.WdExportFormat.wdExportFormatPDF);
                object SaveAsFile = (object)"D:\\Certificate_generation\\Certificate_generation\\Certificates\\word\\Certificate_" + Trimmer.trim(candidate.name.Trim()) + candidate.number.Trim() + ".doc";
                doc.SaveAs2(SaveAsFile, missing, missing, missing);
                doc.Close(false, missing, missing);
                app.Quit(false, false, false);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(app);

                res.resp = true;
                res.type = "Certificate Successful Printed";

            }
            catch (Exception ex)
            {
                res.resp = false;
                res.type = "Certificate cannot be downloaded";

            }
            return res;
        }
    }
}
