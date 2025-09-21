using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models;

public class Specialization
{
    /// <summary>
    /// Unique identifier for the medical specialization.
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Name of the medical specialization.
    /// </summary>
    public required string Name { get; set; }
}
