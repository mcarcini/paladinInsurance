using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using System.Linq;

namespace Paladin.Infrastructure
{
    public static class RazorWorkflowExtensions
    {
        public static MvcHtmlString WorkflowActionLink(this HtmlHelper html, string action, string controller, 
            string displayText, int linkTargetStage, int currentStage, int minRequiredStage, int highestCompletedStage) {
            if(highestCompletedStage >= minRequiredStage) {
                //Generate URL
                var targetURL = UrlHelper.GenerateUrl("Default", action, controller, null, RouteTable.Routes, html.ViewContext.RequestContext, false);

                //Create <a> Tag
                var anchorBuilder = new TagBuilder("a");
                anchorBuilder.MergeAttribute("href", targetURL);

                //Assign CSS classes
                string classes = "btn btn-progress";
                if (linkTargetStage == currentStage) {
                    classes += " btn-progress-active";
                }
                anchorBuilder.MergeAttribute("class", classes);

                //return as MVC string
                anchorBuilder.InnerHtml = displayText;
                return new MvcHtmlString(anchorBuilder.ToString(TagRenderMode.Normal));
            }
            else {
                var spanBuilder = new TagBuilder("span");
                spanBuilder.MergeAttribute("class", "bnt btn-progress");
                spanBuilder.InnerHtml = displayText;
                return new MvcHtmlString(spanBuilder.ToString(TagRenderMode.Normal));
            }
        }
    }
}