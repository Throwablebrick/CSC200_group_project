
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MonoGameLibrary.Scenes;

public abstract class Scene : IDisposable
{
	protected ContentManager Content { get; }

	public bool IsDisposed { get; private set; }

	public Scene()
	{
		Content = new ContentManager(Core.Content.ServiceProvider);
		//set the root directory for game contents to the game's directory
		Content.RootDirectory = Core.Content.RootDirectory;
	}
	~Scene() => Dispose(false);

	//make sure to call base.Initialize() so that LoadContent gets called(that's why you have to do that in the base project templates)
	public virtual void Initialize()
	{
		LoadContent();
	}
	public virtual void LoadContent() { }
	public virtual void UnloadContent()
	{
		Content.Unload();
	}
	public virtual void Update(GameTime gameTime) { }
	public virtual void Draw(GameTime gameTime) { }


	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	//bool is meant to show if it will actually unload content
	protected virtual void Dispose(bool disposing)
	{
		if (IsDisposed)
		{
			return;
		}

		if (disposing)
		{
			UnloadContent();
			Content.Dispose();
		}
		IsDisposed = true; 
	}
}
