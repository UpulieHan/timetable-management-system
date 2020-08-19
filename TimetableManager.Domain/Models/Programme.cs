using System;
using System.Collections.Generic;
using System.Text;

namespace TimetableManager.Domain.Models
{
    public class Programme
    {
        public int ProgrammeId { get; set; }
        public string ProgrammeFullName { get; set; }
        public string ProgrammeShortName { get; set; }
    }
}
