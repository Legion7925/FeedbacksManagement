using Domain.Entities;
using Domain.Shared.Enums;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Shared.Dialog;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class SentToCustomerFeedback
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

        public async Task<TableData<FeedbackReport>> GetFeedbacks(TableState state)
        {

            try
            {
                var totalItems = await FeedbackService.GetFeedbackCount(FeedbackState.Completed);
                if (totalItems == 0)
                {
                    //if we don't fill the total items and the items we will get a null refrence exception
                    return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
                }
                feedbackList = await FeedbackService.GetFeedbacks(state.PageSize, state.Page * state.PageSize, FeedbackState.Completed);

                return new TableData<FeedbackReport>() { TotalItems = totalItems, Items = feedbackList };
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
                return new TableData<FeedbackReport>() { TotalItems = 0, Items = new List<FeedbackReport>() };
            }
        }

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
