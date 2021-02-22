
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SportsStore.IdentityDomain.Entities;
using SportsStore.IdentityDomain.Infrastructure;


namespace SportsStore.WebUI.Infrastructure.IEnumerableExtension
{
    static public class IEnumerableExtensionMethods
    {
        public static IEnumerable<ElementType> UnnestHierarchy<ElementType>(this IEnumerable<IEnumerable<ElementType>> selfObject) 
        {
            foreach (IEnumerable<ElementType> insideIEnumerable in selfObject)
            {
                foreach (ElementType element in insideIEnumerable)
                {
                    yield return element;
                }
            }
        }
    }
}