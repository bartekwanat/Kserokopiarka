using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    public class Copier : BaseDevice, IPrinter, IScanner
    {

        public int PrintCounter { get; set; }
        public int ScanCounter { get; set; }

        int IDevice.Counter => 0;

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                PrintCounter++;
                string documentName = document.GetFileName();
                Console.WriteLine($"{DateTime.Now:dd MMM yyy} Print:  {documentName}");
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.TXT)
        {
            document = null;
            if (state == IDevice.State.on)
            {
                ScanCounter++;
                string fileName = "";
                switch (formatType)
                {
                    case IDocument.FormatType.TXT:
                        fileName = $"TextScan{ScanCounter}.txt";
                        document = new TextDocument(fileName);
                        break;
                    case IDocument.FormatType.JPG:
                        fileName = $"ImageScan{ScanCounter}.jpg";
                        document = new ImageDocument(fileName);
                        break;
                    case IDocument.FormatType.PDF:
                        fileName = $"PDFScan{ScanCounter}.pdf";
                        document = new PDFDocument(fileName);
                        break;
                }
                Console.WriteLine($"{DateTime.Now} Scan: {fileName}");
            }


        }

        public void ScanAndPrint()
        {
            if (state == IDevice.State.on)
            {
                Scan(out IDocument document);
                Print(in document);
            }
        }



        void PowerOff()
        {
            if (GetState() == IDevice.State.off) return;
            state = IDevice.State.off;
        }

        void PowerOn()
        {

            if (GetState() == IDevice.State.on) return;
            PrintCounter++;
            state = IDevice.State.on;
        }


    }
}
