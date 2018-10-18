using System.Collections.Generic;
using System.Threading.Tasks;
using VaultSharp;

namespace Config.HashiCorpVault
{
	public interface IVaultClientWrapper
	{
		Task<IDictionary<string, object>> GetSecretsAsync(string path);
	}

	public class VaultClientWrapper : IVaultClientWrapper
	{
		private readonly IVaultClient _client;

		public VaultClientWrapper(IVaultClient client)
		{
			_client = client;
		}

		public async Task<IDictionary<string, object>> GetSecretsAsync(string path)
		{
			var kv2Secret = await _client.V1.Secrets.KeyValue.V2.ReadSecretAsync(path);

			return kv2Secret.Data.Data;
		}
	}
}
