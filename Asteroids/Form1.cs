using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroids
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        Ship ship;
        List<asteroid> asts;
        List<Shot> shots;
        Random r;
        int density;
        bool ShotCheck = true, hySpace = true;
        bool restart = false, controlCheck = false, deleteCheck = false;
        Graphics g;
        Bitmap b;
        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Location = new Point(100, 50);
            b = new Bitmap(Width, Height);
            g = Graphics.FromImage(b);

            density = Width * Height / 30000;
            r = new Random();

            asts = new List<asteroid>(); //creates list of asteroids
            shots = new List<Shot>(); //creates list of shots
            
            

            pictureBox1.Size = Size;
            pictureBox1.Location = new Point(0, 0);
            

            ship = new Ship(new Point(pictureBox1.Width / 2, pictureBox1.Height / 2)); // creates new ship 
            

            label1.BackColor = Color.FromArgb(255, Color.Black);
            label1.Text = Convert.ToString(ship.score);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 20);
            
            

            for (int i = 0; i < density; i++) // checks to see that np new asteroids instantly kill the ship
            {

                int x = r.Next(pictureBox1.Width);
                int y = r.Next(pictureBox1.Height);
                while (Math.Pow(x-ship.top.X,2) + Math.Pow(y-ship.top.Y,2) < 5000)
                {
                    x = r.Next(pictureBox1.Width);
                    y = r.Next(pictureBox1.Height);
                }
                asts.Add(new asteroid(x, y,r.Next(100),40,pictureBox1.Size,ship.score));

            }

            
            
            timer1.Interval = 25;
            timer1.Start();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            const int margin = 30;

            g.Clear(Color.Black);
            for (int i = 0; i<asts.Count; i++)
            {
                restart = (Collision.collide(ship, asts[i], Width, Height)) ? true : restart; //Checks each asteroid hasn't collided with the ship //REMOVE AND FALSE
                
                    
                    
                
                asts[i].update(g); 
                if (asts[i].centre.X+ asts[i].rad*1.5 < 0 || asts[i].centre.Y - asts[i].rad * 1.5 > pictureBox1.Height || asts[i].centre.Y + asts[i].rad * 1.5 < 0 || asts[i].centre.X- asts[i].rad * 1.5 > pictureBox1.Width) //removes offscreen asteroids and creates new ones in their place
                {
                    
                    
                    if (asts.Count < density)
                    {
                        asts.Add(new asteroid(asts[i].centre.X, asts[i].centre.Y, r.Next(100), 40, pictureBox1.Size,ship.score));
                    }
                    asts.RemoveAt(i);
                }



            }


            for (int i = 0; i<shots.Count;i++) // checks if a shot is off screen and removes it if so
            {
                shots[i].Update(g);
                if (shots[i].yAccPos < 0 || shots[i].yAccPos >pictureBox1.Height|| shots[i].xAccPos < 0 || shots[i].xAccPos > pictureBox1.Width)
                {
                    shots.RemoveAt(i);
                }
                
            }

            

            for (int z = 0; z < shots.Count; z++)
                for (int i = 0; i < asts.Count; i++)
                {
                    if ( i == asts.Count)
                    {
                        break;
                    }
                    if (Collision.Hit(shots[z],asts[i])) //checks each shot against each asteroid
                    {
                        
                        if ((int)(asts[i].rad*0.66) > 10) // creates new two new asteroids
                        {
                            asts.Add(new asteroid(asts[i].centre.X, asts[i].centre.Y, r.Next(100), (int)(asts[i].rad * 0.66),pictureBox1.Size, ship.score));
                            asts.Add(new asteroid(asts[i].centre.X, asts[i].centre.Y, r.Next(100), (int)(asts[i].rad * 0.66), pictureBox1.Size,ship.score));
                        }

                        ship.score += asts[i].rad * 5; 
                        label1.Text = Convert.ToString(ship.score);


                        asts.RemoveAt(i);
                        shots.RemoveAt(z);
                    }
                    if (z == shots.Count)
                    {
                        break;
                    }
                }

            if (ship.centre.X > Width + margin)
            {
                ship.setTop(new Point(ship.top.X - Width-margin, ship.top.Y));
                ship.setVertex2(new Point(ship.vertex2.X - Width - margin, ship.vertex2.Y));
                ship.setVertex3(new Point(ship.vertex3.X - Width - margin, ship.vertex3.Y));
                ship.setUnrotatedPosition(new Point(ship.position.X - Width - margin, ship.position.Y));
            }
            if (ship.centre.X < -margin)
            {
                ship.setTop(new Point(ship.top.X + Width, ship.top.Y));
                ship.setVertex2(new Point(ship.vertex2.X + Width + margin, ship.vertex2.Y));
                ship.setVertex3(new Point(ship.vertex3.X + Width + margin, ship.vertex3.Y));
                ship.setUnrotatedPosition(new Point(ship.position.X + Width + margin, ship.position.Y));
            }
            if (ship.centre.Y > Height + margin)
            {
                ship.setTop(new Point(ship.top.X, ship.top.Y - Height - margin));
                ship.setVertex2(new Point(ship.vertex2.X, ship.vertex2.Y - Height - margin));
                ship.setVertex3(new Point(ship.vertex3.X, ship.vertex3.Y - Height - margin));
                ship.setUnrotatedPosition(new Point(ship.position.X, ship.position.Y - Height - margin));
            }
            if (ship.centre.Y < -margin)
            {
                ship.setTop(new Point(ship.top.X, ship.top.Y + Height + margin));
                ship.setVertex2(new Point(ship.vertex2.X, ship.vertex2.Y + Height + margin));
                ship.setVertex3(new Point(ship.vertex3.X, ship.vertex3.Y + Height + margin));
                ship.setUnrotatedPosition(new Point(ship.position.X, ship.position.Y + Height + margin));
            }
            ship.Update(g);
            pictureBox1.Image = b;
            if(controlCheck && deleteCheck)
            {
                Application.Exit();
            }

            if (restart)
            {
                timer1.Stop();
                DialogResult yesOrno = MessageBox.Show("Game Over, restart? ", "Game Over", MessageBoxButtons.YesNo);
                if (yesOrno == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                ship.lRotate = true;

            }
            if (e.KeyCode == Keys.D)
            {
                ship.rRotate = true;
            }
            if (e.KeyCode == Keys.J)
            {
                ship.up = true;
            }
            
            if (e.KeyCode == Keys.K && ShotCheck)
            {
                
                shots.Add(new Shot(ship.top.X, ship.top.Y, ship.rotation));
                ShotCheck = false;
            }
            if (e.KeyCode == Keys.Space && hySpace)
            {
                ship.hypspace(Width, Height);
                hySpace = false;
            }
            if(e.KeyCode == Keys.ControlKey)
            {
                controlCheck = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                deleteCheck = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                ship.lRotate = false;

            }
            if (e.KeyCode == Keys.D)
            {
                ship.rRotate = false; 
            }
            if (e.KeyCode == Keys.J)
            {
                ship.speedReset();
                ship.up = false;
            }
            
            if (e.KeyCode == Keys.K)
            {
                ShotCheck = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                hySpace = true;
            }
            if(e.KeyCode == Keys.Escape)
            {
                timer1.Enabled = !timer1.Enabled;
            }

            if (e.KeyCode == Keys.ControlKey)
            {
                controlCheck = false;
            }
            if (e.KeyCode == Keys.Delete)
            {
                deleteCheck = false;
            }
        }
    }
}
