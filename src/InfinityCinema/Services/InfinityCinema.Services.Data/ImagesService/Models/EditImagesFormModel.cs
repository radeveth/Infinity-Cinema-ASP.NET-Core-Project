namespace InfinityCinema.Services.Data.ImagesService.Models
{
    using System.Collections.Generic;

    public class EditImagesFormModel
    {
        public EditImagesFormModel()
        {
            this.ExistingImages = new List<string>();
            this.NewImages = new List<ImageFormModel>();
        }

        public IEnumerable<string> ExistingImages { get; set; }

        public IEnumerable<ImageFormModel> NewImages { get; set; }
    }
}
