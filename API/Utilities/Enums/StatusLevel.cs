using System.ComponentModel.DataAnnotations;

namespace API.Utilities.Enums
{
    public enum StatusLevel
    {
        Requested,
        Approved,
        Rejected,
        Canceled,
        [Display(Name = "On Going")] OnGoing,
        [Display(Name = "Waiting for Payment")] WaitingForPayment,
        Finished
    }
}