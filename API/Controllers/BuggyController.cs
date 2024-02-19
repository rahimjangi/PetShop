using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;

    public BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var notExist = _context.Products.Find(420);
        if (notExist == null) return NotFound(new ApiResponse(404));
        return Ok();
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var notExist = _context.Products.Find(420);
        var toGenerateError = notExist.ToString();
        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetValidationError(int id)
    {
        return Ok();
    }
}
