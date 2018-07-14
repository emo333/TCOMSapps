using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TCOMSapps.Data;
using TCOMSapps.Features.Shared;

namespace TCOMSapps.Features.Home
{
  [Route("Home")]
  public class HomeController : Controller
  {
    private readonly AppInitializer _appInitializer;
    public HomeController(AppInitializer appInitializer) { _appInitializer = appInitializer; }

    [Authorize]
    [Route("")]
    [Route("/")]
    public IActionResult Index()
    {
      // _appInitializer.Initialize();.Initialize();
      return View();
    }


    [Route("Error")]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("Error/{errmsg}")]
    public IActionResult Error(string errmsg)
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMsg = errmsg });
    }
  }
}
