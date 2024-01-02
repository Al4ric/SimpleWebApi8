using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleWebApi8.Web.Models;
using SimpleWebApi8.Web.Services;

namespace SimpleWebApi8.Web.Pages;

public class MyThingModel : PageModel
{
    private readonly MyThingService _myThingService;

    [BindProperty]
    public MyThing MyThing { get; set; }

    public MyThingModel(MyThingService myThingService)
    {
        _myThingService = myThingService;
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        MyThing = await _myThingService.CreateMyThing(MyThing);

        return Page();
    }
}