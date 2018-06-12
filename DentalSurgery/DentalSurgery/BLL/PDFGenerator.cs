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
            GridSection();
            _pdfDocument.Save(stream);
        }

        private void GridSection()
        {
            var table = new Table
            {
                ColumnWidths = "26 257 78 78 78",
                Border = new BorderInfo(BorderSide.Box, 1f, _textColor),
                DefaultCellBorder = new BorderInfo(BorderSide.Box, 0.5f, _textColor),
                DefaultCellPadding = new MarginInfo(4.5, 4.5, 4.5, 4.5),
                Margin =
                {
                    Bottom = 10
                },
                DefaultCellTextState =
                {
                    Font = _timeNewRomanFont
                }
            };

            var headerRow = table.Rows.Add();
            var cell = headerRow.Cells.Add("#");
            cell.Alignment = HorizontalAlignment.Center;
            headerRow.Cells.Add("Item");
            headerRow.Cells.Add("Price");
            headerRow.Cells.Add("Quantity");
            headerRow.Cells.Add("Sum");
            foreach (Cell headerRowCell in headerRow.Cells)
            {
                headerRowCell.BackgroundColor = _textColor;
                headerRowCell.DefaultCellTextState.ForegroundColor = _backColor;
            }

            foreach (var surgery in Surgeries)
            {
                var row = table.Rows.Add();
                cell = row.Cells.Add(surgery.SurgeryId.ToString());
                cell.Alignment = HorizontalAlignment.Center;
                row.Cells.Add(surgery.Name);
                cell = row.Cells.Add(surgery.Price.ToString("C2"));
                cell.Alignment = HorizontalAlignment.Right;
                cell = row.Cells.Add(surgery.EstimatedTime.ToString());
                cell.Alignment = HorizontalAlignment.Right;
            }
            _pdfPage.Paragraphs.Add(table);
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