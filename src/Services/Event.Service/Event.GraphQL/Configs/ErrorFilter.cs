using Common.Exceptions;
using HotChocolate;

namespace Event.GraphQL.Configs
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            return error.Exception is ResponseException ? error.WithMessage(error.Exception.Message) : error;
        }
    }
}
