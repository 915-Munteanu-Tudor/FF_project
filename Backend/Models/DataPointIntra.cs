using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Backend.Models
{
    public class DataPointIntra
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal ClosingPrice { get; set; }
        public decimal HighestPrice { get; set; }
        public decimal LowestPrice { get; set; }
        public decimal OpeningPrice { get; set; }
        public DateTime Time { get; set; }
        public long Volume { get; set; }
        public DataPointIntra(string name, decimal closingPrice, decimal highestPrice, decimal lowestPrice, decimal openingPrice, DateTime time, long volume)
        {
            Name = name;
            ClosingPrice = closingPrice;
            HighestPrice = highestPrice;
            LowestPrice = lowestPrice;
            OpeningPrice = openingPrice;
            Time = time;
            Volume = volume;
        }

        override
        public string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Id: " + Id + ", Name: " + Name + ", ClosingPrice: " + ClosingPrice + ", HighestPrice: " + HighestPrice + ", LowestPrice: " + LowestPrice + ", OpeningPrice: " + OpeningPrice + ", Time: " + Time + ", Volume: " + Volume);
            return sb.ToString();
        }
    }
}
