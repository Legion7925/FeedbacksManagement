using Domain.Entities;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Services;
using FeedbackManagementWeb.Shared.Dialog;
using FeedbacksManagementApi.Model;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class Report
    {
        [Inject]
        private IFeedbackService FeedbackService { get; set; } = default!;
        [Inject]
        private IReportService ReportService { get; set; } = default!;
        [Inject]
        private IExpertService ExpertService { get; set; } = default!;
        [Inject]
        private ITagService TagService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IEnumerable<FeedbackReport> feedbackList = new List<FeedbackReport>();

        private bool _loading = false;

        private HashSet<FeedbackReport> selectedFeedbacks = new();

        private readonly int[] _pageSizeOption = { 10, 20, 30 };

        private MudTable<FeedbackReport>? _table = new();

        private FeedbackReportFilterModel filter = new();

        private bool firstRender = true;

        private DateTime? Created;
        private DateTime? RespondDate;
        //private DateTime? Updated;

        IEnumerable<Product> products = new List<Product>();
        IEnumerable<Specialty> specialties = new List<Specialty>();
        IEnumerable<Customer> customers = new List<Customer>();
        IEnumerable<Tag> tags = new List<Tag>();

        private string customerNameFilter = string.Empty;
        private string productNameFilter = string.Empty;

        private string _expertName = string.Empty;
        private string _expertGroupName = string.Empty;
        private IEnumerable<Expert> ExpertList = new List<Expert>();


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender is not true)
                return;

            await GetAppLists();
        }
        //لیست فیدبک را از سرور میگیرد و صفحه بندی میکند داخل جدول قرار میدهد
        public async Task<TableData<FeedbackReport>> GetFeedbackReport(TableState state)
        {
            if (firstRender)
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            try
            {
                var totalItems = await FeedbackService.GetFeedbackReportCount(filter);
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    Snackbar.Add("داده ای برای نمایش یافت نشد", Severity.Info);
                    return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
                }

                filter.Skip = state.Page * state.PageSize;
                filter.Take = state.PageSize;
                filter.Created = Created;
                filter.RespondDate = RespondDate;
                filter.ExpertId = ExpertList.FirstOrDefault(i => i.FirstName + " " + i.LastName == _expertName)?.Id ?? 0;
                filter.SpecialityId = specialties.FirstOrDefault(i => i.Title == _expertGroupName)?.Id ?? 0;
                feedbackList = await FeedbackService.GetFeedbackReport(filter);

                return new TableData<FeedbackReport>() { TotalItems = totalItems, Items = feedbackList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            }
        }

        //اعمال فیلتر انجام میدهد و جدول را آپدیت میکند
        private async Task GetReport()
        {
            filter.CustomerId = customers.FirstOrDefault(i => i.NameAndFamily == customerNameFilter)?.Id ?? 0;
            filter.ProductId = products.FirstOrDefault(i => i.Name == productNameFilter)?.Id ?? 0;
            firstRender = false;
            await _table!.ReloadServerData();
        }

        //لیست برانامه هارا دریافت میکند
        private async Task GetAppLists()
        {
            try
            {
                products = await ReportService.GetProdcuts();
                customers = await ReportService.GetCustomers();
                specialties = await ReportService.GetSpecialties();
                ExpertList = await ExpertService.GetExperts();
                tags = await TagService.GetTags();
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        //نام مشتری را پیدا میکند
        private async Task<IEnumerable<string>> SearchCustomerName(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
            {
                return customers.Select(i => i.NameAndFamily);
            }

            return customers.Where(i => i.NameAndFamily
            .Contains(value, StringComparison.OrdinalIgnoreCase)).Select(u => u.NameAndFamily);
        }

        //نام محصول را پیدا میکند
        private async Task<IEnumerable<string>> SearchProductName(string value)
        {
            await Task.Delay(5);

            if (string.IsNullOrEmpty(value))
            {
                return products.Select(i => i.Name);
            }

            return products.Where(i => i.Name
            .Contains(value, StringComparison.OrdinalIgnoreCase)).Select(u => u.Name);
        }

        //جزییات فیدبک را دریافت میکند
        private void OpenFeedbackDetailsDialog(int feedbackId)
        {
            DialogOptions options = new DialogOptions()
            {
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.ExtraExtraLarge
            ,
                CloseButton = true
            };
            DialogParameters parametr = new DialogParameters();
            parametr.Add("FeedbackId", feedbackId);
            DialogService.Show<FeedbackDetailsDialog>("", parametr, options);
        }

        //مدالی برای انتخاب متخصص
        private async Task ChoiceExpert()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true, MaxWidth = MaxWidth.Large, CloseButton = true };
            var choiceExpert = DialogService.Show<ChoiceKindOfExpertDialog>("", options);
            var result = await choiceExpert.Result;
            if (!result.Canceled)
            {
                var expert = ExpertList.FirstOrDefault(i => i.Id == Convert.ToInt32(result.Data));
                _expertName = expert!.FirstName + " " + expert!.LastName;
                StateHasChanged();
            }

        }

        //مدالی برای انتخاب گروه متخصص
        private async Task ChoiceExpertGroup()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true, MaxWidth = MaxWidth.Large, CloseButton = true };
            var choiceExpert = DialogService.Show<ChoiceKindOfExpertGroupDialog>("", options);
            var result = await choiceExpert.Result;
            if (!result.Canceled)
            {
                var expertGroup = specialties.FirstOrDefault(i => i.Id == Convert.ToInt32(result.Data));
                _expertGroupName = expertGroup.Title ?? string.Empty;
                StateHasChanged();
            }

        }

        public async Task RemoveFilter()
        {
            firstRender = false;
            filter = new();
            _expertName = string.Empty;
            _expertGroupName = string.Empty;
            customerNameFilter = string.Empty;
            productNameFilter = string.Empty;
            Created = null;
            RespondDate = null;
            await _table!.ReloadServerData();
        }
    }
}
