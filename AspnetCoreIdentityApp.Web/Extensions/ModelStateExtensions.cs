using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AspnetCoreIdentityApp.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModalErrorList(this ModelStateDictionary modelstate, List<string> errors)
        {
            errors.ForEach(error =>
            {
                modelstate.AddModelError(String.Empty, error);
            });
        }
    }
}
