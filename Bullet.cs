using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Zombie_shooter
{
    public class Bullet
    {
        public string direction;
        public double bulletLeft;
        public double bulletTop;

        private int speed = 20;
        private Ellipse bullet = new Ellipse();
        private DispatcherTimer bulletTimer = new DispatcherTimer(DispatcherPriority.Render);

        public void MakeBullet(Canvas canvas)
        {
            bullet.Fill = Brushes.White;
            bullet.Height = 5; bullet.Width = 5;
            bullet.Tag = "bullet";
            Canvas.SetLeft(bullet, bulletLeft);
            Canvas.SetTop(bullet, bulletTop);
            Canvas.SetZIndex(bullet, 2);
            canvas.Children.Add(bullet);

            bulletTimer.Interval = new TimeSpan(0, 0, 0, 0, 5);
            bulletTimer.Tick += BulletTimer_Tick;
            bulletTimer.Start();
        }

        private void BulletTimer_Tick(object sender, EventArgs e)
        {
            if (direction == "left")
            {
                Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) - speed);
            }
            if (direction == "right")
            {
                Canvas.SetLeft(bullet, Canvas.GetLeft(bullet) + speed);
            }
            if (direction == "up")
            {
                Canvas.SetTop(bullet, Canvas.GetTop(bullet) - speed);
            }
            if (direction == "down")
            {
                Canvas.SetTop(bullet, Canvas.GetTop(bullet) + speed);
            }

            if (Canvas.GetLeft(bullet) < 10 || Canvas.GetLeft(bullet) > 920 || Canvas.GetTop(bullet) < 10 || Canvas.GetTop(bullet) > 620)
            {
                bulletTimer.Stop();
                bullet.IsEnabled = false;
                bullet.Fill = Brushes.Transparent;
                bullet = null;
            }
        }
    }
}
