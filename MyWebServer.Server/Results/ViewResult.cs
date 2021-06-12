using System.IO;
using System.Linq;
using MyWebServer.Server.Http;

namespace MyWebServer.Server.Results
{
   public class ViewResult : ActionResult
   {
       private const char PathSeparator = '/';
        public ViewResult(HttpResponse response,string viewName , string controllerName , object model)
            : base(response)
        {
            this.GetHtml(viewName , controllerName,model);
        }

        private void GetHtml(string viewName, string controllerName,object model)
        {

            if (!viewName.Contains(PathSeparator))
            {
                viewName = controllerName + PathSeparator + viewName;
            }
            var viewPath = Path.GetFullPath("./Views/" + viewName.TrimStart(PathSeparator) + ".cshtml");

            if (!File.Exists(viewPath))
            {
                PrepareMissingViewError(viewPath);
                return;
            }

            var viewContent = File.ReadAllText(viewPath);

            if (model != null)
            {
                viewContent = this.PopulateModel(viewContent, model);
            }

            this.PrepareContent(viewContent,HttpContentType.Html);
        }

        private void PrepareMissingViewError(string viewPath)
        {
            this.StatusCode = HttpStatusCode.NotFound;
            var errorMessage = $"View {viewPath} was not found.";

            this.PrepareContent(errorMessage, HttpContentType.TextPlain);
        }

        private string PopulateModel(string viewContent, object model)
        {
            var data = model.GetType().GetProperties().Select(pr => new
            {
                Name = pr.Name,
                Value = pr.GetValue(model)
            });

            foreach (var entry in data)
            {
                const string OpeningBrackets = "{{";
                const string ClosingBrackets = "}}";
                viewContent = viewContent.Replace($"{OpeningBrackets}{entry.Name}{ClosingBrackets}" , entry.Value.ToString());
            }

            return viewContent;
        }
    }
}
