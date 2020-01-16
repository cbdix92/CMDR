using System;
using System.Windows.Input;
using CMDR;


namespace Main
{
	static class MainClass
	{
		private static bool _isGameOver = false;

		private static double _speed;
		
		[STAThread]
		public static void Main(string[] args)
		{
			CMDR_.Init();
			
			Render.ShowDebug = true;
			
			Updater.RenderCap = 40;
			
			Scene test_Scene = new Scene();
			
			GameObject test_Object = test_Scene.AddGameObject();
			GameObject test_Object2 = test_Scene.AddGameObject();
			
			
			
			test_Object.AddComponet(new Image("XXXXXX\nX  X"));
			test_Object2.AddComponet(new Image("OOO\nOOO\nOOO"));
			
			test_Object2.transform.Teleport(10,10);

			test_Object.Collider = true;
			test_Object2.Collider = true;

			_speed = 5.5F;
			KeyListener.AddKeyBind(Key.W, () => {test_Object.transform.Yvel = -_speed;}, () => { test_Object.transform.Yvel = 0;  } );
			KeyListener.AddKeyBind(Key.A, () => {test_Object.transform.Xvel = -_speed;}, () => { test_Object.transform.Xvel = 0; } );
			KeyListener.AddKeyBind(Key.S, () => {test_Object.transform.Yvel = _speed;}, () => { test_Object.transform.Yvel = 0; } );
			KeyListener.AddKeyBind(Key.D, () => {test_Object.transform.Xvel = _speed;}, () => { test_Object.transform.Xvel = 0; } );
			KeyListener.AddKeyBind(Key.C, () => {MainClass._isGameOver = true;});
			
            // Game loop
			while (!_isGameOver)
			{
				
				CMDR_.Update();
				
			}
			
			
		}
	}
}