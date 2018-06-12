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
        private bool disposedValue = false;
        private int counter = 0;

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
        public List<Visit> Visits;
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
                ColumnWidths = "60 150 78 78 78",
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
            var cell = headerRow.Cells.Add("Data");
            cell.Alignment = HorizontalAlignment.Center;
            headerRow.Cells.Add("Pacjent");
            headerRow.Cells.Add("Zabieg");
            headerRow.Cells.Add("Ząb");
            headerRow.Cells.Add("Cena");
            headerRow.Cells.Add("Szacowany czas");
            foreach (Cell headerRowCell in headerRow.Cells)
            {
                headerRowCell.BackgroundColor = _textColor;
                headerRowCell.DefaultCellTextState.ForegroundColor = _backColor;
            }

            foreach (var visit in Visits)
            {
                var row = table.Rows.Add();
                cell = row.Cells.Add(visit.Date.ToShortDateString());
                cell.Alignment = HorizontalAlignment.Center;
                row.Cells.Add(visit.Patient.FirstName + " " + visit.Patient.LastName);

                foreach (var surgery in visit.Surgeries)
                {
                    if (counter > 0)
                    {
                        var rowLower = table.Rows.Add();
                        cell = rowLower.Cells.Add("");
                        rowLower.Cells.Add("");
                        rowLower.Cells.Add(surgery.Name);
                        if(surgery.Tooth != null)
                            rowLower.Cells.Add(surgery.Tooth.Name);
                        else
                            rowLower.Cells.Add("-");
                        cell = rowLower.Cells.Add(surgery.Price.ToString("C2"));
                        cell.Alignment = HorizontalAlignment.Right;
                        cell = rowLower.Cells.Add(surgery.EstimatedTime.ToString());
                        cell.Alignment = HorizontalAlignment.Right;
                    }
                    else
                    {
                        row.Cells.Add(surgery.Name);
                        if (surgery.Tooth != null)
                            row.Cells.Add(surgery.Tooth.Name);
                        else
                            row.Cells.Add("-");
                        cell = row.Cells.Add(surgery.Price.ToString("C2"));
                        cell.Alignment = HorizontalAlignment.Right;
                        cell = row.Cells.Add(surgery.EstimatedTime.ToString());
                        cell.Alignment = HorizontalAlignment.Right;
                    }
                    counter++;
                }
                counter = 0;
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