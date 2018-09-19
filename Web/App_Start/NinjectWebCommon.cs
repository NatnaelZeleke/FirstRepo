using System;
using System.Web; 
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using Domain;
using Domain.Services;
using Domain.UnitOfWork;
using Domain.UnitOfWork.Concrete; 
using Web.Infrastructure;
using Web.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Web.App_Start.NinjectWebCommon), "Stop")]

namespace Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Domain.Repository;
    using Domain.Repository.Concrete;

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
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            kernel.Bind<ApplicationUserManager>().ToMethod(GetOwinInjection<ApplicationUserManager>);
            kernel.Bind<ApplicationSignInManager>().ToMethod(GetOwinInjection<ApplicationSignInManager>);
            kernel.Bind<ApplicationRoleManager>().ToMethod(GetOwinInjection<ApplicationRoleManager>);
            kernel.Bind<IAuthenticationManager>().ToMethod(context =>
           {
               var contextBase = new HttpContextWrapper(HttpContext.Current);
               return contextBase.GetOwinContext().Authentication;
           });
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IEmailSenderService>().To<EmailSenderService>();
            kernel.Bind<ITenderTypeService>().To<TenderTypeService>();
            kernel.Bind<ITenderService>().To<TenderService>();
            kernel.Bind<IUserTenderTypeChoiceService>().To<UserTenderTypeChoiceService>();
            kernel.Bind<INewsPaperService>().To<NewsPaperService>();
            kernel.Bind<IUserSavedTenderService>().To<UserSavedTenderService>();
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IUsersBySalesService>().To<UserBySalesService>();
            kernel.Bind<IFileService>().To<FileService>();
//            kernel.Bind<IOcrService>().To<OcrService>();
            kernel.Bind<IConverter>().To<Converter>();

        }

        private static T GetOwinInjection<T>(IContext context) where T : class
        {
            var contextBase = new HttpContextWrapper(HttpContext.Current);
            return contextBase.GetOwinContext().Get<T>();
        }

 
    }
}
