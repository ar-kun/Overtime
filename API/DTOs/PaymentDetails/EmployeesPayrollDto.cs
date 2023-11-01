namespace API.DTOs.PaymentDetails
{
    public class EmployeesPayrollDto
    {
        public Guid Guid { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string Nik { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Salary { get; set; }
        public Guid? ManagerGuid { get; set; }
        public string ManagerFullName { get; set; }
        public DateTime OvertimeDate { get; set; }
        public string Duration { get; set; }
        public string TypeOfDay { get; set; }
        public int TotalPay { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
