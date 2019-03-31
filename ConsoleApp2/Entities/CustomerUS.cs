using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Entities
{
    class CustomerUS: TableEntity
    {

        public string Name { get; set; }
        public string Email { get; set; }

        public CustomerUS(string Name, string Email)
        {
            this.Name = Name;
            this.Email = Email;
            this.PartitionKey = "US";
            this.RowKey = Email;
        }

        public CustomerUS()
        {

        }
    }
}
