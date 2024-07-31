namespace OnlineShop.WebApi.Middlewares
{
	/// <summary>
	/// Represents error details to be returned in the response.
	/// </summary>
	public class ErrorDetails
	{
		/// <summary>
		/// Gets or sets the status code of the error.
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		public string? Message { get; set; }


		/// <summary>
		/// Converts the error details to a JSON string.
		/// </summary>
		/// <returns>The JSON representation of the error details.</returns>
		public override string ToString()
		{
			return System.Text.Json.JsonSerializer.Serialize(this);
		}
	}
}
