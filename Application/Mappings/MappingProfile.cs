using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AgingReport, AgingReportDTO>();

            CreateMap<shipment_columns, shipmentDto>();

            CreateMap<options, OptionsDto>();

            CreateMap<ordersource, OrderSourceDto>();

            CreateMap<socialinfo, SocialInfoDTO>();

            CreateMap<loginmodel, LoginModelDto>();
            CreateMap<CreateAndUpdateLoginModelDto, loginmodel>();

            CreateMap<logindetail, LoginDetailDto>();
            CreateMap<CreateLoginDetailDto, logindetail>();

            CreateMap<userlogins, UserLoginsDto>();
            CreateMap<CreateUserLoginsDto, userlogins>();
            CreateMap<UpdateUserLoginsDto, userlogins>();

            CreateMap<userprofile, UserProfileDto>();
            CreateMap<CreateUserProfileDto, userprofile>();
            CreateMap<UpdateUserProfileDto, userprofile>();
            CreateMap<userprofile, UserProfileDto>().ReverseMap();
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));

            CreateMap<customerprofile, CustomerProfileDto>();
            CreateMap<CreateCustomerProfileDto, customerprofile>();
            CreateMap<UpdateCustomerProfileDto, customerprofile>();

            CreateMap<customeruserprofile, CustomerUserProfileDto>();
            CreateMap<CreateCustomerUserProfileDto, customeruserprofile>();
            CreateMap<UpdateCustomerUserProfileDto, customeruserprofile>();

            CreateMap<couriers, CouriersDto>();
            CreateMap<CreateCouriersDto, couriers>();
            CreateMap<UpdateCouriersDto, couriers>();

            CreateMap<courieruserprofile, CourierUserProfileDto>();
            CreateMap<CreateCourierUserProfileDto, courieruserprofile>();
            CreateMap<UpdateCourierUserProfileDto, courieruserprofile>();

            CreateMap<packagetype, PackageTypeDto>();
            CreateMap<CreatePackageTypeDto, packagetype>();
            CreateMap<UpdatePackageTypeDto, packagetype>();

            CreateMap<packagecontent, PackageContentDto>();
            CreateMap<CreatePackageContentDto, packagecontent>();
            CreateMap<UpdatePackageContentDto, packagecontent>();

            CreateMap<paymentmethod, PaymentMethodDto>();
            CreateMap<CreatePaymentMethodDto, paymentmethod>();
            CreateMap<UpdatePaymentMethodDto, paymentmethod>();

            CreateMap<usertypes, UserTypesDto>();
            CreateMap<CreateUserTypesDto, usertypes>();
            CreateMap<UpdateUserTypesDto, usertypes>();

            CreateMap<OrderDto, order>();
            CreateMap<OrderDetailsDto, orderdetails>();
            CreateMap<orderdetails, OrderDetailsDto>();

            CreateMap<OrderItemsDto, orderitems>();
            CreateMap<orderitems, OrderItemsDto>();

            CreateMap<CreateCustomerPriorityDtoArr, customerpriorityArr>();
            CreateMap<customerpriority, CustomerPriorityDto>();
            CreateMap<CreateCustomerPriorityDto, customerpriority>();
            CreateMap<UpdateCustomerPriorityDto, customerpriority>();

            CreateMap<CreateCustomerBudgetDtoArr, customerbudgetArr>();
            CreateMap<customerbudget, CustomerBudgetDto>();
            CreateMap<CreateCustomerBudgetDto, customerbudget>();
            CreateMap<UpdateCustomerBudgetDto, customerbudget>();

            CreateMap<orderstatuses, OrderStatusesDto>();
            CreateMap<CreateOrderStatusesDto, orderstatuses>();
            CreateMap<UpdateOrderStatusesDto, orderstatuses>();

            CreateMap<orderstatuses, OrderStatusesDto>();
            CreateMap<CreateOrderStatusesDto, orderstatuses>();
            CreateMap<UpdateOrderStatusesDto, orderstatuses>();

            CreateMap<courierstatuses, CourierStatusesDto>();
            CreateMap<CreateCourierStatusesDto, courierstatuses>();
            CreateMap<UpdateCourierStatusesDto, courierstatuses>();

            CreateMap<courierstatusmapping, CourierStatusMappingDto>();
            CreateMap<CreateCourierStatusMappingDto, courierstatusmapping>();
            CreateMap<UpdateCourierStatusMappingDto, courierstatusmapping>();//Test 2

            CreateMap<SenderRecieverOrderItems, SenderRecieverOrderItemsDto>();
            CreateMap<SummaryOrderItems, SummaryOrderItemsDTO>();
            CreateMap<TrackingOrderItems, TrackingOrderItemsDTO>();
            CreateMap<CourierInfoOrderItems, CourierInfoOrderItemsDTO>();
        }
    }
}