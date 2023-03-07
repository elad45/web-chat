namespace server.Models
{
    public interface IInvitationService
    {
        public bool Invite(string from, string to, string server);
    }
}
