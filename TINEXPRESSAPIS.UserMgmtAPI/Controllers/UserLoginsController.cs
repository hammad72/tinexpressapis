using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private readonly IUserLoginsService _userLoginsService;
        public readonly IJwtTokenService _jwtTokenService;
        public readonly IUserProfileService _userProfileService;
        public readonly ICustomerUserProfileService customerUserProfileService;
        public UserLoginsController(IUserLoginsService userLoginsService, IJwtTokenService jwtTokenService, IUserProfileService userProfileService, ICustomerUserProfileService customerUserProfileService)
        {
            _userLoginsService = userLoginsService;
            _jwtTokenService = jwtTokenService;
            _userProfileService = userProfileService;
            this.customerUserProfileService = customerUserProfileService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userLoginsService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _userLoginsService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserLoginsDto dto)
        {
            int ulid = await _userLoginsService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserLoginsDto dto)
        {
            await _userLoginsService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userLoginsService.DeleteAsync(id);
            return Ok();
        }

        [HttpPost("login")] //admin
        public async Task<IActionResult> Login([FromBody] Application.DTOs.LoginRequest request)
        {

            var user = await _userLoginsService.GetByEmailandPassword(request.Email, request.Password, "admin");
            if (user != null)
            {
                var accessToken = _jwtTokenService.GenerateToken(user.id.ToString(), user.username, new List<string> { "User" });
                var refreshToken = _jwtTokenService.GenerateRefreshToken();
                var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                // saving in loginmodel and logindetail
                var loginDetailObj = new CreateLoginDetailDto(
                        Id: 0,
                        UserId: user.id,
                        UserName: user.username,
                        Name: user.username,
                        Password: user.password,
                        UType: user.user_type,
                        UStatus: 1,
                        LastActiveTime: DateTime.UtcNow,
                        Token: accessToken
                    );
                var loginModelobj = new CreateAndUpdateLoginModelDto(
                         UserId: user.id,
                         UserName: user.username,
                         Password: user.password,
                         RefreshToken: refreshToken,
                         RefreshTokenExpiryTime: refreshTokenExpiry
                    );
                //var cup=await customerUserProfileService.GetByIdAsync( user.id );
                await _userLoginsService.AddLoginDetailAsync(loginDetailObj);
                await _userLoginsService.AddAndUpdateLoginModel(loginModelobj);

                return Ok(new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    loginID = user.id,
                    //customerID= cup.customer_id
                });
                //var token = _jwtTokenService.GenerateToken(user.id.ToString(), user.username, new List<string> { "User" });
                //return Ok(new { Token = token });

            }
            return Unauthorized(new { Message = "Invalid email or password." });
        }
        //public class LoginRequest
        //{
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //}



        [HttpPost("social-login")] //admin
        public async Task<IActionResult> SocialLogin([FromBody] SocialLoginRequest request)
        {
            var socialData = new SocialInfoDTO(email: "", name: "");

            if (request.Provider == "google")
            {
                //var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
                //email = payload.Email;
                var model = await _userLoginsService.socialGoogle(request.Token);
                //socialData.name=model.name;
                socialData = model;
            }
            else if (request.Provider == "facebook")
            {
                //using var client = new HttpClient();
                //var fbResponse = await client.GetStringAsync($"https://graph.facebook.com/me?fields=email&access_token={request.Token}");
                //var fbData = JsonDocument.Parse(fbResponse);
                //email = fbData.RootElement.GetProperty("email").GetString();
                var model = await _userLoginsService.socialFb(request.Token);
                socialData = model;
            }

            if (socialData.name == "" || socialData == null)
                return BadRequest("Invalid token");
            var checkUser = await _userLoginsService.GetByEmaila(socialData.email, "admin");
            if (checkUser == null)
            {
                try
                {
                    try
                    {
                        int res = await _userLoginsService.socialSignUp(socialData.email, socialData.name, "admin");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message.ToString());
                        throw;
                    }
                    //       CreateUserProfileDto_Ex dto = new CreateUserProfileDto_Ex(first_name: socialData.name, last_name: "", emp_num: "", dob: "", enrollment_date: System.DateTime.Now, joining_date: System.DateTime.Now, address: "",
                    //postal_code: "", city_id: 0, phone_number: "", other: "",
                    //email: socialData.email, created_by: 1, user_role_id: 1, status: 1);
                    //       //var up =await  _userProfileService.AddAsync( usProf);
                    //       int ulid = await _userProfileService.AddAsync(dto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    throw;
                }

                //var ul = new CreateUserLoginsDto(
                //    username: socialData.email,
                //    password: "",
                //    user_type: 1,
                //    first_login: true,
                //     other: null,
                //    created_by: 1
                //    );

                //int logsa = await _userLoginsService.AddAsync(ul);
            }

            var user = await _userLoginsService.GetByEmailandPassword(socialData.email, "", "admin");

            var accessToken = _jwtTokenService.GenerateToken(user.id.ToString(), user.username, new List<string> { "User" });
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            // saving in loginmodel and logindetail
            var loginDetailObj = new CreateLoginDetailDto(
                    Id: 0,
                    UserId: user.id,
                    UserName: user.username,
                    Name: user.username,
                    Password: user.password,
                    UType: user.user_type,
                    UStatus: 1,
                    LastActiveTime: DateTime.UtcNow,
                    Token: accessToken
                );
            var loginModelobj = new CreateAndUpdateLoginModelDto(
                     UserId: user.id,
                     UserName: user.username,
                     Password: user.password,
                     RefreshToken: refreshToken,
                     RefreshTokenExpiryTime: refreshTokenExpiry
                );

            await _userLoginsService.AddLoginDetailAsync(loginDetailObj);
            await _userLoginsService.AddAndUpdateLoginModel(loginModelobj);

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                userID = user.id
            });

        }



        [HttpPost("login-customer")]
        public async Task<IActionResult> LoginCustomer([FromBody] Application.DTOs.LoginRequest request)
        {

            var user = await _userLoginsService.GetByEmailandPassword(request.Email, request.Password, "customer");
            if (user != null)
            {
                var accessToken = _jwtTokenService.GenerateToken(user.id.ToString(), user.username, new List<string> { "User" });
                var refreshToken = _jwtTokenService.GenerateRefreshToken();
                var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
                // saving in loginmodel and logindetail
                var loginDetailObj = new CreateLoginDetailDto(
                        Id: 0,
                        UserId: user.id,
                        UserName: user.username,
                        Name: user.username,
                        Password: user.password,
                        UType: user.user_type,
                        UStatus: 1,
                        LastActiveTime: DateTime.UtcNow,
                        Token: accessToken
                    );
                var loginModelobj = new CreateAndUpdateLoginModelDto(
                         UserId: user.id,
                         UserName: user.username,
                         Password: user.password,
                         RefreshToken: refreshToken,
                         RefreshTokenExpiryTime: refreshTokenExpiry
                    );

                var cup = await customerUserProfileService.GetByIdAsync(user.id);
                await _userLoginsService.AddLoginDetailAsync(loginDetailObj);
                await _userLoginsService.AddAndUpdateLoginModel(loginModelobj);

                return Ok(new
                {
                    Token = accessToken,
                    RefreshToken = refreshToken,
                    loginID = user.id,
                    customerID = cup.customer_id
                });
                //var token = _jwtTokenService.GenerateToken(user.id.ToString(), user.username, new List<string> { "User" });
                //return Ok(new { Token = token });

            }
            return Unauthorized(new { Message = "Invalid email or password." });
        }
        //public class LoginRequest
        //{
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //}


        [HttpPost("social-login-customer")] 
        public async Task<IActionResult> SocialLoginCustomer([FromBody] SocialLoginRequest request)
        {
            var socialData = new SocialInfoDTO(email: "", name: "");

            if (request.Provider == "google")
            {
                //var payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
                //email = payload.Email;
                var model = await _userLoginsService.socialGoogle(request.Token);
                //socialData.name=model.name;
                socialData = model;
            }
            else if (request.Provider == "facebook")
            {
                //using var client = new HttpClient();
                //var fbResponse = await client.GetStringAsync($"https://graph.facebook.com/me?fields=email&access_token={request.Token}");
                //var fbData = JsonDocument.Parse(fbResponse);
                //email = fbData.RootElement.GetProperty("email").GetString();
                var model = await _userLoginsService.socialFb(request.Token);
                socialData = model;
            }

            if (socialData.name == "" || socialData == null)
                return BadRequest("Invalid token");
            var checkUser = await _userLoginsService.GetByEmaila(socialData.email, "customer");
            if (checkUser == null)
            {
                try
                {
                    int res = await _userLoginsService.socialSignUp(socialData.email, socialData.name, "customer");
                    //try
                    //{

                    //}
                    //catch (Exception ex)
                    //{
                    //    Console.WriteLine(ex.Message.ToString());
                    //    throw;
                    //}
                    //       CreateUserProfileDto_Ex dto = new CreateUserProfileDto_Ex(first_name: socialData.name, last_name: "", emp_num: "", dob: "", enrollment_date: System.DateTime.Now, joining_date: System.DateTime.Now, address: "",
                    //postal_code: "", city_id: 0, phone_number: "", other: "",
                    //email: socialData.email, created_by: 1, user_role_id: 1, status: 1);
                    //       //var up =await  _userProfileService.AddAsync( usProf);
                    //       int ulid = await _userProfileService.AddAsync(dto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    throw;
                }

                //var ul = new CreateUserLoginsDto(
                //    username: socialData.email,
                //    password: "",
                //    user_type: 1,
                //    first_login: true,
                //     other: null,
                //    created_by: 1
                //    );

                //int logsa = await _userLoginsService.AddAsync(ul);
            }

            var user = await _userLoginsService.GetByEmailandPassword(socialData.email, "", "customer");

            var accessToken = _jwtTokenService.GenerateToken(user.id.ToString(), user.username, new List<string> { "User" });
            var refreshToken = _jwtTokenService.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            // saving in loginmodel and logindetail
            var loginDetailObj = new CreateLoginDetailDto(
                    Id: 0,
                    UserId: user.id,
                    UserName: user.username,
                    Name: user.username,
                    Password: user.password,
                    UType: user.user_type,
                    UStatus: 1,
                    LastActiveTime: DateTime.UtcNow,
                    Token: accessToken
                );
            var loginModelobj = new CreateAndUpdateLoginModelDto(
                     UserId: user.id,
                     UserName: user.username,
                     Password: user.password,
                     RefreshToken: refreshToken,
                     RefreshTokenExpiryTime: refreshTokenExpiry
                );

            var cup = await customerUserProfileService.GetByIdAsync(user.id);
            await _userLoginsService.AddLoginDetailAsync(loginDetailObj);
            await _userLoginsService.AddAndUpdateLoginModel(loginModelobj);

            return Ok(new
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                loginID = user.id,
                customerID = cup.customer_id
            });

        }



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] Application.DTOs.RefreshRequest request)
        {
            var user = await _userLoginsService.GetLoginModel(request.RefreshToken);
            var userInfo = await _userLoginsService.GetByIdAsync(user.UserId);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized(new { Message = "Invalid or expired refresh token." });
            }

            var newAccessToken = _jwtTokenService.GenerateToken(user.UserId.ToString(), user.UserName, new List<string> { "User" });
            var newRefreshToken = _jwtTokenService.GenerateRefreshToken();
            var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            // saving in loginmodel and logindetail
            var loginDetailObj = new CreateLoginDetailDto(
                    Id: 0,
                    UserId: userInfo.id,
                    UserName: userInfo.username,
                    Name: userInfo.username,
                    Password: userInfo.password,
                    UType: userInfo.user_type,
                    UStatus: 1,
                    LastActiveTime: DateTime.UtcNow,
                    Token: newAccessToken
                );
            var loginModelobj = new CreateAndUpdateLoginModelDto(
                     UserId: userInfo.id,
                     UserName: userInfo.username,
                     Password: userInfo.password,
                     RefreshToken: newRefreshToken,
                     RefreshTokenExpiryTime: refreshTokenExpiry
                );

            await _userLoginsService.AddLoginDetailAsync(loginDetailObj);
            await _userLoginsService.AddAndUpdateLoginModel(loginModelobj);

            return Ok(new
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                userID = userInfo.id
            });
        }
    }
}
