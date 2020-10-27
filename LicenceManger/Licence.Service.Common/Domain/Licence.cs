using System;
using Licence.Service.Common.Type;

namespace Licence.Service.Common.Domain
{
    public class Licence : IIdentifiable
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Key { get; set; }
        public DateTime CreationDate { get; set; }
        public string Value { get; set; }
    }
}
