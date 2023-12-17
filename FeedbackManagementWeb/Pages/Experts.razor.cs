using Domain.Entities;
using FeedbackManagementWeb.Services;
using FeedbackManagementWeb.Shared.Dialog;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection.Metadata;

namespace FeedbackManagementWeb.Pages
{
    partial class Experts
    {
        [Inject]
        private IExpertService expertService { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; } = default!;

        [Inject]
        public IDialogService DialogService { get; set; } = default!;

        IEnumerable<Expert> expertList = new List<Expert>();


        protected override async Task OnInitializedAsync()
        {
            await GetListExpert();
        }
        //لیست متخصصیت را میگیرد
        private async Task GetListExpert()
        {
            try
            {
                expertList = await expertService.GetExperts();
                StateHasChanged();
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

        //مدال برای تغییر متخصصین باز میشود
        private async Task OpenEditExpertDialog(Expert expert)
        {
            try
            {
                var expertBase = new ExpertBase()
                {
                    Address = expert.Address,
                    BirthDate = expert.BirthDate,
                    Education = expert.Education,
                    Email = expert.Email,
                    FirstName = expert.FirstName,
                    LastName = expert.LastName,
                    FieldOfStudy = expert.FieldOfStudy,
                    FkIdSpecialty = expert.FkIdSpecialty,
                    Gender = expert.Gender,
                    Language = expert.Language,
                    MobileNumber = expert.MobileNumber,
                    NationalCode = expert.NationalCode,
                    Nationality = expert.Nationality,
                    Occupation = expert.Occupation,
                    PassportNumber = expert.PassportNumber,
                    PhoneNumber = expert.PhoneNumber
                };
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
                DialogParameters parametrs = new DialogParameters();
                parametrs.Add("IsEdit", true);
                parametrs.Add("SubmitButtonColor", Color.Info);
                parametrs.Add("SubmitButtonText", "اعمال تغییرات");
                parametrs.Add("SelectedExpert", expertBase);
                parametrs.Add("IdExpertSelected", expert.Id);
                var editDialog = DialogService.Show<AddAndEditExpertDialog>("ویرایش متخصص", parametrs, options);
                var result = await editDialog.Result;
                if (!result.Cancelled)
                {
                    await GetListExpert();
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

        //مدال برای افزودن متخصص باز میشود
        private async Task AddExpert()
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = false, DisableBackdropClick = true };
                DialogParameters parametrs = new DialogParameters();
                parametrs.Add("IsEdit", false);
                parametrs.Add("SubmitButtonColor", Color.Success);
                parametrs.Add("SubmitButtonText", "افزودن جدید");
                parametrs.Add("SelectedExpert", new ExpertBase());
                var addDialog = DialogService.Show<AddAndEditExpertDialog>("افزودن متخصص", parametrs, options);
                var result = await addDialog.Result;
                if (!result.Cancelled)
                {
                    await GetListExpert();
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

        //مدال برای حذف متخصص باز میشود
        private async Task DeleteExpert(Expert expert)
        {
            try
            {
                DialogOptions options = new DialogOptions() { CloseOnEscapeKey = true };
                DialogParameters parmeters = new DialogParameters();
                parmeters.Add("ContentText", $"آیا از حذف {expert.LastName} انتخابی اطمینان دارید ");
                parmeters.Add("ButtonText", "حذف");
                parmeters.Add("Color", Color.Error);
                var dialodDelete = DialogService.Show<MessageDialog>("", parmeters, options);
                var result = await dialodDelete.Result;
                if (!result.Canceled)
                {
                    await expertService.DeleteExpert(expert.Id);
                    Snackbar.Add("مورد با موفقیت حذف شد", Severity.Success);
                    await GetListExpert();
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
