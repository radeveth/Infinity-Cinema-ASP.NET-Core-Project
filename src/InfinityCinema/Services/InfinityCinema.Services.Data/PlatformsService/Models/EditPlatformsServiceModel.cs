namespace InfinityCinema.Services.Data.PlatformsService.Models
{
    using System.Collections.Generic;

    public class EditPlatformsServiceModel
    {
        public int MovieId { get; set; }

        public IEnumerable<PlatformViewModel> ExistingPlatforms { get; set; }

        public PlatformFormModel NewPlatform { get; set; }
    }
}
