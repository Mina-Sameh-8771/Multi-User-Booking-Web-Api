using Reservation_Task.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Reservation_Task.DataBaseManager
{
    public static class ReservationDataBaseManager
    {
        public static void setDefaultData()
        {
            using (var db = new ReservationContext())
            {
                //db.usersTB.Add(new Models.User
                //{
                //    email = "omar@gmail.com",
                //    password = "1234"
                //});

                //db.usersTB.Add(new Models.User
                //{
                //    email = "ahmed@gmail.com",
                //    password = "1234"
                //});
                var trip = new Models.Trip
                {
                    cityName = "Sokhna",
                    creationDate = DateTime.Now,
                    imageUrl = "https://cairoairport.travel/uploads/thumb/1519134207.jpg",
                    price = 350,
                    name = "Summer Offer"
                };

                db.tripsTB.Add(trip);

                db.tripsTB.Add(new Models.Trip
                {
                    cityName = "marsa alam",
                    creationDate = DateTime.Now.AddDays(2),
                    imageUrl = "https://i.pinimg.com/originals/e1/5f/2e/e15f2e20c38822cbbe2da330964fda41.jpg",
                    price = 600,
                    name = "Hello Summer"
                });

                db.tripsTB.Add(new Models.Trip
                {
                    cityName = "sina",
                    creationDate = DateTime.Now.AddDays(5),
                    imageUrl = "https://tse3.mm.bing.net/th?id=OIP.9eVly7UQHnM055GuQ2XsAAHaEA&pid=Api&P=0&w=277&h=151",
                    price = 900,
                    name = "we are ready"
                });


                db.SaveChanges();
            }
        }

        public static bool checkUserInformation(string email, string password)
        {
            using (var db = new ReservationContext())
            {
                var user = db.usersTB.Where(x => x.email == email && x.password == password).FirstOrDefault();

                if (user == null)
                    return false;
            }

            return true;
        }

        public static string getReservationInformation(int reservationID)
        {
            using (var db = new ReservationContext())
            {
               var reservationInformation = db.reservationTB.Where(x => x.ID == reservationID).Select(x => new
                {
                    id = x.ID,
                    reservedBy = x.reservedBy,
                    customerName = x.customerName,
                    reservationDate = x.reservationDate,
                    notes = x.notes,
                    creationDate = x.creationDate,
                    cityName = x.trip.cityName,
                    price = x.trip.price,
                    imageUrl = x.trip.imageUrl,
                    name = x.trip.name
                }).FirstOrDefault();
                string jsonString = JsonSerializer.Serialize(reservationInformation);
                return jsonString;
            }
           
        }

        public static string getAllReservations(string reservedBy)
        {
            
            using (var db = new ReservationContext())
            {
                var reservationList = db.reservationTB.Where(x => x.reservedBy == reservedBy).Select(x => new {
                    id = x.ID,
                    reservedBy = x.reservedBy,
                    customerName = x.customerName,
                    reservationDate = x.reservationDate,
                    notes = x.notes,
                    creationDate = x.creationDate,
                    cityName = x.trip.cityName,
                    price = x.trip.price,
                    imageUrl = x.trip.imageUrl,
                    name = x.trip.name
                }).ToList();
                string jsonString = JsonSerializer.Serialize(reservationList);
                return jsonString;
            }
        }

        public static void addNewReservation(Reservation newReservation)
        {
            using (var db = new ReservationContext())
            {
                db.reservationTB.Add(newReservation);
                db.SaveChanges();
            }
        }

        public static void updateReservation(Reservation reservation)
        {
            using (var db = new ReservationContext())
            {
                var reservationForUpdate = db.reservationTB.Where(x => x.ID == reservation.ID).FirstOrDefault();

                reservationForUpdate.reservationDate = reservation.reservationDate;
                reservationForUpdate.notes = reservation.notes;
                reservationForUpdate.customerName = reservation.customerName;

                db.SaveChanges();
            }
        }

        public static void deleteReservation(int reservationID)
        {
            using (var db = new ReservationContext())
            {
                var reservationForDelete = db.reservationTB.Where(x => x.ID == reservationID).FirstOrDefault();

                var tripID = db.reservationTB.Where(x => x.ID == reservationID).Select(x => x.trip.ID).FirstOrDefault();

                var tripForDelete = db.tripsTB.Where(x => x.ID == tripID).FirstOrDefault();

                db.reservationTB.Remove(reservationForDelete);

                db.tripsTB.Remove(tripForDelete);

                db.SaveChanges();
            }
        }

        public static List<string> getAllTrips()
        {
            using (var db = new ReservationContext())
            {
                return db.tripsTB.Select(x => x.name).Distinct().ToList();
            }
        }

        public static Trip getTripByName (string tripName)
        {
            using (var db = new ReservationContext())
            {
                return db.tripsTB.Where(x => x.name == tripName).FirstOrDefault();
            }
        }

    }
}
