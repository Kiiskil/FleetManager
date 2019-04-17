using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManager.Models{
    [Table("cars")]
    public class Car {
        [Key, Column("regno")]
        public string Regno { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("inspection_date")]
        public string InspectionDate { get; set; }

        [Column("modelID")]
        public int ModelID { get; set; }

        [Column("motorID")]
        public int MotorID { get; set; }

        [Column("brandID")]
        public int BrandID { get; set; }
    }
}