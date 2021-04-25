using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MDC.WebClient
{
    public class State
    {
        private string _authToken;
        private string _user;

        public string User
        {
            get => _user;
            set { _user = value; OnStateChanged(); }
        }

        public string AuthToken
        {
            get => _authToken;
            set { _authToken = value; OnStateChanged(); }
        }

        public event Action StateChanged;

        protected virtual void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
        
    }
}
