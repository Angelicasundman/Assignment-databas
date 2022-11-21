using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
