using WebApplication2.Models;

namespace WebApplication2.ViewModels
{
   public class CustomerViewModel
    {
        public string PageTitle { get; set; } = string.Empty;
        public string WelcomeMessage { get; set; }=string.Empty;
        public List<Customers>? Customers { get; set; }

        public DateTime CurrentDate { get; set; } = DateTime.Now;
    } 
}
