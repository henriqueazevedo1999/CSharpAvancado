using System;

namespace Common.Response
{
    public interface IResponse
    {
        public string Message { get; set; }
        public bool HasSuccess { get; set; }
        public Exception Exception { get; set; }
    }
}