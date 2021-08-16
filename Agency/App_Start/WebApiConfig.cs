using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Agency.Interfaces;
using Agency.Models;
using Agency.Repository;
using Agency.Resolver;
using AutoMapper;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace Agency
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Tracing
            config.EnableSystemDiagnosticsTracing();

            // Unity
            var container = new UnityContainer();
            container.RegisterType<IAgentiRepository, AgentiRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<INekretnineRepository, NekretnineRepository>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            //AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Nekretnina, NekretninaDTO>(); // automatski će mapirati Author.Name u AuthorName
                //.ForMember(dest => dest.DrzavaIme, opt => opt.MapFrom(src => src.Zemlja.Ime)); // ako želimo eksplicitno zadati mapranje
                //cfg.CreateMap<Agent, AgentDTO>(); // automatski će mapirati Author.Name u AuthorName
                //.ForMember(dest => dest.DrzavaIme, opt => opt.MapFrom(src => src.Zemlja.Ime)); // ako želimo eksplicitno zadati mapiranje                
            });

        }
    }
}
