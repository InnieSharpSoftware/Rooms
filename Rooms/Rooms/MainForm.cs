/*
 * Создано в SharpDevelop.
 * Пользователь: inniesharp
 * Дата: 29.09.2022
 * Время: 14:56
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Text;

namespace Rooms
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		//public static int roomscount = 90;
		
		public static int xa; public static int ya; public static int stack;
		public static string[] map;
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Generate()
		{
			Random r = new Random();
			int ja = r.Next(1, 4);
			if (ja == 1) {
				map = new string[]
				{
					"WWWOWWW",
					"W.....W",
					"W.....W",
					"O..P..O",
					"W.....W",
					"W.....W",
					"WWWOWWW",
				};
			}
			if (ja == 2) {
				map = new string[]
				{
					"WWWWWWW",
					"W.....W",
					"W.....W",
					"W..P..W",
					"W.....W",
					"W.....W",
					"WWWOWWW",
				};
			}
			if (ja == 3) {
				map = new string[]
				{
					"WWWOWWW",
					"W.....W",
					"W.....W",
					"W..P..O",
					"W.....W",
					"W.....W",
					"WWWOWWW",
				};
			}
		}
		
		void Draw()
		{
			Dictionary<char, Image> images = new Dictionary<char, Image>()
			{
				{ 'W', Image.FromFile(@"textures\wall.png") },
			    { '.', Image.FromFile(@"textures\floor.png") },
			    { 'P', Image.FromFile(@"textures\player.png") },
			    { 'O', Image.FromFile(@"textures\floor.png") }
			};
			int y = 0;
			foreach (string str in map)
			{
			    int x = 0;
			    foreach (char c in str)
			    {
			        Controls.Add(new PictureBox
			        {
			            Image = images[c],
			            Size = new Size(32, 32),
			            Location = new Point(x * 32, y * 32)
			        });
			        ++x;
			    }
			    ++y;
			}
		}
		
		void Move(string direct)
		{
			if(direct == "left")
			{
				StringBuilder sb = new StringBuilder(map[ya]);
				bool a = false;
				if(xa > 0)
				{
					if(sb[xa - 1] == 'O' || sb[xa - 1] == 'W')
					{
						if(sb[xa - 1] == 'O')
						{
							xa = 3; ya = 3;
							Generate();
							Controls.Clear();
							Draw();
							a = true;
						}
					}
					else
					{
						sb[xa] = '.';
						sb[xa - 1] = 'P';
						xa -= 1;
					}
				}
				if(!a)
					map[ya] = sb.ToString();
			}
			if(direct == "right")
			{
				StringBuilder sb = new StringBuilder(map[ya]);
				bool a = false;
				if(xa < 6)
				{
					if(sb[xa + 1] == 'O' || sb[xa + 1] == 'W')
					{
						if(sb[xa + 1] == 'O')
						{
							xa = 3; ya = 3;
							Generate();
							Controls.Clear();
							Draw();
							a = true;
						}
					}
					else
					{
						sb[xa] = '.';
						sb[xa + 1] = 'P';
						xa += 1;
					}
				}
				if(!a)
					map[ya] = sb.ToString();
			}
			if(direct == "down")
			{
				if(ya < 6)
				{
					StringBuilder sb = new StringBuilder(map[ya]);
					StringBuilder sb2 = new StringBuilder(map[ya + 1]);
					bool a = false;
					if(sb2[xa] == 'O' || sb2[xa] == 'W')
					{
						if(sb2[xa] == 'O')
						{
							xa = 3; ya = 3;
							Generate();
							Controls.Clear();
							Draw();
							a = true;
						}
					}
					else
					{
						sb[xa] = '.';
						sb2[xa] = 'P';
					}
					if(!a)
					{
						map[ya] = sb.ToString();
						map[ya + 1] = sb2.ToString();
						ya += 1;
					}
				}
			}
			if(direct == "up")
			{
				if(ya > 0)
				{
					StringBuilder sb = new StringBuilder(map[ya]);
					StringBuilder sb2 = new StringBuilder(map[ya - 1]);
					bool a = false;
					if(sb2[xa] == 'O' || sb2[xa] == 'W')
					{
						if(sb2[xa] == 'O')
						{
							xa = 3; ya = 3;
							Generate();
							Controls.Clear();
						while 	Draw();
							a = true;
						}
					}
					else
					{
						sb[xa] = '.';
						sb2[xa] = 'P';
					}
					if(!a)
					{
						map[ya] = sb.ToString();
						map[ya - 1] = sb2.ToString();
						ya -= 1;
					}
				}
			}
		}
		
		void Timer1Tick(object sender, EventArgs e)
		{
			
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			xa = 3; ya = 3; stack = 0;
			Generate();
			Draw();
		}
		void MainFormKeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.A)
				Move("left");
			if(e.KeyCode == Keys.D)
				Move("right");
			if(e.KeyCode == Keys.W)
				Move("up");
			if(e.KeyCode == Keys.S)
				Move("down");
			this.Controls.Clear();
			Draw();
		}
	}
}
