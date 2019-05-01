namespace VRegisterApp.API.DTO.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string TextContext { get; set; }
        public string VoiceSample1 { get; set; }
        public string VoiceSample2 { get; set; }
        public string VoiceSample3 { get; set; }
    }
}