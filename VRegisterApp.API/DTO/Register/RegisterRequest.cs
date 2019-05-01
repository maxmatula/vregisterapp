namespace VRegisterApp.API.DTO.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string TextContext { get; set; }
        public byte [] VoiceSample1 { get; set; }
        public byte [] VoiceSample2 { get; set; }
        public byte [] VoiceSample3 { get; set; }
    }
}