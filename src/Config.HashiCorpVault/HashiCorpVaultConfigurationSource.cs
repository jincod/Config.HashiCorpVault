using Microsoft.Extensions.Configuration;
using VaultSharp;

namespace Config.HashiCorpVault
{
	public class HashiCorpVaultConfigurationSource : IConfigurationSource
	{
		private readonly IVaultClient _client;
		private readonly bool _isSilentFailt;
		private readonly string _path;

		public HashiCorpVaultConfigurationSource(IVaultClient client, string path, bool isSilentFailt)
		{
			_client = client;
			_path = path;
			_isSilentFailt = isSilentFailt;
		}

		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			return new HashiCorpVaultConfigurationProvider(new VaultClientWrapper(_client), _path, _isSilentFailt);
		}
	}
}
