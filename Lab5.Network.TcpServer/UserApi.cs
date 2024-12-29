using System.Collections.Concurrent;
using Lab5.Network.Common.UserApi;

internal class UserApi : IUserApi
{
    private static readonly ConcurrentDictionary<int, User> userRepository 
        = new ConcurrentDictionary<int, User>() {
            [1] = new User(1, "Julie Walker", "Off", 20),
            [2] = new User(2, "Maria Smith", "Off", 21),
            [3] = new User(3, "Sharon Williams", "Off", 19),
            [4] = new User(4, "Ann Morris", "Off", 20),
            [5] = new User(5, "Mary Harris", "Off", 21),
        };
    private static int _lastId;

    public UserApi()
    {
        _lastId = userRepository.Count + 1;
    }


    public Task<bool> AddAsync(User newUser)
    {
        newUser = newUser with 
        {
            Id = _lastId
        };

        if (userRepository.ContainsKey(_lastId)) 
        {
            return Task.FromResult(false);
        }

        var result = userRepository.TryAdd(_lastId, newUser);

        if (!result) 
        {
            return Task.FromResult(false);
        }
        Interlocked.Increment(ref _lastId);

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(int id)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(false);
        }

        var result = userRepository.Remove(id, out var _);

        return Task.FromResult(result);
    }

    public Task<User[]> GetAllAsync()
    {
        return Task.FromResult(userRepository.Values.ToArray());
    }

    public Task<User?> GetAsync(int id)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(default(User));
        }

        return Task.FromResult<User?>(userRepository[id]);
    }

    public Task<bool> UpdateAsync(int id, User updateUser)
    {
        if (!userRepository.ContainsKey(id)) 
        {
            return Task.FromResult(false);
        }
        

        var result = userRepository.TryUpdate(id, updateUser, userRepository[id]);
        return Task.FromResult(result);
    }
}