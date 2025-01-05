using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace kuafor.mvc.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Price { get; set; }
        public bool IsAvailable { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
