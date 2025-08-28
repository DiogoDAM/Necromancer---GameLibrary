using System;
using System.Collections.Generic;

namespace Necromancer;

public sealed class SceneManager
{
	private Dictionary<string, Func<Scene>> _scenesFactory;

	private (Scene, bool) _nextScene;

	public Scene CurrentScene { get; private set; }
	public Scene LastScene { get; private set; }

	public SceneManager()
	{
		_scenesFactory = new();

	}

	public void Start() => CurrentScene.Start();

	public void BeforeUpdate(Time time) => CurrentScene?.BeforeUpdate(time);
	public void Update(Time time) => CurrentScene?.Update(time);
	public void AfterUpdate(Time time) => CurrentScene?.AfterUpdate(time);

	public void Draw() => CurrentScene?.Draw();


	public void AddScene(string sceneName, Func<Scene> factory)
	{
		if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("SceneManager sceneName is a string null or empty");

		_scenesFactory.Add(sceneName, factory);
	}

	public bool RemoveScene(string sceneName)
	{
		if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("SceneManager sceneName is a string null or empty");
		if(!_scenesFactory.ContainsKey(sceneName)) return false;

		return _scenesFactory.Remove(sceneName);
	}

	public void ChangeScene(string sceneName, bool currentDispose=false)
	{
		if(string.IsNullOrEmpty(sceneName)) throw new ArgumentNullException("SceneManager sceneName is a string null or empty");
		if(!_scenesFactory.ContainsKey(sceneName)) throw new KeyNotFoundException($"SceneManager doesn't have the scene: {sceneName}");

		_nextScene = (_scenesFactory[sceneName](), currentDispose);
	}

	private void ProcessChangeScene()
	{
		CurrentScene?.Unload();
		CurrentScene?.Desactive();
		LastScene = CurrentScene;

		CurrentScene = _nextScene.Item1;

		CurrentScene.Load();
		CurrentScene.Active();

		if(_nextScene.Item2) LastScene.Dispose();

		_nextScene.Item1 = null;
	}
}
