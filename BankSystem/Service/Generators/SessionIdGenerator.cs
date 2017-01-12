using System.Linq;

namespace Service.Generators
{
    public class SessionIdGenerator
    {
        public string GenerateId()
        {
            var currentSessionIdConfigKeyValue = DAL.Instance.Configurations.First(config => config.Key == "CurrentSessionId");
            var currentSessionIdAsLong = long.Parse(currentSessionIdConfigKeyValue.Value);
            currentSessionIdAsLong++;
            var currentSessionIdAsString = currentSessionIdAsLong.ToString();

            while (currentSessionIdAsString.Length < 16)
            {
                currentSessionIdAsString = "0" + currentSessionIdAsString;
            }

            currentSessionIdConfigKeyValue.Value = currentSessionIdAsString;

            DAL.Instance.Configurations.Update(currentSessionIdConfigKeyValue);

            return currentSessionIdAsString;
        }
    }
}
