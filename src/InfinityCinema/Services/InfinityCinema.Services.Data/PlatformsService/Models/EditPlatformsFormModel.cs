namespace InfinityCinema.Services.Data.PlatformsService.Models
{
    using System.Collections.Generic;

    public class EditPlatformsFormModel
    {
        public EditPlatformsFormModel()
        {
            this.ExistingPlatforms = new List<PlatformViewModel>();
            this.NewPlatforms = new List<PlatformFormModel>();

        }

        public IEnumerable<PlatformViewModel> ExistingPlatforms { get; set; }

        public IEnumerable<PlatformFormModel> NewPlatforms { get; set; }
    }
}
