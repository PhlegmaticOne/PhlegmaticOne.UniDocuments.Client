using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace UniDocuments.App.Client.Web.Infrastructure.Extensions;

public static class ExpressionExtensions
{
    private static readonly ModelExpressionProvider ModelExpressionProvider = new ModelExpressionProvider(new EmptyModelMetadataProvider());

    public static string GetExpressionText<TEntity, TProperty>(this Expression<Func<TEntity, TProperty>> expression)
    {
        return ModelExpressionProvider.GetExpressionText(expression);
    }
}