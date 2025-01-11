using Application.Services;
using CoffeeEvents.Controllers.Base;
using CoffeeEvents.Controllers.Base.Responses;
using CoffeeEvents.Controllers.EventForm.Requests;
using CoffeeEvents.Controllers.Events.Responses;
using CoffeeEvents.Utility;
using Domain.DataQuery;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeEvents.Controllers.EventForm;

[Route("/api/my-events/{eventId:guid}/form")]
[Authorize]
public class SignupFormController : Controller
{
    private readonly ControllerUtils _controllerUtils;
    private readonly BaseService<EventSignupForm> _formService;
    private readonly BaseService<FormDynamicField> _fieldsService;

    public SignupFormController(ControllerUtils controllerUtils, BaseService<EventSignupForm> formService,
        BaseService<FormDynamicField> fieldsService)
    {
        _controllerUtils = controllerUtils;
        _formService = formService;
        _fieldsService = fieldsService;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(EventFormResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseStatusResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSignupForm([FromRoute] Guid eventId)
    {
        var check = await _controllerUtils.CheckUserCanEditEventAsync(HttpContext, eventId);
        if (!check.Success)
        {
            return CustomResults.FailedRequest(check.ErrorMsg);
        }
        
        var forms = await _formService.GetAsync(new DataQueryParams<EventSignupForm>
        {
            Expression = w => w.EventId == eventId
        });
        EventSignupForm form;
        if (forms.Length != 1)
        {
            form = new EventSignupForm
            {
                Id = Guid.NewGuid(),
                IsFioRequired = false,
                IsPhoneRequired = false,
                IsEmailRequired = true,
                EventId = eventId,
            };
            await _formService.SaveAsync(form);
        }
        else
        {
            form = forms[0];
        }

        var fields = await _fieldsService.GetAsync(new DataQueryParams<FormDynamicField>
        {
            Expression = f => f.EventFormId == form.Id,
            IncludeParams = new IncludeParams<FormDynamicField>
            {
                IncludeProperties = [f => f.FieldType]
            }
        });
        var res = new EventFormResponse
        {
            IsFioRequired = form.IsFioRequired,
            IsPhoneRequired = form.IsPhoneRequired,
            IsEmailRequired = form.IsEmailRequired,
            DynamicFields = fields.Select(DtoConverter.DynamicFieldToResponse).ToArray()
        };
        return Ok(res);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(EventFormResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BaseStatusResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EditSignupForm([FromRoute] Guid eventId, [FromBody] UpdateSignupFormRequest request)
    {
        var check = await _controllerUtils.CheckUserCanEditEventAsync(HttpContext, eventId);
        if (!check.Success)
        {
            return CustomResults.FailedRequest(check.ErrorMsg);
        }
        
        var forms = await _formService.GetAsync(new DataQueryParams<EventSignupForm>
        {
            Expression = w => w.EventId == eventId
        });
        var form = forms[0];
        form.IsFioRequired = request.IsFioRequired;
        form.IsEmailRequired = request.IsEmailRequired;
        form.IsPhoneRequired = request.IsPhoneRequired;
        await _formService.SaveAsync(form);
        var fields = await _fieldsService.GetAsync(new DataQueryParams<FormDynamicField>
        {
            Expression = f => f.EventFormId == form.Id,
            IncludeParams = new IncludeParams<FormDynamicField>
            {
                IncludeProperties = [f => f.FieldType]
            }
        });
        var res = new EventFormResponse
        {
            IsFioRequired = form.IsFioRequired,
            IsPhoneRequired = form.IsPhoneRequired,
            IsEmailRequired = form.IsEmailRequired,
            DynamicFields = fields.Select(DtoConverter.DynamicFieldToResponse).ToArray()
        };
        return Ok(res);
    }
}