namespace Common.AspNet;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public MetaData MetaData { get; set; }
}

public class ApiResult<TData>
{
    public bool IsSuccess { get; set; }
    public MetaData MetaData { get; set; }
    public TData Data { get; set; }
}











public  class MetaData
{
    public string  Message{ get; set; }
    public AppStatusCode StatusCode  { get; set; }
}


public enum AppStatusCode : byte
{
    Success = 1,
    NotFount = 2,
    BadRequest = 3,
    LogicError = 4,
    UnAuthorize = 5,
    ServerError=6,
}