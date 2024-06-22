using System;

public interface ISession : IDisposable
{
        public ISession StartSession();
        public float GetSessionTime();
}