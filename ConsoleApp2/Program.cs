using ConsoleApp2.Entities;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
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

            /*   CloudTableClient cloudTableClient = cloudStorageAccount.CreateCloudTableClient();

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

       */
            CloudQueueClient cloudQueueClient = cloudStorageAccount.CreateCloudQueueClient();
            CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("tasks");
            cloudQueue.CreateIfNotExists();

            //CloudQueueMessage cloudQueueMessage = new CloudQueueMessage("HOla papi");
            //var time = new TimeSpan(24, 0, 0);
            //cloudQueue.AddMessage(cloudQueueMessage, time, null, null);
            
            CloudQueueMessage cloudQueueMessage = cloudQueue.GetMessage();
            Console.WriteLine(cloudQueueMessage.AsString);
            cloudQueue.DeleteMessage(cloudQueueMessage);

            //CloudQueueMessage cloudQueueMessage = cloudQueue.PeekMessage();
           // Console.WriteLine(cloudQueueMessage.AsString);
           
            Console.ReadKey();
        }

        
    }
}
