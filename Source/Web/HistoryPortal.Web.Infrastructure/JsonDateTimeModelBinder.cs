namespace HistoryPortal.Web.Infrastructure
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;
    public class JsonDateTimeModelBinder : DefaultModelBinder
    {
        public const string JsonDatePattern = @"/date\((\-?[0-9]+)\)/";
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string attemptedValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;
            if (!Regex.IsMatch(attemptedValue, JsonDatePattern, RegexOptions.IgnoreCase))
                return base.BindModel(controllerContext, bindingContext);
            long miliseconds = long.Parse(Regex.Match(attemptedValue, JsonDatePattern, RegexOptions.IgnoreCase).Groups[1].Value);
            DateTime epoc = new DateTime(1970, 1, 1);
            return epoc.AddMilliseconds(miliseconds);
        }
    }
}
