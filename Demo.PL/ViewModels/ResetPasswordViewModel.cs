using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
	public class ResetPasswordViewModel
	{
		[Required(ErrorMessage = "Password is Required")]
		[MinLength(5, ErrorMessage = "Minimum Password Length is 5")]
		[DataType(DataType.Password)]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "ConfirmPassword is Required")]
		[Compare(nameof(NewPassword), ErrorMessage = "Confirm Passwword dowwnst match Password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

    }
}
