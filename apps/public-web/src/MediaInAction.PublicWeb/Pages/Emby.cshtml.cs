using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MediaInAction.PublicWeb.Pages;

public class EmbyModel : AbpPageModel
{
    public EmbyModel()
    {
    }

    public void OnGet()
    {
    }

    public IActionResult OnPostAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            Logger.LogInformation("Redirecting to Login..");
            return Challenge();
        }

        return RedirectToPage("Trakt");
    }
}