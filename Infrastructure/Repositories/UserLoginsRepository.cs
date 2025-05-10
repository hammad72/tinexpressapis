using Domain.Entities;
using Domain.Interfaces;
using Google.Apis.Auth;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Infrastructure.Repositories
{
    public class UserLoginsRepository : IUserLoginsRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public UserLoginsRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(userlogins ul)
        {
            ul.status = 1;
            await _userMgmtDbContext.AddAsync(ul);
            await _userMgmtDbContext.SaveChangesAsync();
            int ulid = ul.id;
            return ulid;
        }

        public async Task DeleteAsync(int id)
        {
            var ul = await _userMgmtDbContext.userlogins.FindAsync(id);
            if (ul != null)
            {
                _userMgmtDbContext.userlogins.Remove(ul);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<userlogins>> GetAllAsync() => await _userMgmtDbContext.userlogins.ToListAsync();
        public async Task<userlogins> GetAsync(int id) => await _userMgmtDbContext.userlogins.FindAsync(id);

        //public async Task UpdateAsync(userlogins ul)
        //{
        //    _userMgmtDbContext.userlogins.Update(ul);
        //    await _userMgmtDbContext.SaveChangesAsync();
        //}
        public async Task UpdateAsync(userlogins ul)
        {
            var user = await _userMgmtDbContext.userlogins.Where(x => x.id == ul.id).FirstOrDefaultAsync();
            //user.id = ul.id;
            user.username = ul.username;
            user.password = ul.password;
            user.user_type = ul.user_type;
            user.first_login = ul.first_login;
            user.other = ul.other;
            user.updated_by = ul.updated_by;
            _userMgmtDbContext.userlogins.Update(user);
            await _userMgmtDbContext.SaveChangesAsync();
        }
        public async Task<userlogins> GetByEmailandPassword(string email, string password,string uType)
        {
            //    return await _userMgmtDbContext.userlogins
            //        .FirstOrDefaultAsync(x => x.username == email && x.password == password);
            try
            {
                if (uType == "admin")
                {
                    var Uprofile=await _userMgmtDbContext.userprofile.FirstOrDefaultAsync(x => x.email == email);
                    if(Uprofile != null)
                    {
                        var user = await _userMgmtDbContext.userlogins
          .                         FirstOrDefaultAsync(x => x.username == email && x.password == password);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (uType=="customer")
                {
                    var cprofile = await _userMgmtDbContext.customerprofile.FirstOrDefaultAsync(x => x.email == email);
                    if (cprofile != null)
                    {
                        var user = await _userMgmtDbContext.userlogins
                                        .FirstOrDefaultAsync(x => x.username == email && x.password == password);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (uType == "courier")
                {
                    var cprofile = await _userMgmtDbContext.couriers.FirstOrDefaultAsync(x => x.email == email);
                    if (cprofile != null)
                    {
                        var user = await _userMgmtDbContext.userlogins
                                        .FirstOrDefaultAsync(x => x.username == email && x.password == password);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return null;
            }

        }
        public async Task<userlogins> GetByEmail(string email,string uType)
        {
            //    return await _userMgmtDbContext.userlogins
            //        .FirstOrDefaultAsync(x => x.username == email && x.password == password);
            try
            {
                if (uType == "admin")
                {
                    var Uprofile = await _userMgmtDbContext.userprofile.FirstOrDefaultAsync(x => x.email == email);
               
                    if (Uprofile != null)
                    {
                        var user = await _userMgmtDbContext.userlogins
                                      .FirstOrDefaultAsync(x => x.username == email);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (uType == "customer")
                {
                    var cprofile = await _userMgmtDbContext.customerprofile.FirstOrDefaultAsync(x => x.email == email);
                    if (cprofile != null)
                    {
                        var user = await _userMgmtDbContext.userlogins
                                 .FirstOrDefaultAsync(x => x.username == email);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (uType == "courier")
                {
                    var cprofile = await _userMgmtDbContext.couriers.FirstOrDefaultAsync(x => x.email == email);
                    if (cprofile != null)
                    {
                        var user = await _userMgmtDbContext.userlogins
                                             .FirstOrDefaultAsync(x => x.username == email);
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return null;
            }

        }
        public async Task AddLoginDetailAsync(logindetail ld)
        {
            await _userMgmtDbContext.AddAsync(ld);
            await _userMgmtDbContext.SaveChangesAsync();
        }
        public async Task AddUpdateLoginModel(loginmodel lm)
        {
            var userid = await _userMgmtDbContext.loginmodel.Where(x => x.UserId == lm.UserId).FirstOrDefaultAsync();
            try
            {
                if (userid != null)
                {
                    userid.UserName = lm.UserName;
                    userid.Password = lm.Password;
                    userid.RefreshToken = lm.RefreshToken;
                    userid.RefreshTokenExpiryTime = lm.RefreshTokenExpiryTime;
                    //_userMgmtDbContext.Update(lm);
                    await _userMgmtDbContext.SaveChangesAsync();
                }
                else
                {
                    await _userMgmtDbContext.AddAsync(lm);
                    await _userMgmtDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<loginmodel> GetLoginModel(string refreshToken)
        {
            var user = await _userMgmtDbContext.loginmodel
               .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            return user;
        }

        public async Task<socialinfo> socialGoogle(string requestToken)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(requestToken);

            var obj = new socialinfo();
            obj.email = payload.Email;
            obj.name = payload.Name;
            return obj;
        }

        public async Task<socialinfo> socialFb(string requestToken)
        {
            using var client = new HttpClient();
            var fbResponse = await client.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={requestToken}");
            var fbData = JsonDocument.Parse(fbResponse);

            var obj = new socialinfo();
            obj.email = fbData.RootElement.GetProperty("email").GetString();
            obj.name = fbData.RootElement.GetProperty("name").GetString();
            return obj;
        }

        public async Task<bool> checkUserExist(string email)
        {
            int userid = await _userMgmtDbContext.userlogins.Where(x=>x.username == email).Select(x=>x.id).FirstOrDefaultAsync();
            if (userid >= 1)
                return true;
            else 
                return false;
        }
    }
}
