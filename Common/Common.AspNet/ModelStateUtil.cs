﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Common.AspNet;

public static class ModelStateUtil
{
    public static string GetModelStateError(this ModelStateDictionary modelState)
    {
        var errors = new Dictionary<string, List<string>>();

        if (!modelState.IsValid)
        {
            if (modelState.ErrorCount > 0)
            {
                for (int i = 0; i < modelState.Values.Count(); i++)
                {
                    var key = modelState.Keys.ElementAt(i);
                    var value = modelState.Values.ElementAt(i);

                    if (value.ValidationState == ModelValidationState.Invalid)
                    {
                        errors.Add(key, value.Errors.Select(x => string.IsNullOrEmpty(x.ErrorMessage) ? x.Exception?.Message : x.ErrorMessage).ToList());
                    }
                }
            }
        }
        var error = string.Join(" - ", errors.Select(x => $"{string.Join(" - ", x.Value)}"));
        return error;
    }
}

public class CacheOptions
{
    public int ExpireSlidingCacheFromMinutes { get; set; } = 5;
    public int AbsoluteExpirationCacheFromMinutes { get; set; } = 10;
}