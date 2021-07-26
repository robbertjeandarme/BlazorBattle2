using System.Security.Principal;

namespace BlazorBattle2.Shared
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }

        public bool Succes { get; set; } = true;

        public string Message { get; set; }
    }
}