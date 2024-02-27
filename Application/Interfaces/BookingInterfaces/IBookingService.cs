using Application.Models.REST.Request.Booking;
using Application.Models.REST.Response.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.BookingInterfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> Create(BookingRequest bookingRequest);

    }
}
