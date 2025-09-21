using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Models;

/// <summary>
/// Represents the gender of a patient.
/// </summary>
public enum Gender
{
    /// <summary>
    /// Male gender.
    /// </summary>
    Male,
    
    /// <summary>
    /// Female gender.
    /// </summary>
    Female
}

/// <summary>
/// Represents the blood type of a patient.
/// </summary>
public enum BloodGroup
{
    /// <summary>
    /// Blood group O.
    /// </summary>
    O,
    
    /// <summary>
    /// Blood group A.
    /// </summary>
    A,
    
    /// <summary>
    /// Blood group B.
    /// </summary>
    B,
    
    /// <summary>
    /// Blood group AB.
    /// </summary>
    AB
}

/// <summary>
/// Represents the Rh factor of a patient.
/// </summary>
public enum RhFactor
{
    /// <summary>
    /// Positive Rh factor.
    /// </summary>
    Positive,

    /// <summary>
    /// Negative Rh factor.
    /// </summary>
    Negative
}
