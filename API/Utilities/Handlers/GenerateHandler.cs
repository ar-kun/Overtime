namespace API.Utilities.Handlers
{
    public class GenerateHandler
    {
        public static string GenerateNIK(string? lastNik)
        {
            // If there is no previous data
            if (string.IsNullOrEmpty(lastNik))
            {
                return "111111";
            }
            else
            {
                // If there is data, add 1 to the last NIK.
                int lastNikInt = Convert.ToInt32(lastNik);
                lastNikInt++;
                return lastNikInt.ToString().PadLeft(6, '0');
            }
        }
    }
}