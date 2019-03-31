using ConsoleApp2.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));

            CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();

            CloudTable cloudTable = cloudTableClient.GetTableReference("customers");

            cloudTable.CreateIfNotExists();

            TableBatchOperation tableBatchOperation = new TableBatchOperation();

            var customer1 = new CustomerUS("Aldo", "almalaver@hotmail.com");
            var customer2 = new CustomerUS("Junior", "pxamalav@cibertec.edu.pe");
            var customer3 = new CustomerUS("Paty", "npatrimc@hotmail.com");

            tableBatchOperation.Insert(customer1);
            tableBatchOperation.Insert(customer2);
            tableBatchOperation.Insert(customer3);

            cloudTable.ExecuteBatch(tableBatchOperation);
            GetAllCustomers(cloudTable);
            //CreateCustomer(cloudTable, new CustomerUS("Junior", "almalaver1@gmail.com"));
            //GetCustomer(cloudTable, "US", "almalaver@gmail.com");
            //GetAllCustomers(cloudTable);

            //var aldoCustomer = GetOneCustomer(cloudTable, "US", "almalaver@gmail.com");
            // aldoCustomer.Name = "Aldo Junior";
            // UpdateCustomer(cloudTable, aldoCustomer);
            //DeleteCustomer(cloudTable, aldoCustomer);
            //GetAllCustomers(cloudTable);
            Console.ReadKey();
        }

        static void CreateCustomer(CloudTable cloudTable , CustomerUS customerUS)
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
            foreach(CustomerUS customerUs in cloudTable.ExecuteQuery(query))
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
