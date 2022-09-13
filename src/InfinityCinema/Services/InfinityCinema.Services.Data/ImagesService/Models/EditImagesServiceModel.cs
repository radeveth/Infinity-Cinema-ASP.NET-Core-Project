namespace InfinityCinema.Services.Data.ImagesService.Models
{
    using System.Collections.Generic;

    public class EditImagesServiceModel
    {
        public int MovieId { get; set; }

        public IEnumerable<ImageViewModel> ExistingImages { get; set; }

        public ImageFormModel NewImage { get; set; }
    }
}
