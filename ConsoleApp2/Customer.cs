using System;
using System.Collections.Generic;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp2.Entities;

namespace ConsoleApp2
{
    class Customer
    {
        static void CreateCustomer(CloudTable cloudTable, CustomerUS customerUS)
        {
            TableOperation insertOperation = TableOperation.Insert(customerUS);

            cloudTable.Execute(insertOperation);
        }

        static void GetCustomer(CloudTable cloudTable, string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerUS>(partitionKey, rowKey);
            var result = cloudTable.Execute(retrieveOperation);
            Console.WriteLine(((CustomerUS)result.Result).Name);
        }

        static CustomerUS GetOneCustomer(CloudTable cloudTable, string partitionKey, string rowKey)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerUS>(partitionKey, rowKey);
            var result = cloudTable.Execute(retrieveOperation);
            return (CustomerUS)result.Result;
        }

        static void GetAllCustomers(CloudTable cloudTable)
        {
            TableQuery<CustomerUS> query = new TableQuery<CustomerUS>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "US"));
            foreach (CustomerUS customerUs in cloudTable.ExecuteQuery(query))
            {
                Console.WriteLine(customerUs.Name);
            }
        }

        static void UpdateCustomer(CloudTable cloudTable, CustomerUS customerUS)
        {
            TableOperation updateOperation = TableOperation.Replace(customerUS);
            cloudTable.Execute(updateOperation);
        }

        static void DeleteCustomer(CloudTable cloudTable, CustomerUS customerUS)
        {
            TableOperation deleteOperation = TableOperation.Delete(customerUS);
            cloudTable.Execute(deleteOperation);
        }

    }
}
