using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System;

namespace Necromancer;

public class NecroGame : Game
{
	private static NecroGame s_instance;
	public static NecroGame Instance => s_instance;

	public static new GraphicsDevice GraphicsDevice { get; private set; }
	public static GraphicsDeviceManager Graphics { get; private set; }
	public static new ContentManager Content { get; private set; }
	public static SpriteBatch SpriteBatch { get; private set; }

	public static string GameTitle { get; private set; }
	public static int WindowWidth => Graphics.PreferredBackBufferWidth;
	public static int WindowHeight => Graphics.PreferredBackBufferHeight;
	public static Time Time;

	public SceneManager SceneManager;
	public Scene CurrentScene => SceneManager.CurrentScene;
	public Scene LastScene => SceneManager.LastScene;

	public static Input Input;

	public static bool IsDebugMode;

	public NecroGame(string windowTitle, int ww, int wh, bool isfullscreen, bool isdebug=true)
	{
		s_instance = this;

		Graphics = new GraphicsDeviceManager(this);

		Content = base.Content;
		Content.RootDirectory = "Content";

		GameTitle = windowTitle;
		Window.Title = GameTitle;

		SceneManager = new();

		Input = new();

		Drawer.Initialize(GraphicsDevice);

		SetWindowSize(ww, wh);
		SetWindowFullscreen(isfullscreen);

		IsMouseVisible = true;

		IsDebugMode = isdebug;

		SceneManager = new();
	}

	protected override void Initialize()
	{
		GraphicsDevice = base.GraphicsDevice;

		SpriteBatch = new SpriteBatch(GraphicsDevice);

		base.Initialize();
	}

	protected override void Update(GameTime gameTime)
	{
		Time.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

		if(IsDebugMode)
		{
			Window.Title = $"{GameTitle} -- {GC.GetTotalMemory(true)/1024/1024} Mb";
		}

		SceneManager.BeforeUpdate(Time);
		SceneManager.Update(Time);
		SceneManager.AfterUpdate(Time);

		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		SceneManager.Draw();

		base.Draw(gameTime);
	}

	public void SetWindowSize(int ww, int wh)
	{
		Graphics.PreferredBackBufferWidth = ww;
		Graphics.PreferredBackBufferHeight = wh;
		Graphics.ApplyChanges();
	}

	public void SetWindowFullscreen(bool isfullscreen)
	{
		Graphics.IsFullScreen = isfullscreen;
		Graphics.ApplyChanges();
	}
}

