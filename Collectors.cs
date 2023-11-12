using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class Collectors
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Full_name { get; set; } //Full name of the shareholder
        [Required]
        public string? Name { get; set; } //name of shares
        [Required]
        public int Share { get; set; } //share price
        [Required]
        public string? Number { get; set; } //number of shares
        [Required]
        public int Date { get; set; } //date of purchase of shares
        [Required]
        public Decimal? Accruals { get; set; } 
    }
}
