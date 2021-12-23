using System.Linq.Expressions;
using System.Runtime.Serialization;
using EntityBuilder.Abstractions;

namespace EntityBuilder;

public abstract class EntityBuilder<TEntity>
    where TEntity : IEntity
{
    private readonly TEntity _entity = CreateInstance();

    public TEntity Build()
    {
        return _entity;
    }

    public EntityBuilder<TEntity> With<TValue>(Expression<Func<TEntity, TValue>> selectorExpression, TValue value)
    {
        if (selectorExpression.Body is not MemberExpression expression)
            return this;
        
        SetProperty(expression.Member.Name, value);
        return this;
    }

    private void SetProperty<TValue>(string propName, TValue value)
    {
        var propertyInfo = typeof(TEntity).GetProperty(propName);
        if (propertyInfo == null) return;
        propertyInfo.SetValue(_entity, value);
    }

    private static TEntity CreateInstance()
        => (TEntity)FormatterServices.GetSafeUninitializedObject(typeof(TEntity));
}