using System;

namespace Phytime.Models
{
    public class PageInfo
    {
        public PageInfo() { }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 5;
        public int TotalItems { get; set; } 
        public int TotalPages  
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }
}
