/*
using StackExchange.Redis;

namespace Task_Manager.ApiService.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly IDatabase _redisDb;

        public TokenBlacklistService(IDatabase redisDb)
        {
            _redisDb = redisDb;
        }

        public async Task BlacklistTokenAsync(string token, DateTime expiration)
        {
            var expiresIn = expiration - DateTime.UtcNow;
            if (expiresIn > TimeSpan.Zero)
            {
                await _redisDb.StringSetAsync(GetRedisKey(token), "blacklisted", expiresIn);
            }
        }

        public async Task<bool> IsTokenBlacklistedAsync(string token)
        {
            return await _redisDb.KeyExistsAsync(GetRedisKey(token));
        }

        private static string GetRedisKey(string token) => $"blacklist:{token}";
    }
}
*/