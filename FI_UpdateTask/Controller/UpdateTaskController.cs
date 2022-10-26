using FI_Aplication_Aggregate;
using FI_Infra_Tools_Aggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Diagnostics;
using System.Net.Mime;

namespace FIAPI;

public class UpdateTaskController : ControllerBase
{
    private readonly IUpdateTaskService _UpdateTaskService;
    private readonly ILog _log;
    private readonly IActionContextAccessor _accessor;
    public UpdateTaskController(IUpdateTaskService UpdateTaskService,
                                    ILog log,
                                    IActionContextAccessor accessor
                                   )
    {
        _UpdateTaskService = UpdateTaskService;
        _log = log;
        _accessor = accessor;
    }




    

    [Authorize]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/UpdateTask")]

    public IActionResult Invoque([FromBody] FI_Aplication_Core.TaskDTO task)
    {
        IActionResult result;
        try
        {
            result = Ok(_UpdateTaskService.Invoque(task));
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
                 NameAPI = "api/UpdateTask",
                 DateCreated = System.DateTime.Now,
                 LogID = System.Guid.NewGuid(),
                 Content = contentTrace,
                 DataSource = dataSource,
                 IPSource = _accessor.ActionContext.HttpContext.Connection.RemoteIpAddress.ToString()



             });

    }


}


