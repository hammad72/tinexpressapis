using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class OTPControlService:IOTPControlService
    {
        //private readonly IUserRepository _userRepository;
        private readonly ISendOTPService _sendOTPService;
        private readonly IUserLoginsService _userLoginsService;
        private readonly ISendOTPRepository _sendOTPRepository;
        private readonly IUserLoginsRepository _userLoginsRepository;
        private readonly IMapper _mapper;
        public OTPControlService(IMapper mapper, ISendOTPService otpService, IUserLoginsService userLoginsService, ISendOTPRepository sendOTPRepository, IUserLoginsRepository userLoginsRepository)
        {
            //_userRepository = userRepository;
            _sendOTPService = otpService;
            _userLoginsService = userLoginsService;
            _sendOTPRepository = sendOTPRepository;
            _userLoginsRepository = userLoginsRepository;
            _mapper=mapper;
        }
        public async Task<EmailOTPResponseDto> SendOTPAsync(string email)
        {
            var user = await _userLoginsService.GetByEmaila(email);
            if (user == null)
            {
                return new EmailOTPResponseDto
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var otp = _sendOTPService.GenerateOTP();
            var emailSent = await _sendOTPRepository.SendOTPEmailAsync(user.id,email, otp);

            return new EmailOTPResponseDto
            {
                uid=user.id,
                email=email,
                Success = emailSent,
                Message = emailSent ? "OTP sent successfully" : "Failed to send OTP",
                OTP = emailSent ? otp : null // Remove in production
            };
        }
        public async Task<OtpChangePassDTO> verifyOTpControl(OtpChangePassRequestDTO param)
        {
            try
            {
                var user = await _userLoginsRepository.GetAsync(param.uid);
                if (user == null)
                {
                    return new OtpChangePassDTO
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }
                bool succes = false;
                if (user.username == param.email)
                {
                    var verifyOTP = await _sendOTPRepository.VerifyOTPAsync(param.uid, param.otp);
                    if (verifyOTP == true)
                    {
                        user.password = param.newpass;
                        //var ul = _mapper.Map<UpdateUserLoginsDto>(user);
                        var ul = new UpdateUserLoginsDto(

                            id: user.id,
                            username: user.username,
                            password: user.password,
                            user_type: user.user_type,
                            first_login: user.first_login,
                            other: user.other,
                            updated_by: user.updated_by


                            );
                        await _userLoginsService.UpdateAsync(ul);
                        succes = true;
                    }
                }
                if (succes == true)
                {

                    return new OtpChangePassDTO
                    {
                        Success = true,
                        Message = "Succesfully Password Changed"

                    };
                }
                else
                {
                    return new OtpChangePassDTO
                    {
                        Success = false,
                        Message = "Uable to change password"

                    };
                }
            }
            catch (Exception ex)
            {

                return new OtpChangePassDTO
                {
                    Success = false,
                    Message = ex.Message

                };
            }
     
       

        }
    }
}
