using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Paladin.Infrastructure
{
    public class XMLModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var modelType = bindingContext.ModelType;
                var serializer = new XmlSerializer(modelType);

                var inputStream = controllerContext.HttpContext.Request.InputStream;
                return serializer.Deserialize(inputStream);
            }
            catch (Exception e) {
                bindingContext.ModelState.AddModelError("", "The item could not be serialized");
                return null;
            }
        }
    }
}