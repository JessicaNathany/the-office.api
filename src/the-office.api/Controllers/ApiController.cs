using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace the_office.api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    public ApiController(ISender sender)
    {
        Sender = sender;
    }
}