using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kuafor.mvc.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateTime AppointmentTime { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
