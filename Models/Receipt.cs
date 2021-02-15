namespace CookTime.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Receipt")]
    public partial class Receipt
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int User_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string Name { get; set; }

        public TimeSpan? Time { get; set; }

        public int? Callories { get; set; }

        public decimal? Proteins { get; set; }

        public decimal? Fats { get; set; }

        public decimal? Carbohydrates { get; set; }

        public string Ingredients { get; set; }

        public string Steps { get; set; }
        public string Type { get; set; }

        public byte[] Image { get; set; }

        [Key]
        [Column(Order = 2, TypeName = "date")]
        public DateTime Date_create { get; set; }

        public virtual User User { get; set; }
    }
}
