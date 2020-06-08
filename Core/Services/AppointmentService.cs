using DAL.Data;
using DAL.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IAppointmentService
    {
        public Task<Appointment> CreateAppointmentAsync(Appointment appointment, List<ProfileOperation> ops);
        public Task<List<Appointment>> GetUserAppointments(string uId);
        public Task<List<Appointment>> GetArtisanAppointments(string artisanId);
        public Task<List<AppointmentOperations>> GetCustomerAppointmentOperations(string uId);
        public Task<List<AppointmentOperations>> GetArtisanAppointmentOperations(string artisanId);
        public Task<List<Appointment>> GetArtisanAppointmetsForADay(DateTime date, string artisanId);
        public Task<Appointment> ConfirmAppointment(string appointmentId);
        public Task<Appointment> CancelAppointment(string appointmentId);
        public Task<Appointment> DeleteAppointment(string appointmentId);
    }
    public class AppointmentService : IAppointmentService
    {
        ApplicationDbContext _context;

        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> CancelAppointment(string appointmentId)
        {
            var a = await _context.Appointments.FindAsync(appointmentId);
            a.Accepted = false;
            _context.Appointments.Update(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<Appointment> ConfirmAppointment(string appointmentId)
        {
            var a = await _context.Appointments.FindAsync(appointmentId);
            a.Accepted = true;
            _context.Appointments.Update(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment, List<ProfileOperation> ops)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            foreach (ProfileOperation o in ops)
            {
                var ao = new AppointmentOperations();
                ao.Appointment = appointment;
                ao.AppointmentId = appointment.Id;
                ao.ProfileOperation = o;
                ao.ProfileOperationId = o.Id;
                await _context.AppointmentOperations.AddAsync(ao);
                await _context.SaveChangesAsync();
            }
            return appointment;
        }

        public async Task<Appointment> DeleteAppointment(string appointmentId)
        {
            var a = await _context.Appointments.FindAsync(appointmentId);
            if (a == null)
            {
                return null;
            }
            _context.Appointments.Remove(a);
            await _context.SaveChangesAsync();
            return a;
        }

        public async Task<List<AppointmentOperations>> GetArtisanAppointmentOperations(string artisanId)
        {
            return await _context.AppointmentOperations.Where(ao => ao.Appointment.ArtisanId == artisanId).ToListAsync();
        }

        public async Task<List<Appointment>> GetArtisanAppointments(string artisanId)
        {
            return await _context.Appointments.Where(a => a.ArtisanId == artisanId).ToListAsync();
        }

        public async Task<List<Appointment>> GetArtisanAppointmetsForADay(DateTime date, string artisanId)
        {
            return await _context.Appointments.Where(a => a.ArtisanId == artisanId && a.Date.Date == date.Date).ToListAsync();
        }

        public async Task<List<AppointmentOperations>> GetCustomerAppointmentOperations(string uId)
        {
            return await _context.AppointmentOperations.Where(ao => ao.Appointment.Customer.Id == uId).ToListAsync();
        }

        public async Task<List<Appointment>> GetUserAppointments(string uId)
        {
            return await _context.Appointments.Where(a => a.Customer.Id == uId).ToListAsync();
        }
    }
}
