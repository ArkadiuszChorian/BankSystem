using System;
using System.Collections.Generic;
using System.IO;
using MongoRepository;
using Service.Models;

namespace Service
{
    /// <summary>
    /// Class that represents Data Access Layer
    /// </summary>
    public class DAL
    {
        private static readonly Lazy<DAL> Lazy = new Lazy<DAL>(() => new DAL());
        /// <summary>
        /// Singleton instance of DAL
        /// </summary>
        public static DAL Instance => Lazy.Value;
        private const string MappingFilePath = "../../../BankIdToIpMapping.csv";

        private DAL()
        {
            Configurations = new MongoRepository<ConfigKeyValue, string>();
            
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

        /// <summary>
        /// Gets users repository
        /// </summary>
        public MongoRepository<User, string> Users { get; private set; }
        /// <summary>
        /// Gets accounts repository
        /// </summary>
        public MongoRepository<Account, string> Accounts { get; private set; }
        /// <summary>
        /// Gets operations repository
        /// </summary>
        public MongoRepository<Operation, string> Operations { get; private set; }
        /// <summary>
        /// Gets sessions repository
        /// </summary>
        public MongoRepository<Session, string> Sessions { get; private set; }
        /// <summary>
        /// Gets configurations repository
        /// </summary>
        public MongoRepository<ConfigKeyValue, string> Configurations { get; private set; }
        /// <summary>
        /// Gets dictionary with bank ids as keys
        /// </summary>
        public Dictionary<string, string> BankIdToIpMapping { get; private set; }
    }
}
