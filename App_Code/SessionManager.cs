using System.Web.SessionState;
using System.Linq;

/// <summary>
/// Summary description for SessionManager
/// </summary>
public class SessionManager : Singleton<SessionManager> {
    private HttpSessionState session {
        get {
            return System.Web.HttpContext.Current.Session;
        }
    }

    public bool LoggedIn {
        get { return LoggedUser != null; }
    }

    public string LoggedUser {
        get {
            object userObj = session["user"];
            if (userObj == null) {
                return null;
            }
            return userObj as string;
        }
    }

    public void Login(string user) {
        session["user"] = user;
    }
    public void Logout() {
        session.Clear();
    }
}
