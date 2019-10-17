using MindfireSolutions.Custom;
using MindfireSolutions.Service.ServiceClass;
using MindfireSolutions.Service.ServiceInterface;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace MindfireSolutions
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<ICustomHelper, CustomHelper>();
            container.RegisterType<IManageUser, ManageUser>();
            container.RegisterType<IAuthor, Author>();
            container.RegisterType<IBlogManager, BlogManager>();
            container.RegisterType<IIndex, Index>();
            container.RegisterType<IContactMessage, ContactMessage>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}