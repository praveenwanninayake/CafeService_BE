using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeService.Api.Models
{
   
        public class MessageModel<TEntity>
        {
            public string Status { get; set; } = "E";
            public string Text { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
            public TEntity Result { get; set; }
        }

        public class DataResponse<TEntity>
        {
            public TEntity Data { get; set; }
            public Payload Payload { get; set; }          
        }

        public class Arrylsit
        {
            public string Url { get; set; }
            public string Label { get; set; }
            public bool Active { get; set; }
            public int? Page { get; set; } = null;
        }

        public class PaginationInfo
        {
            public List<Arrylsit> Links { get; set; }
            public int Page { get; set; }
            public string First_page_url { get; set; }
            public int From { get; set; }
            public int Last_page { get; set; }
            public string Next_page_url { get; set; }
            public int Items_per_page { get; set; }
            public string Prev_page_url { get; set; }
            public int To { get; set; }
            public int Total { get; set; }
            public int? FullTotal { get; set; }
        }

        public class Payload
        {
            public PaginationInfo Pagination { get; set; }
        }

    
}
