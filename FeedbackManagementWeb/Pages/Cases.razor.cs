using Domain.Entities;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Shared.Dialog;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class Cases
    {
        [Inject]
        private ICaseService CaseService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IEnumerable<CaseReport> casesList = new List<CaseReport>();

        private bool _loading = false;

        private HashSet<CaseReport> selectedCases = new();

        private readonly int[] _pageSizeOption = { 10, 20, 30 };
        private MudTable<CaseReport>? _table = new();

        //لیست مورد های رسیده را از سرور میگیرد و صفحه بندی میکند داخل جدول قرار میدهد
        public async Task<TableData<CaseReport>> GetCases(TableState state)
        {

            try
            {
                var totalItems = await CaseService.GetCasesCount();
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    return new TableData<CaseReport>() { TotalItems = 0, Items = new List<CaseReport>() };
                }
                casesList = await CaseService.GetCases(state.PageSize, state.Page * state.PageSize);

                return new TableData<CaseReport>() { TotalItems = totalItems, Items = casesList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<CaseReport>() { TotalItems = 0, Items = new List<CaseReport>() };
            }
        }

        //مورد انتخاب شده را حذف میکند
        public async Task DeleteSelectedCases()
        {
            try
            {
                var caseIds = selectedCases.Select(i => i.Id).ToArray();
                if (!caseIds.Any())
                {
                    Snackbar.Add("موردی برای حذف انتخاب نشده است", Severity.Warning);
                    return;
                }
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از حذف موارد انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "حذف");
                parmeters.Add("Color", Color.Error);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await CaseService.DeleteCases(caseIds);
                    Snackbar.Add("مورد با موفقیت حذف شد", Severity.Success);
                    await _table!.ReloadServerData();
                    StateHasChanged();
                }
            }
            catch (AppException ax)
            {
                Snackbar.Add(ax.Message, Severity.Warning);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        //مورد انتخابی را میتوان برای جواب دادن به سمت متخصص یا گروه متخصصین ارسال کرد
        public async Task SendCasesForAnswer()
        {
            try
            {
                var caseIds = selectedCases.Select(i => i.Id).ToArray();
                if (!caseIds.Any())
                {
                    Snackbar.Add("موردی برای ارسال انتخاب نشده است", Severity.Warning);
                    return;
                }
                foreach (var id in caseIds)
                {
                    await CaseService.SubmitCaseForAnswer(id);
                }
                Snackbar.Add("موارد انتخابی با موفقیت برای پاسخ دهی ارسال شدند", Severity.Success);
                await _table!.ReloadServerData();
                StateHasChanged();
            }
            catch (AppException ax)
            {
                Snackbar.Add(ax.Message, Severity.Warning);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }
        //میتوان مورد جدید اضافه کرد
        private async void OpenAddCaseDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            var addDialog = DialogService.Show<AddCaseDialog>("افزودن مورد", options);
            var result = await addDialog.Result;
            if (result.Canceled is not true)
            {
                await _table!.ReloadServerData();
                StateHasChanged();
            }
        }
        //جزییات مورد را پاسخ میدهد
        public void ShowCaseDetails(CaseReport @case)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, CloseButton = true, NoHeader = true, MaxWidth = MaxWidth.ExtraLarge };
            var parameters = new DialogParameters();
            parameters.Add("Case", @case);
            DialogService.Show<CaseDetailsDialog>(string.Empty, parameters, options);
        }
    }
}
