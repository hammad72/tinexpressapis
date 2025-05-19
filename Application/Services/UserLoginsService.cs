using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using static System.Formats.Asn1.AsnWriter;

namespace Application.Services
{
    public class UserLoginsService : IUserLoginsService
    {
        private readonly IUserLoginsRepository _ulRepository;
        private readonly ICustomerProfileRepository _cpRepository;
        private readonly ICustomerUserProfileRepository _cupRepository;
        private readonly IUserProfileRepository _upRepository;
        private readonly IMapper _mapper;
        public UserLoginsService(IUserLoginsRepository ulRepository, IMapper mapper, ICustomerProfileRepository cpRepository, ICustomerUserProfileRepository cupRepository, IUserProfileRepository upRepository)
        {
            _mapper = mapper;
            _ulRepository = ulRepository;
            _cpRepository = cpRepository;
            _cupRepository = cupRepository;
            _upRepository = upRepository;
        }
        public async Task<int> AddAsync(CreateUserLoginsDto cUserLoginsDto)
        {
            int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));
            return ulid;
        }

        public async Task DeleteAsync(int id)
        {
            await _ulRepository.DeleteAsync(id);
        }

        public async Task<List<UserLoginsDto>> GetAllAsync()
        {
            return _mapper.Map<List<UserLoginsDto>>(await _ulRepository.GetAllAsync());
        }

        public async Task<UserLoginsDto> GetByIdAsync(int id)
        {
            return _mapper.Map<UserLoginsDto>(await _ulRepository.GetAsync(id));
        }

        public async Task UpdateAsync(UpdateUserLoginsDto uUserLoginsDto)
        {
            await _ulRepository.UpdateAsync(_mapper.Map<userlogins>(uUserLoginsDto));
        }

        public async Task<UserLoginsDto> GetByEmailandPassword(string email, string password, string uType)
        {
            var user = await _ulRepository.GetByEmailandPassword(email, password, uType);
            if (user == null)
            {
                Console.WriteLine("User not found for email: " + email);
                return null;
            }
            var mappedUser = _mapper.Map<UserLoginsDto>(user);
            if (mappedUser == null)
            {
                Console.WriteLine("Mapping failed for user.");
            }

            return mappedUser;
            //return user != null ? _mapper.Map<UserLoginsDto>(user) : null;
        }

        public async Task AddLoginDetailAsync(CreateLoginDetailDto ld)
        {
            //await _ulRepository.AddAsync
            await _ulRepository.AddLoginDetailAsync(_mapper.Map<logindetail>(ld));
        }
        public async Task AddAndUpdateLoginModel(CreateAndUpdateLoginModelDto ld)
        {
            //await _ulRepository.AddAsync
            await _ulRepository.AddUpdateLoginModel(_mapper.Map<loginmodel>(ld));
        }

        public async Task<LoginModelDto> GetLoginModel(string refreshToken)
        {
            var user = await _ulRepository.GetLoginModel(refreshToken);
            return user != null ? _mapper.Map<LoginModelDto>(user) : null;
        }

        public async Task<SocialInfoDTO> socialGoogle(string requestToken)
        {
            var user = await _ulRepository.socialGoogle(requestToken);
            return user != null ? _mapper.Map<SocialInfoDTO>(user) : null;
        }

        public async Task<SocialInfoDTO> socialFb(string requestToken)
        {
            var user = await _ulRepository.socialFb(requestToken);
            return user != null ? _mapper.Map<SocialInfoDTO>(user) : null;
        }

        public async Task<UserLoginsDto> GetByEmaila(string email, string uType)
        {
            var user = await _ulRepository.GetByEmail(email,  uType);
            if (user == null)
            {
                Console.WriteLine("User not found for email: " + email);
                return null;
            }
            var mappedUser = _mapper.Map<UserLoginsDto>(user);
            if (mappedUser == null)
            {
                Console.WriteLine("Mapping failed for user.");
            }

            return mappedUser;
        }

        public async Task<int> socialSignUp(string email, string name,string uType)
        {
            if (uType == "admin")
            {
                CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(email, "", 2, true, "", 1);
                int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

                CreateUserProfileDto cc = new CreateUserProfileDto(ulid,           // required
                                                                        name,           // required
                                                                        null,           // username
                                                                        null,           // password
                                                                        email,      // email (keep this)
                                                                        null,           // dob
                                                                        null,           // enrollment_date
                                                                        null,           // joining_date
                                                                        null,           // address
                                                                        null,           // postal_code
                                                                        null,           // city_id
                                                                        null,           // phone_number
                                                                        null,           // other
                                                                        1,
                                                                        1
                                                                        );          // created_by
    
                int upid = await _upRepository.AddAsync(_mapper.Map<userprofile>(cc));
                return upid;
            }
            else if (uType == "customer")
            {
                CreateUserLoginsDto cUserLoginsDto = new CreateUserLoginsDto(email, "", 8, true, "", 1);
                int ulid = await _ulRepository.AddAsync(_mapper.Map<userlogins>(cUserLoginsDto));

                //CreateCustomerProfileDto cp = new CreateCustomerProfileDto(ulid, name, ccp.legal_name, email, ccp.phone,
                //      ccp.payment_method_id, ccp.referral_name, ccp.fav_pickup_address, ccp.fav_dropoff_address, ccp.address,
                //      ccp.enrollment_date, ccp.business_type_id, ccp.invoice_frequency, ccp.sales_tax_num, ccp.proof_of_delivery,
                //      ccp.desired_attempts, ccp.other, ccp.created_by, 1);

                CreateCustomerProfileDto cp = new CreateCustomerProfileDto(
                                                    ulid,
                                                    name,
                                                    null,         
                                                    email,
                                                    null,         
                                                    1,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    null,         
                                                    1,         
                                                    1             
                                                );

                int cpid = await _cpRepository.AddAsync(_mapper.Map<customerprofile>(cp));

                CreateCustomerUserProfileDto cc = new CreateCustomerUserProfileDto(ulid, name, null, null,
                email, null, DateTime.Now, DateTime.Now, null, null, null,
                null, null, cp.id, 1, 1);
                int upid = await _cupRepository.AddAsync(_mapper.Map<customeruserprofile>(cc));

                return upid;
            }
            else
            {
                return 0;
            }
                

        }

        public async Task<bool> checkUserExist(string email)
        {
            bool res = await _ulRepository.checkUserExist(email);
            return res;
        }
    }
}
