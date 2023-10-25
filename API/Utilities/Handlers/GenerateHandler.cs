using API.Models;

namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        public static string GenerateNik(Employee employee)
        {
            // Default value
            string Nik = "111111";

            // Check if there is any data employee in the table
            if (employee is not null)
            {
                // Convert employee Nik to int and increment it by 1
                int generateNik = int.Parse(employee.Nik);
                Nik = (generateNik + 1).ToString();
            }
            return Nik;
        }
    }
}
