using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SendOTPService : ISendOTPService
    {
        private ISendOTPRepository _sendOTPRepository;
        public SendOTPService(ISendOTPRepository sendOTPRepository) {

            _sendOTPRepository=sendOTPRepository;
        }

        
        public string GenerateOTP()
        {
            return _sendOTPRepository.GenerateOTP();
        }

        //public Task<bool> SendOTPEmailAsync(string email, string otp)
        //{
        //    return _sendOTPRepository.SendOTPEmailAsync();
        //}
    }
}
