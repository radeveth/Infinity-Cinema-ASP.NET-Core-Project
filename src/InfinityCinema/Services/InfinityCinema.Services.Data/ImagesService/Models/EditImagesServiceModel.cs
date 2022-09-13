namespace InfinityCinema.Services.Data.ImagesService.Models
{
    using System.Collections.Generic;

    public class EditImagesServiceModel
    {
        public int MovieId { get; set; }

        public IEnumerable<string> ExistingImages { get; set; }

        public IEnumerable<ImageFormModel> NewImages { get; set; }
    }
}
