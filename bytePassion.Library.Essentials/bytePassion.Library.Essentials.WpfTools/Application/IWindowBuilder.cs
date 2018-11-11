namespace bytePassion.Library.Essentials.WpfTools.Application
{
	public interface IWindowBuilder<TWindow>
	{
		TWindow BuildWindow ();
		void DisposeWindow(TWindow buildedWindow);
	}
}
