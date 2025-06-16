using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;

namespace Hourly.Gateway
{
    public class InMemoryFileConfigurationRepository : IFileConfigurationRepository
    {
        private FileConfiguration _config;

        public InMemoryFileConfigurationRepository(FileConfiguration config)
        {
            _config = config;
        }

        public async Task<Response<FileConfiguration>> Get()
        {
            return new OkResponse<FileConfiguration>(_config);
        }

        public async Task<Response> Set(FileConfiguration fileConfiguration)
        {
            _config = fileConfiguration;
            return new OkResponse();
        }
    }
}
