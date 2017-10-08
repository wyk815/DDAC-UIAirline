using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Airline.Class
{
    public class ViewBookingVM
    {
        public System.Guid FlightID { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public Nullable<System.DateTime> DepartureTime { get; set; }
        public Nullable<System.DateTime> ArrivalTime { get; set; }
        public Nullable<int> TotalSeat { get; set; }

        public System.Guid BookingID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public string Seat { get; set; }
    }
}