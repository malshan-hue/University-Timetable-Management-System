namespace WebApp.Models.UserModels
{
    public class LoginResponse
    {
        public string message { get; set; }

        public bool success { get; set; }

        public string token { get; set; }

        #region Navigational Properties
        public UserModel user { get; set; }

        #endregion
    }
}
