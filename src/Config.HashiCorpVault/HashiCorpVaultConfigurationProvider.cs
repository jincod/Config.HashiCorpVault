using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Config.HashiCorpVault
{
	public class HashiCorpVaultConfigurationProvider : ConfigurationProvider
	{
		private readonly IVaultClientWrapper _client;
		private readonly bool _optional;
		private readonly string _path;

		public HashiCorpVaultConfigurationProvider(IVaultClientWrapper client, string path, bool optional)
		{
			_client = client;
			_path = path;
			_optional = optional;
		}

		public override void Load() => LoadAsync().ConfigureAwait(false).GetAwaiter().GetResult();

		private async Task LoadAsync()
		{
			var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

			try
			{
				var secrets = await _client.GetSecretsAsync(_path);
				data = secrets.ToDictionary(x => x.Key, x => x.Value.ToString());
			}
			catch (Exception)
			{
				if (!_optional)
					throw;
			}

			Data = data;
		}
	}
}
