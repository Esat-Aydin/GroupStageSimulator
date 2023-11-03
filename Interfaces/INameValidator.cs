using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simulator.Interfaces
{
    public interface INameValidator<T>
    {
        bool IsValid(T item);
        string ErrorMessage { get; }
    }
}