﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Windows;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls.Primitives;

namespace _2048_
{

    public class Tile
    {

        public Button button;
        public long num;
        //public int x;
        //public int y;

        public Tile(Button btn,int num)
        {
            this.num = num;
            this.button = btn;
            //this.x = x;
            //this.y = y;
            //this.Resize(Width, Height);
        }
        public Tile()
        {
            this.num = 0;
            this.button = null;
        }
        public void Initialize()
        {
            this.button.Content = "";
            this.button.IsEnabled = true;
        }

        //public void Resize(int Width, int Height)
        //{
        //    int num = ((this.x - 1) / 3) * 10;
        //    int num2 = ((this.y - 1) / 3) * 10;
        //    this.button.Left = ((Width / 500) + ((this.x - 1) * (Width / 5)));
        //    this.button.Top = ((Height / 500) + ((this.y - 1) * (Height / 5)));
        //    this.button.Width = Width / 5;
        //    this.button.Height = Height / 5;
        //    this.button.Font = new Font("微软雅黑", (float)(this.button.Width / 3), FontStyle.Bold);
        //}
    }
    public class Map
    {
        public Tile[,] TilesPtr;
        public int Size;
        public long[,] Matrix;
        public long score;
        public int step;
        public Map(int size, Tile[,] tiles)
        {
            this.Size = size;
            TilesPtr = tiles;
            Matrix = new long[size, size];
            score= 0;
            step = 0;
        }
        bool Victory = false;
        
