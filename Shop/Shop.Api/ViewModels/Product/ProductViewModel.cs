using Common.Domain.ValueObjects;
using Newtonsoft.Json;

namespace Shop.Api.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Title { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long SecondarySubCategoryId { get; set; }
        public string Slug { get; set; }
        public SeoDataViewModel SeoData { get; set; }
        public string Specifications { get; set; }
        public Dictionary<string, string> MapSpecifications()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(Specifications);
        }
    }

    public class EditProductViewModel
    {
        public long ProductId { get; set; }
        public string Title { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public long SubCategoryId { get; set; }
        public long SecondarySubCategoryId { get; set; }
        public string Slug { get; set; }
        public SeoDataViewModel SeoData { get; set; }
        public string Specifications { get; set; }
        public Dictionary<string, string> MapSpecifications()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(Specifications);
        }
    }


    public class AddProductImageViewModel
    {
        public IFormFile ImageFile { get; set; }
        public long ProductId { get; set; }
        public int Sequence { get; set; }
    }



    public class SeoDataViewModel
    {
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }
        public bool IndexPage { get; set; }
        public string Canonical { get; set; }
        public string Schema { get; set; }

        public SeoData map()
        {
            return new SeoData(MetaKeyWords, MetaDescription, MetaTitle, IndexPage, Canonical, Schema);
        }

    }
}
