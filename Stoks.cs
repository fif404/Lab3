using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class Stoks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Name  { get; set; } //name of shares
        [Required]
        public string? Cost  { get; set; }  //cost of 1 share
        public decimal? Dividends { get; set; } //% of dividends per share per year
    }
}
