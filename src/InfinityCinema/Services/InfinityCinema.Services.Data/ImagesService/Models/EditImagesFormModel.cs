namespace InfinityCinema.Services.Data.ImagesService.Models
{
    using System.Collections.Generic;

    public class EditImagesFormModel
    {
        public IEnumerable<string> ExistingImages { get; set; }

        public IEnumerable<ImageFormModel> NewImages { get; set; }
    }
}
