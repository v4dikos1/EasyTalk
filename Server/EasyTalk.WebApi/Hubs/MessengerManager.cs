namespace EasyTalk.WebApi.Hubs
{
    public class MessengerManager
    {
        public List<ChatUser> Users { get; } = new();

        public void ConnectUser(Guid userId, string connectionId)
        {
            var userAlreadyExists = GetConnectedUserByUsername(userId);
            if (userAlreadyExists != null)
            {
                userAlreadyExists.AddConnection(connectionId);
                return;
            }

            var user = new ChatUser(userId);
            user.AddConnection(connectionId);
            Console.WriteLine($"--> New connection: {connectionId} for User {user.Id}");

            Users.Add(user);
        }

        public bool DisconnectUser(string connectionId)
        {
            var userExists = GetConnectedUserById(connectionId);
            if (userExists == null)
            {
                return false;
            }

            if (!userExists.Connections.Any())
            {
                return false;
            }

            var connectionExists = userExists.Connections.Select(c => c.ConnectionId).First().Equals(connectionId);
            if (!connectionExists)
            {
                return false;
            }

            if (userExists.Connections.Count() == 1)
            {
                Users.Remove(userExists);
                Console.WriteLine($"--> User {userExists} log out");

                return true;
            }

            userExists.RemoveConnection(connectionId);
            Console.WriteLine($"--> Connection {connectionId} disconnected");

            return false;
        }

        private ChatUser? GetConnectedUserById(string connectionId)
        {
            return Users.FirstOrDefault(u => u.Connections.Any(c => c.ConnectionId.Equals(connectionId)));
        }

        private ChatUser? GetConnectedUserByUsername(Guid userId)
        {
            return Users.FirstOrDefault(u => u.Id.Equals(userId));
        }
    }
}
