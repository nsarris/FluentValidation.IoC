using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;

namespace FluentValidation.IoC
{
    public interface IDuplicateValidatorResolutionStrategy
    {
        ServiceDescriptor Resolve(IEnumerable<ServiceDescriptor> duplicates);
    }
}
