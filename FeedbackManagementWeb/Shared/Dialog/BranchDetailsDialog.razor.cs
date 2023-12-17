using Domain.Entities;
using Domain.Model;
using FeedbackManagementWeb.Services;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Shared.Dialog
{
    partial class BranchDetailsDialog
    {
        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Inject]
        public IKnowledgeTreeService KnowledgeTreeService { get; set; } = default!;

        [Inject]
        public ITagService TagService { get; set; } = default!;

        [Parameter]
        public int BranchId { get; set; }

        private IEnumerable<TagsBranchModel> Tags =new List<TagsBranchModel>();

        private IEnumerable<ExpertsBranchModel> ExpertList = new List<ExpertsBranchModel>();    

        protected override async Task OnInitializedAsync()
        {
            await GetBranchTags();
            await GetBranchExperts();
        }

        private async Task GetBranchTags()
        {
            try
            {
                Tags = await KnowledgeTreeService.GetBranchTags(BranchId);
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

        private async Task GetBranchExperts()
        {
            try
            {
                ExpertList = await KnowledgeTreeService.GetBranchExperts(BranchId);
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

        private async Task OpenAddTagDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("IsEdit", false);
            parmeters.Add("AddTagToBranch", true);
            parmeters.Add("BranchId", BranchId);
            var addDialog = DialogService.Show<AddEditTagDialog>("افزودن تگ موضوعی", parmeters, options);
            var result = await addDialog.Result;
            if (result.Canceled is not true)
            {
                await GetBranchTags();
                StateHasChanged();
            }
        }

        private async Task OpenDeleteTagDialog(int relationId)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از حذف تگ انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "حذف");
                parmeters.Add("Color", Color.Error);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await KnowledgeTreeService.DeleteTagFromBranch(relationId);
                    Snackbar.Add("تگ موضوعی با موفقیت حذف شد", Severity.Success);
                    await GetBranchTags();
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

        private async Task OpenEditTagDialog(int tagId, Tag tag)
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("IsEdit", true);
            parmeters.Add("Tag", tag);
            parmeters.Add("TagId", tagId);
            var addDialog = DialogService.Show<AddEditTagDialog>("ویرایش تگ موضوعی", parmeters, options);
            var result = await addDialog.Result;
            if (result.Canceled is not true)
            {
                await GetBranchTags();
                StateHasChanged();
            }
        }

        private async Task OpenAddExpertToBranchDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false , MaxWidth=MaxWidth.Large, DisableBackdropClick = true };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("BranchId", BranchId);
            var addDialog = DialogService.Show<AddExpertToBranchDialog>("افزودن کارشناس", parmeters, options);
            var result = await addDialog.Result;
            if (result.Canceled is not true)
            {
                await GetBranchExperts();
                StateHasChanged();
            }
        }

        private async Task OpenDeleteExpertDialog(int relationId)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از حذف متخصص انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "حذف");
                parmeters.Add("Color", Color.Error);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await KnowledgeTreeService.DeleteExpertFromBranch(relationId);
                    Snackbar.Add("متخصص با موفقیت حذف شد", Severity.Success);
                    await GetBranchExperts();
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

        private async Task OpenUpdateExpertBranchDialog(int relationID)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = true, MaxWidth = MaxWidth.ExtraLarge };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("RelationId", relationID);
                var dialog = DialogService.Show<EditExpertBranchDialog>("", parmeters, options);
                var result = await dialog.Result;
                if (!result.Canceled)
                {
                    await GetBranchExperts();
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
    }
}
