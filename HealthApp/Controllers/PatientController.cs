using HealthApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthApp.Controllers;

public class PatientController : Controller
{
    public IActionResult AddPatient()
    {
        var patients = new List<Patient>();
        var patient1 = new Patient();
        patient1.PatientId = 1;
        patient1.Name = "Guslem";
        patients.Add(patient1);
        return View(patients);
    }
}