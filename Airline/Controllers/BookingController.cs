using Airline.Class;
using Airline.Models;
using Airline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Airline.Controllers
{
    public class BookingController : Controller
    {
        //
        // GET: /Booking/
        UIAirlinesEntities db = new UIAirlinesEntities();
      
        public ActionResult Manage(string q)
        {
            string str = q;
            //var item = (from s in db.Flights where s.Origin.StartsWith(str) select s).ToList();
            var item = db.Flights.ToList();
            if(!string.IsNullOrWhiteSpace(q))
            {
                item = item.Where(p => p.Origin.ToLower().Contains(q.ToLower())).ToList();
            }
            if (item.Count != 0)
            {
                return View(item);
            }
            else
            {
                ViewBag.empty = "empty";
                return View();
            }
        }
        
        [Authorize]
        public ActionResult Book(Guid id)
        {
            var FlightID = id;
            var loginID = Session["loginID"];
            BookFlightVM item = new BookFlightVM();
            var data = db.Flights.Where(x => x.FlightID == FlightID).First();

            item.FlightID = FlightID;
            item.Origin = data.Origin;
            item.Destination = data.Destination;
            item.DepartureTime = data.DepartureTime;
            item.ArrivalTime = data.ArrivalTime;
            return View(item);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Book(BookFlightVM model)
        {

            Booking tbl = new Booking();
            Guid loginID = (Guid)Session["loginID"];

            Guid guid = Guid.NewGuid();
            tbl.BookingID = guid;
            tbl.FlightID = model.FlightID;
            tbl.UserID = loginID;



            var tbl2 = db.Flights.Where(x => x.FlightID == model.FlightID).First();

            if (tbl2.SeatRemained != 0)
            {
                tbl2.SeatRemained = tbl2.SeatRemained - 1;
                db.SaveChanges();
                var seat = tbl2.TotalSeat - tbl2.SeatRemained;

                if (seat >= 1 && seat <= 3)
                {
                    var seatMod = seat % 3;
                    if(seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "A" + seatMod.ToString();
                }

                if (seat >= 4 && seat <= 6)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "B" + seatMod.ToString();
                }

                if (seat >= 7 && seat <= 9)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "C" + seatMod.ToString();
                }

                if (seat >= 10 && seat <= 12)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "D" + seatMod.ToString();
                }
                if (seat >= 13 && seat <= 15)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "E" + seatMod.ToString();
                }
                if (seat >= 16 && seat <= 18)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "E" + seatMod.ToString();
                }
                if (seat >= 19 && seat <= 20)
                {
                    var seatMod = seat % 3;
                    if (seatMod == 0)
                    {
                        seatMod = 3;
                    }
                    tbl.Seat = "E" + seatMod.ToString();
                }
                
            }
            db.Bookings.Add(tbl);
            db.SaveChanges();
            return RedirectToAction("ViewBooking","Booking");
        }

        [Authorize]
        public ActionResult ViewBooking()
        {
            
            Guid userID = (Guid)Session["loginID"];
            var data = (from s in db.Bookings
                        join d in db.Flights on s.FlightID equals d.FlightID
                        where s.UserID == userID
                        select new ViewBookingVM
                        {
                            Origin = d.Origin,
                            Destination = d.Destination,
                            DepartureTime = d.DepartureTime,
                            ArrivalTime = d.ArrivalTime,
                            Seat = s.Seat
                        }).ToList();

            ListViewBookingVM model = new ListViewBookingVM();
            model.List1 = data;

            if (data.Count != 0)
            {
                return View(model);
            }
            else
            {
                ViewBag.empty = "empty";
                    return View();
            }


        }
    }
}
