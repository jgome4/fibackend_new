using FI_Aplication_Aggregate;
using FI_Infra_Tools_Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;
using System.Net.Mime;

namespace FIAPI;

public class GetTaskController : ControllerBase
{
    private readonly IGetTasksService _GetTasksService;
    private readonly ILog _log;
    private readonly IActionContextAccessor _accessor;
    public GetTaskController(IGetTasksService GetTasksService,
                                    ILog log,
                                    IActionContextAccessor accessor
                                   )
    {
        _GetTasksService = GetTasksService;
        _log = log;
        _accessor = accessor;
    }




    

    [Authorize]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/GetTask")]

    public IActionResult Invoque()
    {
        IActionResult result;
        try
        {
            result = Ok(_GetTasksService.Invoque());
            return result;
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message + "-" + ex.InnerException);
        }
    }

    private void InsertLogTrace(string contentTrace, string dataSource)
    {
        StackTrace stackTrace = new StackTrace();

        _log.InsertTraceLog(
             new FI_Infra_Tools_Core.Log
             {
                 TypeLog = FI_Infra_Tools_Core.TypeLog.ERROR,
                 NameMethod = "Invoque",
                 NameAPI = "api/GetTasks",
                 DateCreated = System.DateTime.Now,
                 LogID = System.Guid.NewGuid(),
                 Content = contentTrace,
                 DataSource = dataSource,
                 IPSource = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()



             });

    }


}


