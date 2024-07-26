using System.Collections.Generic;

namespace EventReservation.PL.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string UserSSN { get; set; }

        public IEnumerable<string> Roles { get; set; }

    }
}
