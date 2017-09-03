using FavEdge.CnTPieMenu;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DemoApp02
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

        private void LayoutRoot_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            if (!checkBox1.IsChecked.Value) return;
            e.Handled = true;
            CnTPieMenu menu = new CnTPieMenu();

            if (checkBox2.IsChecked.Value)
            {
                menu.Range = 320;

                CnTPieMenuItem item1 = new CnTPieMenuItem();
                item1.labelText = "Item 1";
                item1.Ratio = 2;
                item1.Fired += new EventHandler(item_Fired);
                menu.items.Add(item1);

                CnTPieMenuItem item2 = new CnTPieMenuItem();
                item2.labelText = "Item 2";
                item2.Fired += new EventHandler(item_Fired);
                menu.items.Add(item2);

                CnTPieMenuItem item3 = new CnTPieMenuItem();
                item3.isSpacer = true;
                item3.Fired += new EventHandler(item_Fired);
                menu.items.Add(item3);

                CnTPieMenuItem item4 = new CnTPieMenuItem();
                item4.labelText = "Item 4";
                item4.Fired += new EventHandler(item_Fired);
                item4.isEnabled = false;
                menu.items.Add(item4);

                CnTPieMenuItem item5 = new CnTPieMenuItem();
                item5.labelText = "Item 5";
                item5.requiresClick = true;
                item5.Fired += new EventHandler(item_Fired);
                item5.InnerRadius = 64;
                item5.OuterRadius = 240;
                item5.fontWeight = FontWeights.Bold;
                item5.backgroundBrush = new SolidColorBrush(Colors.Pink);
                menu.items.Add(item5);
            }
            else
            {
                CnTPieMenuItem item1 = new CnTPieMenuItem();
                item1.labelText = "Item 1";
                item1.Fired += new EventHandler(item_Fired);
                menu.items.Add(item1);

                CnTPieMenuItem item2 = new CnTPieMenuItem();
                item2.labelText = "Item 2";
                item2.Fired += new EventHandler(item_Fired);
                menu.items.Add(item2);

                CnTPieMenuItem item3 = new CnTPieMenuItem();
                item3.labelText = "Item 3";
                item3.Fired += new EventHandler(item_Fired);
                menu.items.Add(item3);

                CnTPieMenuItem separator = new CnTPieMenuItem();
                separator.isSpacer = true;
                separator.Ratio = .5;
                menu.items.Add(separator);

                CnTPieMenuItem item4 = new CnTPieMenuItem();
                item4.labelText = "Item 4";
                item4.Fired += new EventHandler(item_Fired);
                item4.isEnabled = false;
                menu.items.Add(item4);

                CnTPieMenuItem item5 = new CnTPieMenuItem();
                item5.labelText = "Item 5";
                item5.Fired += new EventHandler(item_Fired);
                menu.items.Add(item5);
            }
            menu.Show();
        }

        void item_Fired(object sender, EventArgs e)
        {
            MessageBox.Show(this, ((CnTPieMenuItem)sender).labelText + " selected.");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(((MenuItem)sender).Header.ToString() + " selected.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Activate();
        }

    }
}
