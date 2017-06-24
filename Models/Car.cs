namespace CarFinder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Car
    {
        [StringLength(50)]
        public string id { get; set; }

        public int? make_id { get; set; }

        [StringLength(50)]
        public string model { get; set; }

        [StringLength(128)]
        public string trim { get; set; }

        [StringLength(50)]
        public string year { get; set; }

        public int? bodystyle_id { get; set; }

        public int? engine_position_id { get; set; }

        [StringLength(50)]
        public string engine_cc { get; set; }

        [StringLength(50)]
        public string engine_cyl { get; set; }

        public int? engine_type_id { get; set; }

        [StringLength(50)]
        public string engine_valves_per_cyl { get; set; }

        [StringLength(50)]
        public string engine_power_ps { get; set; }

        [StringLength(50)]
        public string engine_power_rpm { get; set; }

        [StringLength(50)]
        public string engine_torque_nm { get; set; }

        [StringLength(50)]
        public string engine_torque_rpm { get; set; }

        [StringLength(50)]
        public string engine_bore_mm { get; set; }

        [StringLength(50)]
        public string engine_stroke_mm { get; set; }

        [StringLength(50)]
        public string engine_compression { get; set; }

        public int? engine_fuel_id { get; set; }

        [StringLength(50)]
        public string top_speed_kph { get; set; }

        [StringLength(50)]
        public string zero_to_100_kph { get; set; }

        public int? drive_id { get; set; }

        public int? transmission_type_id { get; set; }

        [StringLength(50)]
        public string seats { get; set; }

        [StringLength(50)]
        public string doors { get; set; }

        [StringLength(50)]
        public string weight_kg { get; set; }

        [StringLength(50)]
        public string length_mm { get; set; }

        [StringLength(50)]
        public string width_mm { get; set; }

        [StringLength(50)]
        public string height_mm { get; set; }

        [StringLength(50)]
        public string wheelbase_mm { get; set; }

        [StringLength(50)]
        public string lkm_hwy { get; set; }

        [StringLength(50)]
        public string lkm_mixed { get; set; }

        [StringLength(50)]
        public string lkm_city { get; set; }

        [StringLength(50)]
        public string fuel_cap_l { get; set; }

        [StringLength(50)]
        public string sold_in_us { get; set; }

        [StringLength(50)]
        public string co2 { get; set; }

        [StringLength(50)]
        public string make_display { get; set; }

        public virtual BodyStyle BodyStyle { get; set; }

        public virtual DriveType DriveType { get; set; }

        public virtual EngineFuel EngineFuel { get; set; }

        public virtual EnginePosition EnginePosition { get; set; }

        public virtual EngineType EngineType { get; set; }

        public virtual Make Make { get; set; }

        public virtual TransmissionType TransmissionType { get; set; }
    }
}
