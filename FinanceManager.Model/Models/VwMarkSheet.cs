using System;
using System.Collections.Generic;

namespace FinanceManager.Model.Models
{
    public partial class VwMarkSheet
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public string AttendanceMarks { get; set; }
        public string Comment { get; set; }
        public int AttendanceId { get; set; }
    }
}
