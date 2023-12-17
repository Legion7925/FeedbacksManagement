using Domain.Entities;
using Domain.Shared.Enums;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Shared.Dialog;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class ArchivedFeedbacks
    {
        [Inject]
        private IFeedbackService FeedbackService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private IEnumerable<FeedbackReport> feedbackList = new List<FeedbackReport>();

        private bool _loading = false;

        private HashSet<FeedbackReport> selectedFeedbacks = new();

        private readonly int[] _pageSizeOption = { 10, 20, 30 };

        private MudTable<FeedbackReport>? _table = new();

        //لیست فبدبک هارا از سرور میگیرید و داخل جدول قرار میدهد و صفحه بندی را انجام میدهد
        public async Task<TableData<FeedbackReport>> GetFeedbacks(TableState state)
        {
            try
            {
                var totalItems = await FeedbackService.GetFeedbackCount(FeedbackState.Archived);
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
                }
                feedbackList = await FeedbackService.GetFeedbacks(state.PageSize, state.Page * state.PageSize, FeedbackState.Archived);

                return new TableData<FeedbackReport>() { TotalItems = totalItems, Items = feedbackList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            }
        }

        //فیدبک حذف شده را به لیست بر میگرداند
        public async Task RecycleSelectedCases()
        {
            try
            {
                var feedbackIds = selectedFeedbacks.Select(i => i.Id).ToArray();
                if (!feedbackIds.Any())
                {
                    Snackbar.Add("موردی برای بازیابی انتخاب نشده است", Severity.Warning);
                    return;
                }
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از بازیابی موارد انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "بازیابی موارد");
                parmeters.Add("Color", Color.Info);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await FeedbackService.RecycleFeedbacks(feedbackIds);
                    Snackbar.Add("عملیات بازیابی با موفقیت انجام شد", Severity.Success);
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

        //جزییات فیدبک را در مدال باز نمایش میدهد
        private void OpenFeedbackDetailsDialog(int feedbackId)
        {
            DialogOptions options = new DialogOptions()
            {
                CloseOnEscapeKey = false,
                DisableBackdropClick = true,
                MaxWidth = MaxWidth.ExtraExtraLarge,
                CloseButton = true
            };
            DialogParameters parametr = new DialogParameters();
            parametr.Add("FeedbackId", feedbackId);
            DialogService.Show<FeedbackDetailsDialog>("", parametr, options);
        }
    }
}
