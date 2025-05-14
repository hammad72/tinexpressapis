using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class rejectedparcels
    {
        [Key]
        public int id { get; set; }
        public string? address_pick { get; set; }
        public string? post_code_pick { get; set; }
        public string? suburb_pick { get; set; }
        public string? city_pick { get; set; }
        public string? state_pick { get; set; }
        public string? country_pick { get; set; }
        public string? latlong_pick { get; set; }
        public bool? building_type_pick { get; set; }
        public bool? tail_lift_pick { get; set; }
        public string? address_drop { get; set; }
        public string? post_code_drop { get; set; }
        public string? suburb_drop { get; set; }
        public string? city_drop { get; set; }
        public string? state_drop { get; set; }
        public string? country_drop { get; set; }
        public string? latlong_drop { get; set; }
        public bool? building_type_drop { get; set; }
        public bool? tail_lift_drop { get; set; }
        public bool? pobox_drop { get; set; }
        public string? name_sender { get; set; }
        public string? email_sender { get; set; }
        public string? phone_sender { get; set; }
        public string? unit_sender { get; set; }
        public string? street_sender { get; set; }
        public string? street_name_sender { get; set; }
        public string? name_receiver { get; set; }
        public string? email_receiver { get; set; }
        public string? phone_receiver { get; set; }
        public string? unit_receiver { get; set; }
        public string? street_receiver { get; set; }
        public string? street_name_receiver { get; set; }
        public DateTime? pickup_date { get; set; }
        public DateTime? pickup_time { get; set; }
        public bool? auth_to_leave { get; set; }
        public bool? agree_tnc { get; set; }
        public bool? agree_ctnncnpr { get; set; }
        public string? special_instructions { get; set; }
        public int customer_id { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? created_at { get; set; }
    }
    public class rp_rpi
    {
        public rejectedparcels rp { get; set; }
        public List<rejectedparcelitems> rpi { get; set; }
    }
    /*
    public class rejectedParcel_columns
    {
        [Key]
        public int id { get; set; }
        public string? address_pick { get; set; }
        public string? address_drop { get; set; }
        public string? post_code_pick { get; set; }
        public string? post_code_drop { get; set; }
        public string? suburb_pick { get; set; }
        public string? suburb_drop { get; set; }
        public string? city_pick { get; set; }
        public string? city_drop { get; set; }
        public string? state_pick { get; set; }
        public string? state_drop { get; set; }
        public string? name_sender { get; set; }
        public string? name_receiver { get; set; }
        public string? email_sender { get; set; }
        public string? email_receiver { get; set; }
        public string? phone_sender { get; set; }
        public string? phone_receiver { get; set; }
    }
    */
}