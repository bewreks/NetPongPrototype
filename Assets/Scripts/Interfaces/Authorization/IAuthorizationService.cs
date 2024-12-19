using System;
using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface IAuthorizationService : IAuthorizable, IModelContainer<IAuthorizationModel>, IDisposable
    {
    }
}