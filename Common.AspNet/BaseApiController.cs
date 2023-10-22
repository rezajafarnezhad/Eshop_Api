

using System.Net;
using System.Runtime.InteropServices;
using Common.Application;
using Microsoft.AspNetCore.Mvc;

namespace Common.AspNet;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected ApiResult CommandResult(OperationResult result)
    {
        return new ApiResult()
        {
            IsSuccess = result.Status == OperationResultStatus.Success,
            MetaData = new MetaData()
            {
                Message = result.Message,
                StatusCode = EnumHelper.MapOperationStatus(result.Status)
            },
        };
    }


    protected ApiResult<TData> CommandResult<TData>(OperationResult<TData> result
        ,HttpStatusCode statusCode=HttpStatusCode.OK , string url=null)
    {
        bool isSuccess = result.Status == OperationResultStatus.Success;
        if (isSuccess)
        {
            HttpContext.Response.StatusCode = (int)statusCode;
            if (!string.IsNullOrWhiteSpace(url))
            {
                HttpContext.Response.Headers.Add("location" , url);
            }
        }
        
        return new ApiResult<TData>()
        {
            IsSuccess = isSuccess,
            Data = isSuccess?result.Data:default,
            MetaData = new MetaData()
            {
                Message = result.Message,
                StatusCode = EnumHelper.MapOperationStatus(result.Status)
            },
        };
    }


    protected ApiResult<TData> QueryResult<TData>(TData result)
    {
        return new ApiResult<TData>()
        {
            IsSuccess = true,
            Data =result,
            MetaData = new MetaData()
            {
                Message = "عملیات انجام شد",
                StatusCode = AppStatusCode.Success
    },
        };
    }
}

public static class EnumHelper
{
    public static AppStatusCode MapOperationStatus(OperationResultStatus status)
    {
        switch (status)
        {
            case OperationResultStatus.Success:
                return AppStatusCode.Success;

            case OperationResultStatus.Error:
                return AppStatusCode.LogicError;

            case OperationResultStatus.NotFound:
                return AppStatusCode.NotFount;
        }
        return AppStatusCode.LogicError;
    }
}