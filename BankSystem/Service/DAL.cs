using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoRepository;
using Service.Models;

namespace Service
{
    public class DAL
    {
        private static readonly Lazy<DAL> Lazy = new Lazy<DAL>(() => new DAL());
        public static DAL Instance => Lazy.Value;
        private const string MappingFilePath = "../../../BankIdToIpMapping.csv";

        private DAL()
        {
            Configurations = new MongoRepository<ConfigKeyValue, string>();

            //if (!Configurations.Any(config => config.Key == "CurrentAccountId"))
            if (!Configurations.Exists(config => config.Key == "CurrentAccountId"))
            {
                Configurations.Add(new ConfigKeyValue
                {
                    Key = "CurrentAccountId",
                    Value = "0000000000000000"
                });
            }

            if (!Configurations.Exists(config => config.Key == "CurrentSessionId"))
            {
                Configurations.Add(new ConfigKeyValue
                {
                    Key = "CurrentSessionId",
                    Value = "0000000000000000"
                });
            }

            Users = new MongoRepository<User, string>();
            Accounts = new MongoRepository<Account, string>();
            Operations = new MongoRepository<Operation, string>();
            Sessions = new MongoRepository<Session, string>();
            BankIdToIpMapping = new Dictionary<string, string>();
            ReadBankIdToIpMappingFromFile();

            //Client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDB"].ConnectionString);
            //Database = Client.GetDatabase("banksystem");

            //Configurations = Database.GetCollection<ConfigKeyValue>("Configurations");

            //if (!Configurations.AsQueryable().Any(config => config.Key == "CurrentAccountId"))
            //{
            //    Configurations.InsertOne(new ConfigKeyValue
            //    {
            //        Key = "CurrentAccountId",
            //        Value = "0000000000000000"
            //    });
            //}

            //Users = Database.GetCollection<User>("Users");
            //Accounts = Database.GetCollection<Account>("Accounts");
            //Operations = Database.GetCollection<Operation>("Operations");
        }

        private void ReadBankIdToIpMappingFromFile()
        {           
            try
            {
                using (var streamReader = new StreamReader(MappingFilePath))
                {
                    var line = streamReader.ReadLine();

                    while ((line = streamReader.ReadLine()) != null)
                    {
                        var splitted = line.Split(';');
                        BankIdToIpMapping.Add(splitted[0], splitted[1]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        //public IMongoClient Client { get; set; }
        //public IMongoDatabase Database { get; set; }
        //public IMongoCollection<User> Users { get; set; } 
        //public IMongoCollection<Account> Accounts { get; set; }
        //public IMongoCollection<Operation> Operations { get; set; }
        //public IMongoCollection<ConfigKeyValue> Configurations { get; set; }

        public MongoRepository<User, string> Users { get; set; }
        public MongoRepository<Account, string> Accounts { get; set; }
        public MongoRepository<Operation, string> Operations { get; set; }
        public MongoRepository<Session, string> Sessions { get; set; }
        public MongoRepository<ConfigKeyValue, string> Configurations { get; set; }
        public Dictionary<string, string> BankIdToIpMapping { get; set; }

        //public static IMongoCollection<Artist> Artists;
        //public static IMongoCollection<Song> Songs;
        //public static IMongoCollection<User> Users;
        //public static IMongoCollection<Listen> Listens;
        //public static IMongoCollection<FullListen> FullListens;
        //public static IMongoCollection<Statistics> Statistics;
    }
}
