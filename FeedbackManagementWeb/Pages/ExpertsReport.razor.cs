using Domain.Entities;
using Domain.Model;
using FeedbackManagementWeb.Services;
using FeedbackManagementWeb.Shared.Dialog;
using FeedbacksManagementApi.Model;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class ExpertsReport
    {
        [Inject]
        private IExpertService expertService { get; set; }

        [Inject]
        private IReportService reportService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;


        private IEnumerable<Expert> expertList = new List<Expert>();

        IEnumerable<Specialty> SpecialtiesList = new List<Specialty>();

        private bool firstRender = true;
        private readonly int[] _pageSizeOption = { 10, 20, 30 };
        
        private ExpertReportFilterModel filter = new();
        private MudTable<Expert>? _table = new();
        IEnumerable<Expert> experts = new List<Expert>();

        protected override async Task OnInitializedAsync()
        {
            await GetSpecialties();
        }

        //لیست گروه متخصصین را میگیرد
        private async Task GetSpecialties()
        {
            try
            {
                SpecialtiesList = await reportService.GetSpecialties();
            }
            catch (AppException ax)
            {
                Snackbar.Add(ax.Message, Severity.Warning);
            }
            catch (Exception)
            {
                Snackbar.Add("در واکشی اطلاعات خطایی رخ داده است", Severity.Error);
            }
        }

        //لیست فیدبک هارا میگیرد و داخل جدول قرار میدهد
        public async Task<TableData<Expert>> GetFeedbackReport(TableState state)
        {
            if (firstRender)
                return new TableData<Expert>() { TotalItems = 0, Items = new List<Expert>() };
            try
            {
                var totalItems = await expertService.GetExpertReportCount(filter);
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    Snackbar.Add("داده ای برای نمایش یافت نشد", Severity.Info);
                    return new TableData<Expert>() { TotalItems = 0, Items = new List<Expert>() };
                }

                filter.Skip = state.Page * state.PageSize;
                filter.Take = state.PageSize;
                expertList = await expertService.GetExpertReports(filter);

                return new TableData<Expert>() { TotalItems = totalItems, Items = expertList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<Expert>() { TotalItems = 0, Items = new List<Expert>() };
            }
        }
        //با پارامتر های جدید آپدیت میکند
        public async Task GetReport()
        {
            firstRender = false;
            await _table!.ReloadServerData();
        }

        //فیلتر را حذف میکند
        public async Task RemoveFilter()
        {
            firstRender = false;
            filter = new();
            await _table!.ReloadServerData();
        }
    }
}
