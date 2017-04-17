using System;
using System.Linq;
using System.Linq.Expressions;
using Application.Domain;
using Application.Service.Contract;
using AutoMapper;
using System.Text;
using System.IO;
using System.Web;

namespace Application.Service
{
    public class ConverterProfile : Profile
    {
        protected override void Configure()
        {
            base.Configure();            

            #region Products

            //Mapper.CreateMap<Product, ProductDto>()
            //    .ForMemberEx(d => d.Name, s => s.Name)
            //    .Bidirectional();
            //Mapper.CreateMap<Guest, GuestDto>().Bidirectional();

            #endregion
            
        }        
    }

    #region Extensions
    internal static class IMappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDestination> ForMemberEx<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            Expression<Func<TDestination, object>> destinationMember,
            Expression<Func<TSource, object>> sourceMember)
        {
            return mappingExpression.ForMember(destinationMember, cfg => cfg.MapFrom(sourceMember));
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression,
            Expression<Func<TDestination, object>> destinationMember)
        {
            return mappingExpression.ForMember(destinationMember, cfg => cfg.Ignore());
        }

        public static IMappingExpression<TSource, TDestination> Bidirectional<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mappingExpression)
        {
            Mapper.CreateMap<TDestination, TSource>();

            return mappingExpression;
        }
    }
    #endregion
}
