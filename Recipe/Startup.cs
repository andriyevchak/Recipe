using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Recipe.Startup))]
namespace Recipe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
