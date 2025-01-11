using CoffeeEvents.Controllers.Base.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeEvents.Controllers.Base;

public static class CustomResults
{
    public static BadRequestObjectResult FailedRequest(string msg)
    {
        return new BadRequestObjectResult(new BaseStatusResponse
        {
            Status = "Ошибка",
            Message = msg,
            Completed = false
        });
    }
    
    public static BadRequestObjectResult NoUserFound()
    {
        return new BadRequestObjectResult(new BaseStatusResponse
        {
            Status = "Ошибка",
            Message = "Пользователя с указанным id не существует.",
            Completed = false
        });
    }
}