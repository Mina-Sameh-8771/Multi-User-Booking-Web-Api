using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservation_Task.Models;
using Reservation_Task.DataBaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Reservation_Task.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class ReservationApis : ControllerBase
    {

        [Route("api/ReservationApis/getAllReservations")]
        [HttpGet]
        public string getAllReservations(string reservedBy)
        {
            return ReservationDataBaseManager.getAllReservations(reservedBy);
        }

        [Route("api/ReservationApis/addNewReservation")]
        [HttpPost]
        public void addNewReservation([FromBody] Reservation newReservation)
        {
            ReservationDataBaseManager.addNewReservation(newReservation);
        }

        [Route("api/ReservationApis/updateReservation")]
        [HttpPut]
        public void updateReservation([FromBody]Reservation newReservation)
        {
            ReservationDataBaseManager.updateReservation(newReservation);
        }

        [Route("api/ReservationApis/deleteReservation")]
        [HttpDelete]
        public void deleteReservation(int reservationID)
        {
            ReservationDataBaseManager.deleteReservation(reservationID);
        }

    }
}
