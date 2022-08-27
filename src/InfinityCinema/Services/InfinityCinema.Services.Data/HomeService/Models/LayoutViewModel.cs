namespace InfinityCinema.Services.Data.HomeService.Models
{
    using InfinityCinema.Common;
    using InfinityCinema.Services.Data.MoviesService.Models;

    public class LayoutViewModel
    {
        public LayoutViewModel()
        {
            //this.MoviesQueryModel = new AllMoviesQueryModel();
            this.ProjectName = GlobalConstants.SystemName;
            this.EmailAdress = GlobalConstants.EmailAdress;
            this.TelephoneNumber = GlobalConstants.TelephoneNumber;
        }

        // public AllMoviesQueryModel MoviesQueryModel { get; set; }

        public string ProjectName { get; set; }

        public string EmailAdress { get; set; }

        public string TelephoneNumber { get; set; }
    }
}