        bool JudgeLose()
        {
            for (int j = 0; j < Size; j++)
                for (int i = 1; i < Size; i++)
                    if (Matrix[i, j] == 0) return false;

            for (int j = 0; j < Size; j++)
            {
                for (int i = 1; i < Size; i++)
                {
                    int k = i;
                    if (k >= 1 && (Matrix[k - 1, j] == Matrix[k, j] || Matrix[k - 1, j] == 0))
                    {
                        return false;
                    }
                }
            }

            for (int j = 0; j < Size; j++)
            {
                for (int i = 1; i < Size; i++)
                {
                    int k = i;
                    if (k >= 1 && (Matrix[j, k - 1] == Matrix[j, k] || Matrix[j, k - 1] == 0))
                    {
                        return false;
                    }
                }
            }

            for (int j = 0; j < Size; j++)
            {
                for (int i = Size - 2; i >= 0; i--)
                {
                    int k = i;
                    if (k <= Size - 2 && (Matrix[k + 1, j] == Matrix[k, j] || Matrix[k + 1, j] == 0))
                    {
                        return false;
                    }
                }
            }

            for (int j = 0; j < Size; j++)
            {
                for (int i = Size - 2; i >= 0; i--)
                {
                    int k = i;
                    if (k <= Size - 2 && (Matrix[j, k + 1] == Matrix[j, k] || Matrix[j, k + 1] == 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        public void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs args)
        {
            long score = 0;
            int Moved = 0;
            bool[] trig = new bool[4];
            if (args.EventType.ToString().Contains("KeyDown"))
            {
                VirtualKey virtualKey = args.VirtualKey;

                switch (virtualKey)
                {

                    case VirtualKey.Escape:
                        {
                            Matrix[0, 0] *= 2;
                            break;
                        }
                    case VirtualKey.Up:
                    case VirtualKey.W:
                        {
                            for (int j = 0; j < Size; j++)//x
                            {
                                for (int i = 1; i < Size; i++)//y
                                {
                                    int k = i;
                                    while (k >= 1 && ((Matrix[k - 1, j] == Matrix[k, j] && Matrix[k, j] != 0) || Matrix[k - 1, j] == 0))
                                    {
                                        if (Matrix[k - 1, j] == 0)
                                        {
                                            if (Matrix[k, j] != 0) Moved = 1;
                                            Matrix[k - 1, j] += Matrix[k, j];
                                            Matrix[k, j] = 0;
                                        }
                                        if(k >= 1 && (Matrix[k - 1, j] == Matrix[k, j])&& Matrix[k, j]!=0)
                                        {
                                            if (!trig[j])
                                            {
                                                Moved = 2;
                                                trig[j] = true;
                                                Matrix[k - 1, j] += Matrix[k, j];
                                                Matrix[k, j] = 0;
                                                score += Matrix[k - 1, j] / Size * (Victory ? 2 : 1);
                                            }
                                        }
                                        Moved = Moved > 0 ? Moved : 0;

                                        k--;
                                    }
                                }
                            }
                            break;
                        }
                    case VirtualKey.Down:
                    case VirtualKey.S:
                        {
                            for (int j = 0; j < Size; j++)
                            {
                                for (int i = Size - 2; i >= 0; i--)
                                {
                                    int k = i;
                                    while (k <= Size - 2 && (((Matrix[k + 1, j] == Matrix[k, j]) && Matrix[k, j] != 0) || Matrix[k + 1, j] == 0))
                                    {
                                        if (Matrix[k + 1, j] == 0) 
                                        {
                                            if (Matrix[k, j] != 0) Moved = 1;
                                            Matrix[k + 1, j] += Matrix[k, j];
                                            Matrix[k, j] = 0;
                                        }
                                        if (k <= Size - 2 && (Matrix[k + 1, j] == Matrix[k, j])&& Matrix[k, j]!=0)
                                        {
                                            if (!trig[j])
                                            {
                                                Moved = 2;
                                                trig[j] = true;
                                                Matrix[k + 1, j] += Matrix[k, j];
                                                Matrix[k, j] = 0;
                                                score += Matrix[k + 1, j] / Size * (Victory ? 2 : 1);
                                            }
                                        }
                                        Moved = Moved > 0 ? Moved : 0;
                                        k++;
                                    }
                                }
                            }
                            break;
                        }
                    case VirtualKey.Left:
                    case VirtualKey.A:
                        {
                            for (int j = 0; j < Size; j++)
                            {
                                for (int i = 1; i < Size; i++)
                                {
                                    int k = i;
                                    while (k >= 1 && ((Matrix[j, k - 1] == Matrix[j, k] && Matrix[j, k] != 0) || Matrix[j, k - 1] == 0))
                                    {
                                        if (Matrix[j, k - 1] == 0)
                                        {
                                            if (Matrix[k, j] != 0)
                                                Moved = 1;
                                            Matrix[j, k - 1] += Matrix[j, k];
                                            Matrix[j, k] = 0;
                                        }
                                        if(k >= 1 && (Matrix[j, k - 1] == Matrix[j, k])&& Matrix[j, k]!=0)
                                        {
                                            if (!trig[j])
                                            {
                                                Moved = 2;
                                                trig[j] = true;
                                                Matrix[j, k - 1] += Matrix[j, k];
                                                Matrix[j, k] = 0;
                                                score += Matrix[j, k - 1] / Size * (Victory ? 2 : 1);
                                            }
                                        }
                                        Moved = Moved > 0 ? Moved : 0;
                                        k--;
                                    }
                                }
                            }
                            break;
                        }
                    case VirtualKey.Right:
                    case VirtualKey.D:
                        {
                            for (int j = 0; j < Size; j++)
                            {
                                for (int i = Size - 2; i >= 0; i--)
                                {
                                    int k = i;
                                    while (k <= Size - 2 && ((Matrix[j, k + 1] == Matrix[j, k] && Matrix[j, k] != 0) || Matrix[j, k + 1] == 0))
                                    {
                                        if (Matrix[j, k + 1] == 0)
                                        {
                                            if (Matrix[k, j] != 0)
                                                Moved = 1;
                                            Matrix[j, k + 1] += Matrix[j, k];
                                            Matrix[j, k] = 0;
                                        }
                                        if(k <= Size - 2 && (Matrix[j, k + 1] == Matrix[j, k]) && Matrix[j, k] != 0) {
                                            if (!trig[j])
                                            {
                                                Moved = 2;
                                                trig[j] = true;
                                                Matrix[j, k + 1] += Matrix[j, k];
                                                Matrix[j, k] = 0;
                                                score += Matrix[j, k + 1] / Size * (Victory ? 2 : 1);
                                            }
                                        }
                                        Moved = Moved > 0 ? Moved : 0;

                                        k++;
                                    }
                                }
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                //_ = new MessageDialog(Convert.ToString(Moved), "提示").ShowAsync();
                step++;
                NewNum(Moved);
            }
            this.score += score;
            SharedData.textScore.Text = "分数：" + Convert.ToString(this.score);
            SharedData.textStep.Text ="步数："+Convert.ToString(step);
            Render();
            for (int j = 0; j < Size && !Victory; j++)
                for (int i = 0; i < Size && !Victory; i++)
                {
                    if (Matrix[j, i] >= 2048)
                    {
                        //MainPage mainPage = (MainPage)sender;
                        //MessageBox.Show("恭喜！你达成了2048！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _ = new MessageDialog("恭喜！你达成了2048！","提示").ShowAsync();
                        Victory = true;
                    }
                }
            if (JudgeLose())
            {
                //MessageBox.Show("你已经无处可走！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ = new MessageDialog("你已经无处可走！", "提示").ShowAsync();
                //ContentDialog contentDialog= new ContentDialog();
                //TextBox textBox = new TextBox();
                
            }
            
        }
        
        public void Initialize()
        {
            bool New = false;
            while (!(New = NewNum(2))) ;
            Render();
        }
        private Color getColor(long num)
        {
            if (num == 0)
            {
                return Color.FromArgb(255, 220, 220, 220);
            }
            int index = 0;
            while ((num /= 2) > 0) index++;
            if (index <= 12)
                return Color.FromArgb(255,255, 255, (byte)((index * 20 <= 255) ? 255 - index * 20 : 0));
            else
                return Color.FromArgb(255,0, 0, (byte)((index * 5 <= 255) ? 255 - index * 5 : 0));
        }
        private void Render()
        {
            for (int i = 0; i < TilesPtr.GetLength(0); i++)
            {
                for (int j = 0; j < TilesPtr.GetLength(1); j++)
                {
                    TilesPtr[i, j].num = Matrix[i, j];
                    //TilesPtr[i, j].button.BackColor = getColor(Matrix[i, j]);
                    Brush brush = new SolidColorBrush(getColor(Matrix[i, j]));
                    TilesPtr[i, j].button.Background = brush;
                    double fontsize = 4*(Matrix[i, j] < 10 ? 40 : (Matrix[i, j] < 100 ? 30 : (Matrix[i, j] < 1000 ? 25 : 15)));
                    TilesPtr[i, j].button.FontSize= fontsize;
                    TilesPtr[i, j].button.Content = Matrix[i, j] != 0 ? Convert.ToString(Matrix[i, j]) : "";
                }
            }
        }
        private bool P(int SuccessPercent)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int num = random.Next(1, 100);
            return num <= SuccessPercent;
        }
        private bool NewNum(int Status)
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int times = 0;
            if (Status == 1) times = (P(75) ? 1 : 0);
            else if (Status == 2) times = (P(50) ? 1 : 0) + (P(25) ? 1 : 0);
            else if (Status == 0) times = 0;
            for (int i = 0; i < times; i++)
            {
                int newNum=(random.Next(0,2)==0)?2:4;
                int x=random.Next(0,Size), y=random.Next(0,Size);
                int TrialTimes = 0;
                while (Matrix[y,x] != 0&&TrialTimes++<=2)
                {
                    x = random.Next(0, Size);
                    y = random.Next(0, Size);
                }
                if(Matrix[y, x] == 0) Matrix[y, x] = newNum;
            }
            return times > 0;
        }

    }
}
