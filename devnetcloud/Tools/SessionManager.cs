using DAL.Entities;

namespace devnetcloud.Tools
{
    public class SessionManager
    {
        private readonly ISession _session;
        public SessionManager(IHttpContextAccessor accessor)
        {
            _session = accessor.HttpContext.Session;
        }

        public string Token
        {
            get { return _session.GetString("token"); }
            set { _session.SetString("token", value); }
        }



        public void Logout()
        {
            _session.Clear();
        }

    }
}
