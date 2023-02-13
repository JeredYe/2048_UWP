using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace _2048_
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Map map;
        public Tile[,] tiles;
        private long Score = 0;
        private void btnInit()
        {
            tiles[0, 0].button = B01;
            tiles[0, 1].button = B02;
            tiles[0, 2].button = B03;
            tiles[0, 3].button = B04;
            tiles[1, 0].button = B11;
            tiles[1, 1].button = B12;
            tiles[1, 2].button = B13;  
            tiles[1, 3].button = B14;
            tiles[2, 0].button = B21;
            tiles[2, 1].button = B22;
            tiles[2, 2].button = B23;
            tiles[2, 3].button = B24;
            tiles[3, 0].button = B31;
            tiles[3, 1].button = B32;
            tiles[3, 2].button = B33;
            tiles[3, 3].button = B34;
        }
        public MainPage()
        {
            this.InitializeComponent();
            
            tiles = new Tile[4, 4];
            for(int i = 0;i < 4; i++)
                for(int j = 0;j < 4; j++)
                {
                    tiles[i, j] = new Tile();
                }
            btnInit();
            map = new Map(4, tiles);
            map.Initialize();
            Window.Current.CoreWindow.Dispatcher.AcceleratorKeyActivated += map.Dispatcher_AcceleratorKeyActivated;
            SharedData.textScore = tScore;
            SharedData.textStep = tStep;
        }


    }
}
