using Microsoft.Extensions.Configuration;
using VaultSharp;
using VaultSharp.V1.AuthMethods.Token;

namespace Config.HashiCorpVault
{
	public static class HashiCorpVaultConfigurationExtensions
	{
		public static IConfigurationBuilder AddHashiCorpVault(
			this IConfigurationBuilder configurationBuilder,
			IVaultClient client,
			string path
		)
		{
			return AddHashiCorpVault(configurationBuilder, client, path, false);
		}

		public static IConfigurationBuilder AddHashiCorpVault(
			this IConfigurationBuilder configurationBuilder,
			string vault,
			string token,
			string path
		)
		{
			return AddHashiCorpVault(configurationBuilder, vault, token, path, false);
		}

		public static IConfigurationBuilder AddHashiCorpVault(
			this IConfigurationBuilder configurationBuilder,
			string vault,
			string token,
			string path,
			bool optional
		)
		{
			var client = GetClient(vault, token, optional);
			return AddHashiCorpVault(configurationBuilder, client, path, optional);
		}

		public static IConfigurationBuilder AddHashiCorpVault(
			this IConfigurationBuilder configurationBuilder,
			IVaultClient client,
			string path,
			bool optional
		)
		{
			configurationBuilder.Add(new HashiCorpVaultConfigurationSource(client, path, optional));

			return configurationBuilder;
		}

		private static IVaultClient GetClient(string url, string token, bool optional)
		{
			if (!string.IsNullOrEmpty(url)|| !optional)
			{
				return new VaultClient(new VaultClientSettings(url, new TokenAuthMethodInfo(token)));
			}
			return null;
		}
	}
}
