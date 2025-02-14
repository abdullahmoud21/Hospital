using System.Diagnostics;
using ContactDoctor.Models;
using Hospital.DataAccess;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Controllers
{
    public class DoctorsController : Controller
    {
        ApplicationDbContext DbContext = new ApplicationDbContext();
        public IActionResult BookAppointment()
        {
            var doctors = DbContext.doctors;
            return View(doctors.ToList());
        }
        public IActionResult CompleteAppointment(int DoctorId)
        {
            var doctor = DbContext.doctors.FirstOrDefault(e => e.Id == DoctorId);
            
            return View(doctor);
        }
        public  IActionResult ConfirmAppointment(int doctorId , string Patient_Name , DateOnly Appointment_Date, TimeOnly Appointment_Time)
        {
            //Validation

            var appointmentdata = new Appointment
            {
                DoctorId = doctorId,
                PatientName = Patient_Name,
                AppointmentDate = Appointment_Date,
                AppointmentTime = Appointment_Time
            };
            DbContext.appointments.Add(appointmentdata);
            DbContext.SaveChanges();
            return RedirectToAction("BookAppointment");
        }
        public IActionResult ReservationsManagement()
        {
            var appointment = DbContext.appointments.Include(e => e.Doctor);
            return View(appointment.ToList());
        }
    }
}
