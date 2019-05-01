namespace VRegisterApp.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string TextContext { get; set; }
        public int AreaLength { get; set; }
        public int AlgorithmSamples { get; set; }
        public string Pattern { get; set; }
        
    }
}