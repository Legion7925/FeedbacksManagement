using Domain.Entities;
using Domain.Shared.Enums;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Services;
using FeedbackManagementWeb.Shared.Dialog;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class Feedback
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

        private bool showHighSimilarity = false;

        //لیست فیدبک را از سرور میگیرد و صفحه بندی میکند داخل جدول قرار میدهد
        public async Task<TableData<FeedbackReport>> GetFeedbacks(TableState state)
        {
            try
            {
                var totalItems = await FeedbackService.GetFeedbackCount(FeedbackState.ReadyToSend);
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
                }
                feedbackList = await FeedbackService.GetFeedbacks(state.PageSize, state.Page * state.PageSize, FeedbackState.ReadyToSend);
                if (showHighSimilarity)
                    feedbackList = feedbackList.OrderByDescending(i => i.Similarity);


                return new TableData<FeedbackReport>() { TotalItems = totalItems, Items = feedbackList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            }
        }

        //مورد انتخابی را حذف میکند
        public async Task DeleteSelectedCases()
        {
            try
            {
                var feedbackIds = selectedFeedbacks.Select(i => i.Id).ToArray();
                if (!feedbackIds.Any())
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
                    await FeedbackService.DeleteFeedbacks(feedbackIds);
                    Snackbar.Add("عملیات حذف با موفقیت انجام شد", Severity.Success);
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

        //مورد انتخابی را بایگانی میکند
        public async Task ArchiveSelectedCases()
        {
            try
            {
                var feedbackIds = selectedFeedbacks.Select(i => i.Id).ToArray();
                if (!feedbackIds.Any())
                {
                    Snackbar.Add("موردی برای بایگانی انتخاب نشده است", Severity.Warning);
                    return;
                }
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از بایگانی موارد انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "ارسال به بایگانی");
                parmeters.Add("Color", Color.Info);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await FeedbackService.ArchiveFeedbacks(feedbackIds);
                    Snackbar.Add("عملیات بایگانی با موفقیت انجام شد", Severity.Success);
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
        //مورد را برای متخصص میفرستد برای جواب دهی
        public async Task OpenSubmitToExpertDialog()
        {
            var idList = selectedFeedbacks.Select(i => i.Id).ToList();
            if (!idList.Any())
            {
                Snackbar.Add("موردی برای ارسال انتخاب نشده", Severity.Warning);
            }
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = false };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("FeedbackIds", idList);
            var submitDialog = DialogService.Show<SubmitFeedbackToExpertDialog>("ارسال به متخصص", parmeters, options);
            var result = await submitDialog.Result;
            if (!result.Canceled)
            {
                await _table!.ReloadServerData();
                StateHasChanged();
            }
        }

        //مورد را برای گروه متخصص میفرستد برای جواب دهی
        public async Task OpenSubmitToExpertGroupDialog()
        {
            var idList = selectedFeedbacks.Select(i => i.Id).ToList();
            if (!idList.Any())
            {
                Snackbar.Add("موردی برای ارسال انتخاب نشده", Severity.Warning);
            }
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = false };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("FeedbackIds", idList);
            var submitDialog = DialogService.Show<SubmitFeedbackToExpertGroupDialog>("ارسال به گروه متخصصین", parmeters, options);
            var result = await submitDialog.Result;
            if (!result.Canceled)
            {
                await _table!.ReloadServerData();
                StateHasChanged();
            }
        }

        //افزودن فیدبک برای ارجاع به متخصص
        private async Task OpenAddFeedbackByExpertDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            var addFeedback = DialogService.Show<AddFeedbackByExpert>("", options);
            var result = await addFeedback.Result;
            if (!result.Canceled)
            {
                await _table.ReloadServerData();
                StateHasChanged();
            }
        }

        //اعمال فیلتر
        private async Task FilterSimilarity(bool showHigh)
        {
            showHighSimilarity = showHigh;
            await _table!.ReloadServerData();
            StateHasChanged();
        }

        //فیدبک را اماده برای ارسال برای مشتری میکند
        private async Task OpenSubmitToReadyToSendDialog()
        {
            try
            {
                var feedbackIds = selectedFeedbacks.Select(i => i.Id).ToArray();
                if (!feedbackIds.Any())
                {
                    Snackbar.Add("موردی برای ارسال انتخاب نشده است", Severity.Warning);
                    return;
                }
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از ارسال موارد انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "ارسال");
                parmeters.Add("Color", Color.Info);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await FeedbackService.SendToReadyToSendCustomer(feedbackIds);
                    Snackbar.Add("عملیات ارسال با موفقیت انجام شد", Severity.Success);
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

        //افزودن جواب برای مشتری
        private void OpenAddResponseDialog(int feedbackId)
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            DialogParameters parametr = new DialogParameters();
            parametr.Add("IdFeedback", feedbackId);
            var addResponseDialog = DialogService.Show<AddResponseExpert>("", parametr, options);
        }

        //نمایش جزییات فیدبک
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

    }
}
