using System;
using System.Drawing;
using System.Numerics;
using Lab5.Network.Common;
using Lab5.Network.Common.UserApi;

internal class Program
{
    private static object _locker = new object();

    public static async Task Main(string[] args)
    {
        var serverAdress = new Uri("tcp://127.0.0.1:5555");
        var client = new NetTcpClient(serverAdress);
        Console.WriteLine($"Connect to server at {serverAdress}");
        await client.ConnectAsync();

        var userApi = new UserApiClient(client);
        await ManageUsers(userApi);
        client.Dispose();
    }

    private static async Task ManageUsers(IUserApi userApi)
    {
        //PrintMenu();
        while (true)
        {
            

           //PrintMenu();

            while (true)
            {
                string[] Array = new string[7]; // Создаём массив из 5 элементов

                // Заполняем массив данными о погоде
                Array[0] = "Active";
                Array[1] = "Pause";
                Array[2] = "Off";


                Console.Clear();
                var users = await userApi.GetAllAsync();
                Console.Clear();
                Console.WriteLine($"|    Id |             UserName |        Activity |");
                foreach (var user in users)
                {
                    Console.WriteLine($"| {user.Id,5} | {user.Name,20} | {user.Active,15} |");
                }
                Thread.Sleep(1000);
                Random random = new Random();

                int UserId = random.Next(1, 6);
                int ActivityId = random.Next(0, 3);
                Console.WriteLine(UserId);
                Console.WriteLine(ActivityId);
                string UserIdId = UserId.ToString();
                var userIdString = UserIdId;
                int.TryParse(userIdString, out var userId);
                var user1 = await userApi.GetAsync(userId);
                var Name = user1?.Name;
                var Active = user1?.Active;

                var addUser = new User(
                    Id: user1.Id,
                    Name: user1?.Name,
                    Age: 30,
                    Active: Array[ActivityId]
                );
                var addResult = await userApi.UpdateAsync(UserId, addUser); 
            }
            
    }

      
    }
}
