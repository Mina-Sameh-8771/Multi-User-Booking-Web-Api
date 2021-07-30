using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Reservation_Task.DataBaseManager;
using Reservation_Task.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Reservation_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public const string localUrl = "https://localhost:44357";


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login(bool valid = true)
        {
            TempData["IsCorrect"] = valid;
            return View();
        }

        public async Task<ActionResult> HomePage()
        {
            List<ReservationInformation> reservationList = new List<ReservationInformation>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(localUrl + "/api/ReservationApis/getAllReservations?reservedBy=" + TempData["email"].ToString()))
                {
                    TempData.Keep("email");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<ReservationInformation>>(apiResponse);
                }
            }
            return View(reservationList);
    
        }

        public ActionResult AddReservation()
        {
            var tripsList = ReservationDataBaseManager.getAllTrips();
            return View(tripsList);
        }

        public ActionResult updateReservation(int reservationID)
        {
            var reservationInformationJsonString = ReservationDataBaseManager.getReservationInformation(reservationID);
            var reservationInformationObj = JsonConvert.DeserializeObject<ReservationInformation>(reservationInformationJsonString);
            return View(reservationInformationObj);
        }

        public async Task<ActionResult> DeleteReservation(int reservationID)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync(localUrl + "/api/ReservationApis/deleteReservation?reservationID=" + reservationID.ToString()))
                {
                    return RedirectToAction("HomePage", "Home");
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateReservation(IFormCollection collection)
        {
            CultureInfo provider = new CultureInfo("en-US");
            var newReservation = new Reservation();
            newReservation.creationDate = DateTime.Now;
            newReservation.customerName = collection["customerName"];
            newReservation.notes = collection["notes"];
            newReservation.reservationDate = Convert.ToDateTime(collection["reservationDate"]);
            newReservation.reservedBy = collection["reservedBy"];
            newReservation.ID = int.Parse(collection["id"]);
            newReservation.trip = ReservationDataBaseManager.getTripByName(collection["tripName"]);

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newReservation);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync(localUrl + "/api/ReservationApis/updateReservation", data))
                {
                    return RedirectToAction("HomePage", "Home");
                }
            }
        }

        [HttpPost]
        public IActionResult CheckUserInformation(string email, string password)
        {
            if (ReservationDataBaseManager.checkUserInformation(email, password))
            {
                TempData["email"] = email;
                TempData.Keep("email");
                return RedirectToAction("HomePage", "Home");
            }
            else
            {
                return RedirectToAction("Login", "Home" , new { valid = false });
            }
            
        }

        [HttpPost]
        public async Task<ActionResult> addNewReservation(IFormCollection collection)
        {

            CultureInfo provider = new CultureInfo("en-US");
            var newReservation = new Reservation();
            newReservation.creationDate = DateTime.Now;
            newReservation.customerName = collection["customerName"];
            newReservation.notes = collection["notes"];
            var date = Convert.ToDateTime(collection["reservationDate"]);
            DateTime dateTime15;
            bool isSuccess5 = DateTime.TryParseExact(collection["reservationDate"], "YYYY-MM-DD", provider, DateTimeStyles.None, out dateTime15);
            newReservation.reservationDate = date;
            newReservation.reservedBy = TempData["email"].ToString();
            TempData.Keep("email");
            newReservation.trip = ReservationDataBaseManager.getTripByName(collection["tripName"]);

            using (var httpClient = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(newReservation);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync(localUrl + "/api/ReservationApis/addNewReservation", data ))
                {
                    return RedirectToAction("HomePage", "Home");
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
