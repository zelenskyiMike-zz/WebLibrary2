//using Owin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WebLibrary2.Domain.Extensions
//{
//    public static class Class1
//    {
//        public static void UseOwinContextInjector(this IAppBuilder app, Container container)
//        {
//            // Create an OWIN middleware to create an execution context scope
//            app.Use(async (context, next) =>
//            {
//                using (var scope = container.BeginExecutionContextScope())
//                {
//                    await next.Invoke();
//                }
//            });
//        }
//    }
//}
