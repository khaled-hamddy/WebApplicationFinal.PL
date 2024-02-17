using System.ComponentModel.DataAnnotations;

namespace WebApplicationFinal.PL.ViewModels
{
	public class ResetPasswordViewModel
	{

		[Required(ErrorMessage = "Password is Required!")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "ConfirmPassword is Required!")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Password does not Match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }
	}
}
