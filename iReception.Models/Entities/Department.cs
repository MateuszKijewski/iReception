using System;
using System.Collections.Generic;
using System.Text;

namespace iReception.Models.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }

        /* Relations */
        public IEnumerable<Worker> Workers { get; set; }

        /* Flags */
        public bool IsDeleted { get; set; }
    }

}
