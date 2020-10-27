using System.Linq;
using MethodBoundaryAspect.Fody.Attributes;

namespace Licence.Service.Common.Common.Behavior
{
    public sealed class BindingModelValidation : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            try
            {
                args.Arguments.ToList().ForEach(x =>
                 {
                     if (x.GetType().Namespace.StartsWith("Licence.Service.Common.Model.Request"))
                         x.GetType().GetMethod("Validate")?.Invoke(x, new[] { x });
                 });
            }
            catch (System.Exception exception)
            {
                throw exception.InnerException;
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
        }


        public override void OnException(MethodExecutionArgs args)
        {
        }
    }
}
