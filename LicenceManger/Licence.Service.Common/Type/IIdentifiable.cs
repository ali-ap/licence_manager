using System;

namespace Licence.Service.Common.Type
{
    public interface IIdentifiable
    {
        Guid Id { get; set; }
    }
}