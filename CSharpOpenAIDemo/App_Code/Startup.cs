using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharpOpenAIDemo.Startup))]
namespace CSharpOpenAIDemo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
