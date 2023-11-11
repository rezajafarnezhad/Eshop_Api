namespace Shop.Api.ViewModels.Slider;

public class CreateSliderViewModel
{

    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Title { get; set; }
}

public class EditSliderViewModel
{
    public long Id { get; set; }
    public string Link { get; set; }
    public IFormFile ImageFile { get; set; }
    public string Title { get; set; }
}