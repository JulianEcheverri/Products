using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Products.Startup))]
namespace Products
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
