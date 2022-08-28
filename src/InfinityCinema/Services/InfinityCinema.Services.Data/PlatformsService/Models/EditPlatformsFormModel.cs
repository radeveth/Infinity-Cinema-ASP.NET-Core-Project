namespace InfinityCinema.Services.Data.PlatformsService.Models
{
    using System.Collections.Generic;

    public class EditPlatformsFormModel
    {
        public IEnumerable<PlatformViewModel> ExistingPlatforms { get; set; }

        public IEnumerable<PlatformFormModel> NewPlatforms { get; set; }
    }
}
