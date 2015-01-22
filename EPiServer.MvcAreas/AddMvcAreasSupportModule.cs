using System.Web;
using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using InitializationModule = EPiServer.Web.InitializationModule;

namespace EPiServer.MvcAreas
{
    [ModuleDependency(typeof(InitializationModule))]
    public class AddMvcAreasSupportModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            GlobalFilters.Filters.Add(ServiceLocator.Current.GetInstance<DetectAreaAttribute>());
            ContentRoute.RoutingContent += OnRoutingContent;
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }

        private void OnRoutingContent(object sender, RoutingEventArgs e)
        {
            PartialViewsInAreasRegistrar.Register(new HttpContextWrapper(HttpContext.Current));
            ContentRoute.RoutingContent -= OnRoutingContent;
        }
    }
}
