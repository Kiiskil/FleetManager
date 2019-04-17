using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FleetManager.Models{

    [Table("model")]
    public class Model{
        [Key, Column("modelID")]
        public int ID{get;set;}
        [Column("name")]
        public string Name{get;set;}
        public Car Car {get;set;}
    }

    [Table("motor")]
    public class Motor{
        [Key, Column("motorID")]
        public int ID{get;set;}
        [Column("motorsize")]
        public double Size{get;set;}
        [Column("power")]
        public int Power{get;set;}
        public Car Car {get;set;}
    }

    [Table("brand")]
    public class Brand{
        [Key, Column("brandID")]
        public int ID{get;set;}
        [Column("name")]
        public string Name{get;set;}
        public Car Car {get;set;}
    }

    [Table("cars")]
    public class Car {
        [Key, Column("regno")]
        public string Regno { get; set; }

        [Column("year")]
        public int Year { get; set; }

        [Column("inspection_date")]
        public string InspectionDate { get; set; }

        [Column("modelID")]
       /*  public int ModelID { get; set; } */
        public Model Model {get;set;} //Navigation property

        [Column("motorID")]
/*         public int MotorID { get; set; } */
        public Motor Motor {get;set;}
        [Column("brandID")]
        /* public int BrandID { get; set; } */
        public Brand Brand {get;set;}
    }
}