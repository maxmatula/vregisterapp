using System;
using System.Linq;
using System.Text;
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
            var samplesNumber = new Random();
            newUser = FindPattern(registerRequest, newUser, samplesNumber.Next(200, 260));
            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            return true;
        }

        private User FindPattern(RegisterRequest registerRequest, User newUser, int AlgorithmSamples)
        {
            var user = newUser;

            var sample1 = registerRequest.VoiceSample1.Substring(37, 33000);
            var sample2 = registerRequest.VoiceSample2.Substring(37, 33000);
            var sample3 = registerRequest.VoiceSample3.Substring(37, 33000);

            var sampleLength = sample1.Length;
            int areaLength = (int)(sampleLength / AlgorithmSamples);
            user.AreaLength = areaLength;

            if (areaLength * AlgorithmSamples > 33000)
            {
                areaLength -= 15;
            }

            var pattern = new string[AlgorithmSamples + 1];
            for (int j = 1; j <= AlgorithmSamples; j++)
            {
                pattern[j] = "";
                var machCount = 0;
                var patternBegining = 0;
                string mattern = "";
                var maxPatternCount = 0;
                for (int i = ((areaLength * j) - areaLength); i < (areaLength * j); i++)
                {
                    if (machCount == 1)
                    {
                        patternBegining = i - 1;
                    }

                    if (sample1.Contains(mattern) && sample2.Contains(mattern) && sample3.Contains(mattern))
                    {
                        mattern += sample1[i];
                        machCount++;
                    }
                    else
                    {
                        if (maxPatternCount == 0)
                        {
                            maxPatternCount = machCount;
                            pattern[j] = mattern;
                        }

                        if (machCount > maxPatternCount)
                        {
                            maxPatternCount = machCount;
                            pattern[j] = mattern;
                        }

                        mattern = "";
                        machCount = 0;
                    }
                }
            }

            user.AlgorithmSamples = AlgorithmSamples;
            user.Pattern = string.Join(",",pattern);

            return user;
        }
    }
}