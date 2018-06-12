using Aspose.Pdf;
using Aspose.Pdf.Text;
using DentalSurgery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DentalSurgery.BLL
{
    public class PDFGenerator : IDisposable
    {
        private static readonly License Licence = new License();
        private Color _textColor, _backColor;
        private readonly Font _timeNewRomanFont;
        private readonly TextBuilder _builder;
        private readonly Page _pdfPage;
        private readonly Document _pdfDocument;
        private readonly Rectangle _logoPlaceHolder;
        private bool disposedValue = false;

        public string ForegroundColor
        {
            get { return _textColor.ToString(); }
            set { _textColor = Color.Parse(value); }
        }
        public string BackgroundColor
        {
            get { return _backColor.ToString(); }
            set { _backColor = Color.Parse(value); }
        }

        public string Number;
        public List<string> Patient;
        public List<Surgery> Surgeries;
        public string Footer;
        public PDFGenerator()
        {
            _pdfDocument = new Document();
            _pdfDocument.PageInfo.Margin.Left = 36;
            _pdfDocument.PageInfo.Margin.Right = 36;
            _pdfPage = _pdfDocument.Pages.Add();
            _textColor = Color.Black;
            _backColor = Color.Transparent;
            _timeNewRomanFont = FontRepository.FindFont("Times New Roman");
            _builder = new TextBuilder(_pdfPage);
        }
        public void Save(Stream stream)
        {
            _pdfDocument.Save(stream);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _pdfPage.Dispose();
                    _pdfDocument.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}