using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SSResurrezioneBR.Models.InputModels;

namespace SSResurrezioneBR.Customization.TagHelpers
{
    public class OrderLinkTagHelper: AnchorTagHelper
    {
        public string OrderBy{get;set;}
        public ListInputModel Input {get;set;}
        
        public OrderLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName ="a";
            // imposto i valori del link
            RouteValues["search"] = Input.Search;
            RouteValues["orderBy"] = OrderBy;
            RouteValues["ascending"] = (!Input.Ascending).ToString().ToLowerInvariant();
            RouteValues["chiamante"] = Input.Chiamante;
            //RouteValues["inputOrderBy"] = Input.OrderBy;
            // faccio generare l'output dall'AnchorTagHelper
            base.Process(context, output);

            output.AddClass("text-decoration-none",HtmlEncoder.Default);
            
            // aggiungo l'indicatore di direzione
            if(Input.OrderBy == OrderBy){
                var direction = Input.Ascending ? "up" : "down";
                output.PostContent.SetHtmlContent($" <i class=\"fas fa-caret-{direction}\"></i>");
                
            }
        }
    }
}