using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VRegisterApp.API.DAL;
using VRegisterApp.API.DTO.Register;
using VRegisterApp.API.Models;
using VRegisterApp.API.Services.Abstract;

namespace VRegisterApp.API.Services.Concentre
{
    public class VoiceService : IVoiceService
    {
        private readonly VRegisterContext _db;
        public VoiceService(VRegisterContext db)
        {
            _db = db;

        }
        public async Task<bool> RegisterUser(RegisterRequest registerRequest)
        {
            if (await _db.Users.AnyAsync(u => u.Email == registerRequest.Email))
            {
                return false;
            }

            var newUser = new User();
            newUser.Email = registerRequest.Email;
            newUser.TextContext = registerRequest.TextContext;
            newUser = FindPattern(registerRequest, newUser);

            // await _db.Users.AddAsync(newUser);
            // await _db.SaveChangesAsync();
            return true;
        }

        private User FindPattern(RegisterRequest registerRequest, User newUser)
        {
            var user = newUser;

            var sample1 = registerRequest.VoiceSample1;
            var sample2 = registerRequest.VoiceSample2;
            var sample3 = registerRequest.VoiceSample3;

            var sampleLength = sample1.Length;
            int areaLength = (int)Math.Round((sampleLength / 10) * 1.1);
            user.AreaLength = areaLength;

            var pattern = new string[11];
            for (int j = 1; j <= 10; j++)
            {
                pattern[j] = "";
                var machCount = 0;
                var patternBegining = 0;
                string mattern = "";
                for (int i = 0; i < (areaLength * j); i++)
                {
                    if (sample1[i] == sample2[i] && sample1[i] == sample3[i])
                    {
                        mattern += sample1[i];
                        patternBegining = i;
                        machCount++;
                    }
                    else
                    {
                        if (machCount > 0)
                        {
                            pattern[j] = mattern;
                        }
                        machCount = 0;
                        mattern = "";
                    }
                }
            }

            return user;
        }
    }
}