using System.Windows;

namespace bytePassion.Library.Essentials.WpfTools.Application
{
    public class BytePassionApplication : System.Windows.Application
    {
        public IApplicationLifeCycle LifeCycle { private get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LifeCycle?.Startup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            LifeCycle?.Exit(e);
        }

        protected override void OnSessionEnding(SessionEndingCancelEventArgs e)
        {
            base.OnSessionEnding(e);
            LifeCycle?.SessionEnding(e);
        }
    }
}
