using System;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
	public interface ITokenHandler
	{
		Task<string> CreateTokenAsync(User user);
	}
}

