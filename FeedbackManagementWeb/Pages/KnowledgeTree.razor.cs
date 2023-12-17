using Domain.Entities;
using FeedbackManagementWeb.Interface;
using FeedbackManagementWeb.Services;
using FeedbackManagementWeb.Shared.Dialog;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FeedbackManagementWeb.Pages
{
    partial class KnowledgeTree
    {
        [Inject]
        private IKnowledgeTreeService KnowledgeTreeService { get; set; } = default!;

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        private HashSet<KnowledgeTreeReport> treeList = new HashSet<KnowledgeTreeReport>();

        private bool _loading = false;

        protected override async Task OnInitializedAsync()
        {
            await GetTree();
        }
        //اطلاعات درخت را از سرور میگیرد
        private async Task GetTree()
        {
            try
            {
                var result = await KnowledgeTreeService.GetKnowledgeTree();
                treeList = new HashSet<KnowledgeTreeReport>(result);
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
        //ریشه جدید در درخت ایجاد میکند
        private async Task OpenAddRootDialog()
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
                DialogParameters parametrs = new DialogParameters();
                parametrs.Add("IsEdit", false);
                parametrs.Add("SubmitButtonColor", Color.Success);
                parametrs.Add("SubmitButtonText", "ثبت");
                var addDialog = DialogService.Show<AddEditBranchDialog>("افزودن درخت", parametrs, options);
                var result = await addDialog.Result;
                if (!result.Canceled)
                {
                    await GetTree();
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

        //شاخه به ریشه درخت اضافه میکند
        private async Task OpenAddBranchDialog(int parentId)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
                DialogParameters parametrs = new DialogParameters();
                parametrs.Add("IsEdit", false);
                parametrs.Add("IsRoot", false);
                parametrs.Add("ParentId", parentId);
                parametrs.Add("SubmitButtonColor", Color.Success);
                parametrs.Add("SubmitButtonText", "ثبت");
                var addDialog = DialogService.Show<AddEditBranchDialog>("افزودن شاخه", parametrs, options);
                var result = await addDialog.Result;
                if (!result.Canceled)
                {
                    await GetTree();
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
        //مدال برای تغییر شاخه
        private async Task OpenEditBranchDialog(KnowledgeTreeReport branch)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
                DialogParameters parametrs = new DialogParameters();
                parametrs.Add("IsEdit", true);
                parametrs.Add("IsRoot", false);
                parametrs.Add("BranchId", branch.Id);
                parametrs.Add("ParentId", branch.ParentId);
                parametrs.Add("Branch", branch);
                parametrs.Add("SubmitButtonColor", Color.Info);
                parametrs.Add("SubmitButtonText", "ثبت تغییرات");
                var addDialog = DialogService.Show<AddEditBranchDialog>("ویرایش شاخه", parametrs, options);
                var result = await addDialog.Result;
                if (!result.Canceled)
                {
                    await GetTree();
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

        //شاخه را حذف میکند
        private async Task OpenDeleteBranchDialog(KnowledgeTreeReport selectedBranch)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از حذف {selectedBranch.Name} انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "حذف");
                parmeters.Add("Color", Color.Error);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await KnowledgeTreeService.DeleteBranch(selectedBranch.Id);
                    Snackbar.Add("عملیات حذف با موفقیت انجام شد", Severity.Success);
                    await GetTree();
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

        //مدالی برای جزییات شاخه
        private void OpenBranchDetailsDialog(int branchId)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true , CloseButton=true , MaxWidth = MaxWidth.ExtraLarge};
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("BranchId", branchId);
                DialogService.Show<BranchDetailsDialog>("", parmeters, options);
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

        //مدالی برای تغییر ریشه درخت
        private async Task OpenChangeParentDialog(KnowledgeTreeReport selectedBranch)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true , CloseButton=true , MaxWidth = MaxWidth.ExtraLarge};
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("BranchId", selectedBranch.Id);
                parmeters.Add("BranchName", selectedBranch.Name);
                var dialogResult = DialogService.Show<ChangeBranchParentDialog>("", parmeters, options);
                var result = await dialogResult.Result;
                if (!result.Canceled)
                {
                    await GetTree();
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
