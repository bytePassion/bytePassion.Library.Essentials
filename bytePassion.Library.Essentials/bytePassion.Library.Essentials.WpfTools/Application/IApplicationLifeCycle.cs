using System.Windows;

namespace bytePassion.Library.Essentials.WpfTools.Application
{
    public interface IApplicationLifeCycle
    {
        void Startup (StartupEventArgs startupEventArgs);
        void SessionEnding (SessionEndingCancelEventArgs sessionEndingCancelEventArgs);
        void Exit (ExitEventArgs exitEventArgs);
    }
}