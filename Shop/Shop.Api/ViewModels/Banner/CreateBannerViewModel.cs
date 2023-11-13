using Shop.Domain.SiteEntities;

namespace Shop.Api.ViewModels.Banner;

public class CreateBannerViewModel
{
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}

public class EditBannerViewModel
{
    public long Id { get; set; }
    public string Link { get; set; }
    public IFormFile? ImageFile { get; set; }
    public BannerPosition Position { get; set; }
}