﻿using Azure;
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
    partial class ReportOneFeedback
    {

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        [Inject]
        public IFeedbackService FeedbackService { get; set; } = default!;
        [Inject]
        public ITagService TagService { get; set; } = default!;

        [Inject]
        private IAnswerService answerService { get; set; }

        private FeedbackReport Feedback = new();

        private IEnumerable<Tag> Tags = new List<Tag>();

        private IEnumerable<Ansower> answers = new List<Ansower>();


        private string serialNumber = string.Empty;

        private string respondPattern = string.Empty;

        //با سریال نامبر گزارش فیدبک را میگیرد
        private async Task GetReport()
        {
            try
            {
                if (string.IsNullOrEmpty(serialNumber))
                {
                    Snackbar.Add("شماره مورد نمیتواند خالی باشد", Severity.Warning);
                    return;
                }
                Feedback = await FeedbackService.GetFeedbackBySerialNumber(serialNumber);
                await GetTagsList(Feedback.Id);
                await GetAnswerForFeedback();
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

        //لیست تگ را میگیرد
        private async Task GetTagsList(int feedbackId)
        {
            try
            {
                Tags = await FeedbackService.GetTagsForOneFeedback(feedbackId);
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

        //مدالی برا افزودن تگ است
        private async Task OpenAddTagDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("IsEdit", false);
            parmeters.Add("AddTagToFeedback", true);
            parmeters.Add("FeedbackId", Feedback.Id);
            var addDialog = DialogService.Show<AddEditTagDialog>("افزودن تگ موضوعی", parmeters, options);
            var result = await addDialog.Result;
            if (result.Canceled is not true)
            {
                await GetTagsList(Feedback.Id);
                StateHasChanged();
            }
        }

        //جواب را برای فیدبک میگیرد
        private async Task GetAnswerForFeedback()
        {
            try
            {
                answers = await answerService.GetAnswersForFeedback(Feedback.Id);
            }
            catch (AppException ax)
            {
                Snackbar.Add(ax.Message, Severity.Warning);
            }
            catch (Exception)
            {
                Snackbar.Add("در واکشی اطلاعات خطایی رخ داده", Severity.Error);
            }
        }

        //مدالی برای تایید جواب
        private async Task ConfirmAnswer(int answer)
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            DialogParameters parametr = new DialogParameters();
            parametr.Add("ContentText", "آیا میخواهید این پاسخ تایید نهایی شود؟");
            parametr.Add("ButtonText", "تایید");
            parametr.Add("Color", Color.Success);
            var confirmAnswerDialog = DialogService.Show<MessageDialog>("", parametr, options);
            var result = await confirmAnswerDialog.Result;
            if (!result.Canceled)
            {
                try
                {
                    await FeedbackService.ConfirmFeedbackAnswer(Feedback.Id, answer);
                    Snackbar.Add("پاسخ با موفقیت ثبت شد", Severity.Success);
                    await GetAnswerForFeedback();
                }
                catch (AppException ax)
                {
                    Snackbar.Add(ax.Message, Severity.Warning);
                }
                catch (Exception)
                {
                    Snackbar.Add("در تایید پاسخ خطایی رخ داده", Severity.Error);
                }
            }
        }
        //مدالی برای حذف تگ
        private async Task OpenDeleteTagDialog(int tagId)
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
                    await FeedbackService.DeleteTagFromFeedback(Feedback.Id, tagId);
                    Snackbar.Add("تگ موضوعی با موفقیت حذف شد", Severity.Success);
                    await GetTagsList(Feedback.Id);
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

        //مدالی برای تغییر تگ
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
                await GetTagsList(Feedback.Id);
                StateHasChanged();
            }
        }

        //مدالی برای افزودن جواب برای فیدبک
        private async Task OpenAddResponseDialog()
        {
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
            DialogParameters parametr = new DialogParameters();
            parametr.Add("IdFeedback", Feedback.Id);
            var addResponseDialog = DialogService.Show<AddResponseExpert>("", parametr, options);
            var result = await addResponseDialog.Result;
            if (result.Canceled is not true)
            {
                await GetAnswerForFeedback();
            }
        }

        //فیدبک را متخصص مورد تظر انتخاب کرده و میتوان جواب داد
        public async Task OpenSubmitToExpertDialog()
        {
            var idFeedback = new List<int> { Feedback.Id };
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = false };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("FeedbackIds", idFeedback);
            var submitDialog = DialogService.Show<SubmitFeedbackToExpertDialog>("ارسال به متخصص", parmeters, options);
            var result = await submitDialog.Result;
            if (!result.Canceled)
            {
                await GetReport();
            }
        }

        //فیدبک را گروه متخصص مورد تظر انتخاب کرده و میتوان جواب داد
        public async Task OpenSubmitToExpertGroupDialog()
        {
            var idFeedback = new List<int> { Feedback.Id };
            DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true, CloseButton = false };
            DialogParameters parmeters = new DialogParameters();
            parmeters.Add("FeedbackIds", idFeedback);
            var submitDialog = DialogService.Show<SubmitFeedbackToExpertGroupDialog>("ارسال به گروه متخصصین", parmeters, options);
            var result = await submitDialog.Result;
            if (!result.Canceled)
            {
                await GetReport();
            }
        }
    }
}
