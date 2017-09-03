using FavEdge.CnTPieMenu;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DemoApp01
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
            CnTPieMenu menu = new CnTPieMenu();
            for (int i = 0; i < 6; ++i)
            {
                CnTPieMenuItem item = new CnTPieMenuItem();
                item.labelText = "Item " + i.ToString();
                item.requiresClick = (clickOptionSlider.Value == 1);
                menu.items.Add(item);
            }
            menu.closingAnimation = (fadeOutOptionSlider.Value == 0 ? CnTPieMenuClosingAnimations.None : CnTPieMenuClosingAnimations.Fade);
            menu.closingAnimationDuration = (int)(fadeOutOptionSlider.Value * 1000);
            menu.selectedItemClosingAnimationDuration = (int)(fadeOutOptionSlider.Value * 1000 * (Math.Pow(2, selectedItemFadeOutOptionSlider.Value)));
            menu.Show();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Maximized)
            {
                WindowStyle = System.Windows.WindowStyle.None;
            }
            else
            {
                WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                WindowState = System.Windows.WindowState.Normal;
            }
        }
    }
}
