using System;

namespace Common;

public class Response : BaseResponse
{
    public Response(bool hasSuccess, string message) : base(hasSuccess, message)
    {
    }

    public Response(Exception ex) : base(ex)
    {
    }
}
