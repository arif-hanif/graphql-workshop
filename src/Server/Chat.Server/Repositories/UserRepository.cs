using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Chat.Server.Repositories
{
    public class UserRepository
        : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(IMongoCollection<User> users)
        {
            _users = users;

            _users.Indexes.CreateOne(
                new CreateIndexModel<User>(
                    Builders<User>.IndexKeys.Ascending(x => x.Name),
                    new CreateIndexOptions { Unique = true }));

            _users.Indexes.CreateOne(
                new CreateIndexModel<User>(
                    Builders<User>.IndexKeys.Ascending(x => x.Email),
                    new CreateIndexOptions { Unique = true }));
        }

        public IQueryable<User> Users => _users.AsQueryable();

        public Task<User> GetUserByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return _users.AsQueryable().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyDictionary<Guid, User>> GetUsersByIdsAsync(
            IReadOnlyList<Guid> ids,
            CancellationToken cancellationToken)
        {
            List<User> result = await _users.AsQueryable()
                .Where(t => ids.Contains(t.Id))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return result.ToDictionary(t => t.Id);
        }

        public async Task<IReadOnlyDictionary<string, User>> GetUsersByNamesAsync(
            IReadOnlyList<string> names,
            CancellationToken cancellationToken)
        {
            List<User> result = await _users.AsQueryable()
                .Where(t => names.Contains(t.Name))
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return result.ToDictionary(t => t.Name);
        }

        public Task CreateUserAsync(
            User user,
            CancellationToken cancellationToken) =>
            _users.InsertOneAsync(user, options: default, cancellationToken);

        public async Task UpdatePasswordAsync(
            string userName,
            string newPasswordHash,
            string salt,
            CancellationToken cancellationToken)
        {
            await _users.UpdateOneAsync(
                Builders<User>.Filter.Eq(t => t.Name, userName),
                Builders<User>.Update.Combine(
                    Builders<User>.Update.Set(t => t.PasswordHash, newPasswordHash),
                    Builders<User>.Update.Set(t => t.Salt, salt)),
                options: default,
                cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task AddFriendIdAsync(
            string userName,
            Guid friendId,
            CancellationToken cancellationToken = default)
        {
            await _users.UpdateOneAsync(
                Builders<User>.Filter.Eq(t => t.Name, userName),
                Builders<User>.Update.AddToSet(t => t.FriendIds, friendId),
                options: default,
                cancellationToken)
                .ConfigureAwait(false);
        }
    }
}