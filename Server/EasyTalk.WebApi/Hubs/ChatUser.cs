using EasyTalk.Application.Common.Exceptions;

namespace EasyTalk.WebApi.Hubs
{
    public class ChatUser
    {
        public Guid Id { get; }
        private List<MessengerConnection> _connections;

        public IEnumerable<MessengerConnection> Connections => _connections;

        public ChatUser(Guid userId)
        {
            Id = userId;
            _connections = new List<MessengerConnection>();
        }

        public void AddConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connection = new MessengerConnection
            {
                ConnectionId = connectionId
            };

            _connections.Add(connection);
        }

        public void RemoveConnection(string connectionId)
        {
            if (connectionId == null)
            {
                throw new ArgumentNullException(nameof(connectionId));
            }

            var connection = _connections.SingleOrDefault(c => c.ConnectionId.Equals(connectionId));

            if (connection == null)
            {
                throw new NotFoundException(nameof(MessengerConnection), connectionId);
            }

            _connections.Remove(connection);
        }
    }
}
