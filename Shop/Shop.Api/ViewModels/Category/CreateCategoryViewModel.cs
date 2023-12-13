using Common.Domain.ValueObjects;

namespace Shop.Api.ViewModels.Category;

public record CreateCategoryViewModel(string Title, string Slug, SeoData SeoData);