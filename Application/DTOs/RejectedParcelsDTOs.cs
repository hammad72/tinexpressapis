using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record RejectedParcelsDto(int id, string? address_pick, string? post_code_pick, string? suburb_pick, string? city_pick, string? state_pick,
                                    string? country_pick, string? latlong_pick, bool? building_type_pick, bool? tail_lift_pick, string? address_drop,
                                    string? post_code_drop, string? suburb_drop, string? city_drop, string? state_drop, string? country_drop,
                                    string? latlong_drop, bool? building_type_drop, bool? tail_lift_drop, bool? pobox_drop, string? name_sender,
                                    string? email_sender, string? phone_sender, string? unit_sender, string? street_sender, string? street_name_sender,
                                    string? name_receiver, string? email_receiver, string? phone_receiver, string? unit_receiver, string? street_receiver,
                                    string? street_name_receiver, string? pickup_date, string? pickup_time, bool? auth_to_leave, bool? agree_tnc,
                                    bool? agree_ctnncnpr , string? special_instructions, int customer_id, DateTime? created_at);
    //public record CreateRejectedParcelsDto(string? address_pick, string? post_code_pick, string? suburb_pick, string? city_pick, string? state_pick,
    //                                string? country_pick, string? latlong_pick, bool? building_type_pick, bool? tail_lift_pick, string? address_drop,
    //                                string? post_code_drop, string? suburb_drop, string? city_drop, string? state_drop, string? country_drop,
    //                                string? latlong_drop, bool? building_type_drop, bool? tail_lift_drop, bool? pobox_drop, string? name_sender,
    //                                string? email_sender, string? phone_sender, string? unit_sender, string? street_sender, string? street_name_sender,
    //                                string? name_receiver, string? email_receiver, string? phone_receiver, string? unit_receiver, string? street_receiver,
    //                                string? street_name_receiver, DateTime? pickup_date, DateTime? pickup_time, bool? auth_to_leave, bool? agree_tnc,
    //                                bool? agree_ctnncnpr, string? special_instructions, int customer_id, DateTime? created_at);

    public record RejectedParcelItemsDto(int? id, int rp_id, int? package_type_id, string? package_type, int? package_content_id, string? package_content,
                                    int? qty, int? weight, int? length, int? width, int? height, string? unit);
    //public record CreateRejectedParcelItemsDto(int rp_id, int? package_type_id, string? package_type, int? package_content_id, string? package_content,
    //                                int? qty, int? weight, int? length, int? width, int? height, string? unit);

    //public record CreateRPDto(CreateRejectedParcelsDto rp, List<CreateRejectedParcelItemsDto> rpidto);
    public record RPDto(RejectedParcelsDto rp, List<RejectedParcelItemsDto> rpi);
}
