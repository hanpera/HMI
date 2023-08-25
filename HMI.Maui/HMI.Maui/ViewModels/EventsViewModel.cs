using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HMI.Maui.Models;
using HMI.Maui.Services;
using Syncfusion.Maui.DataGrid;
using Syncfusion.Maui.DataGrid.Exporting;
using Syncfusion.Pdf;
using Syncfusion.XlsIO;

namespace HMI.Maui.ViewModels
{
    public partial class EventsViewModel : BaseViewModel
    {
        private readonly EventsService _eventsService;
        IFileSaver _fileSaver;
        public EventsViewModel(EventsService eventsService, IFileSaver fileSaver)
        {
            _eventsService = eventsService;
            _fileSaver = fileSaver;
            Title = "Events";
        }

        public ObservableCollection<Event> Events { get; set; } = new();

        [ObservableProperty]
        bool _isRefreshing;

        [ObservableProperty]
        EventsRequest _request = new();

        [RelayCommand]
        public async Task RefreshEventsAsync()
        {
            try
            {
                IsLoading = true;
                IsRefreshing = true;
                var events = await _eventsService.GetEventsAsync(Request);
                Events.Clear();
                foreach (var @event in events)
                {
                    Events.Add(@event);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        public void Clear()
        {
            Request = new();
            Events.Clear();
        }

        [RelayCommand]
        public async Task ExportPdf(object parameter)
        {
            try
            {
                #region Pdf Document
                MemoryStream stream = new MemoryStream();
                DataGridPdfExportingController pdfExport = new DataGridPdfExportingController();
                DataGridPdfExportingOption option = new DataGridPdfExportingOption();
                option.PdfDocument = new();
                option.PdfDocument.PageSettings.Orientation = PdfPageOrientation.Landscape;
                option.CanFitAllColumnsInOnePage = true;
                var pdfDoc = pdfExport.ExportToPdf((parameter as SfDataGrid)! , option);
                pdfDoc.Save(stream);
                pdfDoc.Close(true);
                await SaveFileAsync("Events.pdf", stream);
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task ExportExcel(object parameter)
        {
            try
            {
                #region Excel Document

                MemoryStream stream = new MemoryStream();
                DataGridExcelExportingController excelExport = new DataGridExcelExportingController();
                DataGridExcelExportingOption option = new DataGridExcelExportingOption();
                option.ExcelVersion = ExcelVersion.Excel2016;
                var excelEngine = excelExport.ExportToExcel((parameter as SfDataGrid)!, option);
                var workbook = excelEngine.Excel.Workbooks[0];
                workbook.SaveAs(stream);
                workbook.Close();
                excelEngine.Dispose();
                await SaveFileAsync("Events.xlsx", stream);

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task SaveFileAsync(string fileName, Stream content)
        {
            await _fileSaver.SaveAsync(fileName, content, new CancellationToken());
        }
    }
}
