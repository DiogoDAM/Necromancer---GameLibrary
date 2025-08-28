using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Necromancer;

public class AnimatedSprite : Sprite 
{
	private AnimationFrame[] _frames;

	public AnimationFrame CurrentFrame { get; private set; }
	public int CurrentFrameIndex { get; private set; }
	public float CurrentFrameDuration { get { return CurrentFrame.Duration; } }
	private float _frameTime;
	
	public int FramesCount { get { return _frames.Length; } }

	public int SourceWidth { get; }
	public int SourceHeight { get; }

	public bool IsRunning { get; set; }
	public bool IsLooping { get; set; }

	public event Action AnimationEnded;

	public AnimatedSprite(Texture2D texture, Rectangle region, int x, int y, int sourcewidth, int sourceheight, int width, int height, bool isLooping=false, float defaultFrameDuration=0.1f) : base(texture, region)
	{
		int rows = height / sourceheight;
		int columns = width / sourcewidth;
		int count = rows * columns;

		SourceWidth = sourcewidth;
		SourceHeight = sourceheight;

		_frames = new AnimationFrame[count];

		for(int row=0; row<rows; row++)
		{
			for(int col=0; col<columns; col++)
			{
				_frames[row * columns + col] = new AnimationFrame(new Rectangle(x + (sourcewidth * col), y + (sourceheight * row), sourcewidth, sourceheight), defaultFrameDuration);
			}
		}

		_frameTime = _frames[0].Duration;
		IsLooping = isLooping;
	}

	public AnimatedSprite(Texture2D texture, Rectangle region, int x, int y, int sourcewidth, int sourceheight, int width, int height, bool isLooping=false, params float[] framesDuration) : base(texture, region)
	{
		int rows = height / sourceheight;
		int columns = width / sourcewidth;
		int count = rows * columns;

		SourceWidth = sourcewidth;
		SourceHeight = sourceheight;

		_frames = new AnimationFrame[count];

		for(int row=0; row<rows; row++)
		{
			for(int col=0; col<columns; col++)
			{
				_frames[row * columns + col] = new AnimationFrame(new Rectangle(x + (sourcewidth * col), y + (sourceheight * row), sourcewidth, sourceheight), framesDuration[row * columns + col]);
			}
		}

		_frameTime = _frames[0].Duration;
		IsLooping = isLooping;
	}

	public void Update(Time time)
	{
		if(IsRunning)
		{
			_frameTime -= time.DeltaTime;

			if(_frameTime <= 0)
			{
				if(CurrentFrameIndex+1 >= FramesCount )
				{
					if(IsLooping) CurrentFrameIndex = 0;
					else IsRunning = false;

					AnimationEnded?.Invoke();
				}
				else
				{
				    CurrentFrameIndex++;
				}

				CurrentFrame = _frames[CurrentFrameIndex];
				_frameTime = CurrentFrame.Duration;
			}
		}
	}

	public override void Draw()
	{
		NecroGame.SpriteBatch.Draw(Texture, Transform.GlobalPosition, CurrentFrame.Bounds, Color, Transform.GlobalRotation, Origin, Transform.GlobalScale, Flip, LayerDepth);
	}

	public void Play()
	{
		IsRunning = true;
	}

	public void Restart()
	{
		IsRunning = true;
		CurrentFrameIndex = 0;
	}

	public void Stop()
	{
		IsRunning = false;
	}

	protected override void Dispose(bool disposable)
	{
		if(disposable)
		{
			if(!Disposed)
			{
				Transform = null;
				AnimationEnded = null;
				_frames = null;
				Disposed = true;
			}
		}
	}
}
