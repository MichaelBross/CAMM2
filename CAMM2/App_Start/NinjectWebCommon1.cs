
// Generated Code! Do not manually edit. Adjust template (NinjectWebCommon.tt) to make changes to this file.
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(App_Start.NinjectWebCommon), "Stop")]

namespace App_Start
{
    using System;

    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using Persistance;
    using Persistance.Repository;    
    using Application.Interfaces;
    using Application.Service;
    

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {  
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IUserService>().To<UserService>();

            kernel.Bind<IDocumentRepository>().To<DocumentRepository>();
            kernel.Bind<IDocumentService>().To<DocumentService>();

            kernel.Bind<IComponentRepository>().To<ComponentRepository>();
            kernel.Bind<IComponentService>().To<ComponentService>();

            kernel.Bind<IConnectorRepository>().To<ConnectorRepository>();
            kernel.Bind<IConnectorService>().To<ConnectorService>();

            kernel.Bind<IContactRepository>().To<ContactRepository>();
            kernel.Bind<IContactService>().To<ContactService>();

            kernel.Bind<IItemRepository>().To<ItemRepository>();
            kernel.Bind<IItemService>().To<ItemService>();

            kernel.Bind<IToolRepository>().To<ToolRepository>();
            kernel.Bind<IToolService>().To<ToolService>();

            kernel.Bind<IAssemblyRepository>().To<AssemblyRepository>();
            kernel.Bind<IAssemblyService>().To<AssemblyService>();

            kernel.Bind<IAssemblyItemRepository>().To<AssemblyItemRepository>();
            kernel.Bind<IAssemblyItemService>().To<AssemblyItemService>();

            kernel.Bind<IWorkOrderRepository>().To<WorkOrderRepository>();
            kernel.Bind<IWorkOrderService>().To<WorkOrderService>();

        }        
    }
}
