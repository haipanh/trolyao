using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Solar.Core;
using AutoMapper;
using Application.Service.Contract;
using System.Reflection;
using System.Collections;

namespace Application.Service
{
    public static class Converter
    {
        static Converter()
        {
            Initialize();
        }

        private static void Initialize()
        {
            Mapper.AddProfile<ConverterProfile>();
        }

        internal static TDestination ExposedAs<TDestination>(this DomainEntity entity)
            where TDestination : DtoBase
        {
            return entity == null
              ? default(TDestination)
              : (TDestination)Mapper.Map(entity, entity.GetType(), typeof(TDestination));
        }

        internal static TDestination ExposedAs<TDestination>(this DtoBase dto)
            where TDestination : DomainEntity
        {
            if (dto == null)
            {
                return default(TDestination);
            }

            using (var repository = DomainRepository.Open())
            {
                using (RepositoryScope.Share(repository))
                {
                    var entity = dto.Id == 0 ? null : repository.Get<TDestination>(dto.Id);

                    if (!typeof(IReadOnly).IsAssignableFrom(typeof(TDestination)))
                    {
                        if (entity != null)
                        {
                            return entity.UpdatedWith(dto);
                        }
                    }

                    var domainObject = (TDestination)Mapper.Map(dto, dto.GetType(), typeof(TDestination));

                    UpdateModifiedDate(domainObject);

                    return domainObject;
                }
            }
        }

        internal static TEntity UpdatedWith<TEntity, TDto>(this TEntity entity, TDto dto)
            where TEntity : DomainEntity
        {
            Mapper.Map(dto, entity, dto.GetType(), entity.GetType());

            UpdateModifiedDate(entity);

            return entity;
        }

        private static void UpdateModifiedDate(object entity)
        {
            if (entity != null)
            {
                Type type = entity.GetType();

                PropertyInfo[] properties = type.GetProperties();

                if (properties.Any(p => p.Name.Equals("ModifiedDate")))
                {
                    properties.FirstOrDefault(p => p.Name.Equals("ModifiedDate")).SetValue(entity, DateTime.Now, null);
                }

                foreach (PropertyInfo property in properties)
                {
                    if (!property.PropertyType.IsValueType)
                    {
                        var subproperty = property.GetValue(entity, null);

                        if (subproperty is DomainEntity)
                        {
                            UpdateModifiedDate(subproperty);
                        }

                        if (subproperty is ICollection)
                        {
                            foreach (var child in subproperty as IList)
                            {
                                UpdateModifiedDate(child);
                            }
                        }
                    }
                }
            }
        }
    }
}
