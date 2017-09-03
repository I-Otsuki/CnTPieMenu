using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.Threading;

namespace FavEdge.CnTPieMenu
{
    public class CnTPieMenu
    {
        public const Double DefaultInnerRadius = 32;
        public const Double DefaultOuterRadius = 150;
        public const Double MinimumInnerRadius = 5;
        public const Double MinimumRange = 30;
        public const Double MaximumRange = 360;

        public CnTPieMenu()
        {
        }

        public Double startAngle = 0;
        private Double range = 360;
        public List<CnTPieMenuItem> items = new List<CnTPieMenuItem>();
        public Boolean enabled = true;
        public CnTPieMenuMenuItemDisplayStyles displayStyle = CnTPieMenuMenuItemDisplayStyles.IconAndText;
        public FontFamily fontFamily = SystemFonts.MenuFontFamily;
        public Double fontSize = SystemFonts.MenuFontSize;
        public FontStyle fontStyle = SystemFonts.MenuFontStyle;
        public TextDecorationCollection fontTextDecorations = SystemFonts.MenuFontTextDecorations;
        public FontWeight fontWeight = SystemFonts.MenuFontWeight;
        public Brush backgroundBrush = SystemColors.MenuBrush;
        public Brush foregroundBrush = SystemColors.MenuTextBrush;
        public Brush borderBrush = SystemColors.ActiveBorderBrush;
        private Double borderThickness = 1;
        public Brush selectedBackgroundBrush = SystemColors.MenuHighlightBrush;
        public Brush selectedForegroundBrush = SystemColors.HighlightTextBrush;
        public Brush selectedBorderBrush = SystemColors.ActiveBorderBrush;
        private Double selectedBorderThickness = 1;
        public Brush disabledBackgroundBrush = SystemColors.MenuBrush;
        public Brush disabledForegroundBrush = new SolidColorBrush(Color.FromArgb(0x80, SystemColors.MenuTextColor.R, SystemColors.MenuTextColor.G, SystemColors.MenuTextColor.B));
        public Brush disabledBorderBrush = SystemColors.ActiveBorderBrush;
        private Double disabledBorderThickness = 1;
        private Double innerRadius = DefaultInnerRadius;
        private Double outerRadius = DefaultOuterRadius;
        private Double gapWidth = 9;
        private String cancelLabelText = "Cancel";
        public FontFamily cancelLabelFontFamily = null;
        public Double cancelLabelFontSize = 0;
        public FontStyle cancelLabelFontStyle;
        public TextDecorationCollection cancelLabelFontTextDecorations = null;
        public FontWeight cancelLabelFontWeight;
        public Brush cancelCircleBackgroundBrush = new RadialGradientBrush(SystemColors.ControlBrush.Color, Color.FromArgb(0x80, SystemColors.ControlBrush.Color.R, SystemColors.ControlBrush.Color.G, SystemColors.ControlBrush.Color.B));
        public Brush cancelCircleForegroundBrush = SystemColors.ControlTextBrush;
        public Brush cancelCircleBorderBrush = new SolidColorBrush(Colors.Transparent);
        private Double cancelCircleBorderThickness = 0;
        private Double cancelCircleRadius = DefaultOuterRadius + 10;
        public CnTPieMenuOpeningAnimations openingAnimation = CnTPieMenuOpeningAnimations.Fade;
        public int openingAnimationDuration = 200;
        public CnTPieMenuClosingAnimations closingAnimation = CnTPieMenuClosingAnimations.Fade;
        public int closingAnimationDuration = 200;
        public int selectedItemClosingAnimationDuration = 400;
        public event EventHandler Opening;
        public event EventHandler Opened;
        public event EventHandler Cancelled;
        private Window pieWindow;
        private Boolean windowLocked = true;
        public Boolean CloseOnMouseLeave = true;

        public Double CancelCircleBorderThickness
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("CancelCircleBorderThickness can't be negative.", "CancelCircleBorderThickness");
                }
                else
                {
                    cancelCircleBorderThickness = value;
                }
            }
            get
            {
                return cancelCircleBorderThickness;
            }
        }

        public Double DisabledBorderThickness
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("DisabledBorderThickness can't be negative.", "DisabledBorderThickness");
                }
                else
                {
                    disabledBorderThickness = value;
                }
            }
            get
            {
                return disabledBorderThickness;
            }
        }

        public Double SelectedBorderThickness
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("SelectedBorderThickness can't be negative.", "SelectedBorderThickness");
                }
                else
                {
                    selectedBorderThickness = value;
                }
            }
            get
            {
                return selectedBorderThickness;
            }
        }

        public Double BorderThickness
        {
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("BorderThickness can't be negative.", "BorderThickness");
                }
                else
                {
                    borderThickness = value;
                }
            }
            get
            {
                return borderThickness;
            }
        }

        public String CancelLabelText
        {
            set
            {
                if (value == null || value == "")
                {
                    cancelLabelText = "Cancel";
                }
                else
                {
                    cancelLabelText = value;
                }
            }
            get
            {
                return cancelLabelText;
            }
        }

        public Double Range
        {
            set
            {
                if (value < MinimumRange || value > MaximumRange)
                {
                    throw new ArgumentOutOfRangeException("Range must be in " + MinimumRange + " to " + MaximumRange, "Range");
                }
                range = value;
            }
            get
            {
                return range;
            }
        }

        public Double InnerRadius
        {
            set
            {
                if (value < MinimumInnerRadius)
                {
                    throw new ArgumentOutOfRangeException("InnerRadius cannot be smaller than " + MinimumInnerRadius + ".", "InnerRadius");
                }
                else if (value >= outerRadius)
                {
                    throw new ArgumentOutOfRangeException("InnerRadius must be smaller than OuterRadius");
                }
                else
                {
                    innerRadius = value;
                }
            }
            get
            {
                return innerRadius;
            }
        }

        public Double OuterRadius
        {
            set
            {
                if (!(value > MinimumInnerRadius))
                {
                    throw new ArgumentOutOfRangeException("OuterRadius must be greater than " + MinimumInnerRadius + ".", "OuterRadius");
                }
                else if (value <= innerRadius)
                {
                    throw new ArgumentOutOfRangeException("OuterRadius must be greater than InnerRadius");
                }
                else
                {
                    outerRadius = value;
                }
            }
            get
            {
                return outerRadius;
            }
        }

        public Double GapWidth
        {
            get
            {
                return gapWidth;
            }
            set
            {
                if (0 > value)
                {
                    throw new ArgumentOutOfRangeException("GapWidth cannot be negative.", "GapWidth");
                }
                gapWidth = value;
            }
        }

        public Double CancelCircleRadius
        {
            get
            {
                return cancelCircleRadius;
            }
            set
            {
                if (value <= GetLargestInnerRadius())
                {
                    throw new ArgumentOutOfRangeException("CancelCircleRadius must be larger than the largest inner radius.", "CancelCircleRadius");
                } cancelCircleRadius = value;
            }
        }

        public void Show()
        {
            if (!enabled)
            {
                return;
            }
            if (Opening != null)
            {
                Opening(this, new EventArgs());
            }
            if (GetCommandItemCount() == 0)
            {
                return;
            }
            Double windowSize = Math.Max(GetLargestOuterRadius(), cancelCircleRadius) * 2 + 10;
            Double windowCenter = windowSize * .5;
            pieWindow = new Window();
            pieWindow.WindowStyle = WindowStyle.None;
            pieWindow.ResizeMode = ResizeMode.NoResize;
            pieWindow.AllowsTransparency = true;
            pieWindow.Background = new SolidColorBrush(Color.FromArgb(1, 0x80, 0x80, 0x80));
            pieWindow.Width = windowSize;
            pieWindow.Height = windowSize;
            pieWindow.ShowInTaskbar = false;
            pieWindow.Icon = null;
            pieWindow.Topmost = true;
            pieWindow.Title = "";
            pieWindow.SourceInitialized += new EventHandler(pieWindow_SourceInitialized);
            pieWindow.PreviewKeyDown += new KeyEventHandler(pieWindow_PreviewKeyDown);
            pieWindow.LostFocus += new RoutedEventHandler(pieWindow_LostFocus);
            pieWindow.Deactivated += new EventHandler(pieWindow_Deactivated);
            Canvas layoutRoot = new Canvas();
            layoutRoot.Width = windowSize;
            layoutRoot.Height = windowSize;
            Canvas cancelCircleGroup = new Canvas();
            cancelCircleGroup.Width = windowSize;
            cancelCircleGroup.Height = windowSize;
            Ellipse cancelCircle = new Ellipse();
            cancelCircle.Height = CancelCircleRadius * 2;
            cancelCircle.Width = CancelCircleRadius * 2;
            cancelCircle.Fill = cancelCircleBackgroundBrush;
            cancelCircle.Stroke = cancelCircleBorderBrush;
            cancelCircle.StrokeThickness = CancelCircleBorderThickness;
            cancelCircle.RenderTransform = new TranslateTransform(windowCenter - CancelCircleRadius, windowCenter - CancelCircleRadius);
            layoutRoot.MouseLeave += new MouseEventHandler(cancelCircle_MouseLeave);
            cancelCircleGroup.MouseDown += new MouseButtonEventHandler(cancelCircleGroup_MouseDown);
            cancelCircleGroup.Children.Add(cancelCircle);
            TextBlock cancelCaption = new TextBlock();
            cancelCaption.Text = CancelLabelText;
            cancelCaption.TextAlignment = TextAlignment.Center;
            cancelCaption.Foreground = cancelCircleForegroundBrush;
            cancelCaption.FontFamily = (cancelLabelFontFamily != null ? cancelLabelFontFamily : fontFamily);
            cancelCaption.FontSize = (cancelLabelFontSize != 0 ? cancelLabelFontSize : fontSize);
            cancelCaption.FontStyle = (cancelLabelFontStyle != null ? cancelLabelFontStyle : fontStyle);
            cancelCaption.TextDecorations = (cancelLabelFontTextDecorations != null ? cancelLabelFontTextDecorations : fontTextDecorations);
            cancelCaption.FontWeight = (cancelLabelFontWeight != null ? cancelLabelFontWeight : fontWeight);
            cancelCaption.Measure(new Size(windowSize, windowSize));
            cancelCaption.RenderTransform = new TranslateTransform(windowCenter - cancelCaption.DesiredSize.Width * .5, windowCenter - cancelCaption.DesiredSize.Height * .5);
            cancelCircleGroup.Children.Add(cancelCaption);
            layoutRoot.Children.Add(cancelCircleGroup);
            Double total = GetVisibleItemTotalRatio();
            Double cumulRatio = 0;
            foreach (CnTPieMenuItem item in items)
            {
                if (!item.isVisible)
                {
                    continue;
                }
                Double itemStartAngle = Deg2Rad(startAngle + cumulRatio * Range / total);
                cumulRatio += item.Ratio;
                Double itemEndAngle = Deg2Rad(startAngle + cumulRatio * Range / total);
                if (item.isSpacer)
                {
                    continue;
                }
                Double itemInnerRadius = (item.InnerRadius != 0 ? item.InnerRadius : InnerRadius);
                Double itemOuterRadius = (item.OuterRadius != 0 ? item.OuterRadius : OuterRadius);
                Canvas itemCanvas = new Canvas();
                itemCanvas.Width = windowSize;
                itemCanvas.Height = windowSize;
                Path path = new Path();
                path.Width = windowSize;
                path.Height = windowSize;
                Double innerGapAngle = Math.Asin(GapWidth * .5 / itemInnerRadius);
                Double outerGapAngle = Math.Asin(GapWidth * .5 / itemOuterRadius);
                Point innerLeft = new Point(windowCenter - Math.Cos(itemStartAngle + innerGapAngle) * itemInnerRadius, windowCenter - Math.Sin(itemStartAngle + innerGapAngle) * itemInnerRadius);
                Point innerRight = new Point(windowCenter - Math.Cos(itemEndAngle - innerGapAngle) * itemInnerRadius, windowCenter - Math.Sin(itemEndAngle - innerGapAngle) * itemInnerRadius);
                Point outerLeft = new Point(windowCenter - Math.Cos(itemStartAngle + outerGapAngle) * itemOuterRadius, windowCenter - Math.Sin(itemStartAngle + outerGapAngle) * itemOuterRadius);
                Point outerRight = new Point(windowCenter - Math.Cos(itemEndAngle - outerGapAngle) * itemOuterRadius, windowCenter - Math.Sin(itemEndAngle - outerGapAngle) * itemOuterRadius);
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                ArcSegment innerArc = new ArcSegment();
                ArcSegment outerArc = new ArcSegment();
                LineSegment edge = new LineSegment();
                figure.StartPoint = innerLeft;
                figure.IsClosed = true;
                innerArc.Point = innerRight;
                innerArc.Size = new Size(itemInnerRadius, itemInnerRadius);
                innerArc.SweepDirection = SweepDirection.Clockwise;
                innerArc.IsLargeArc = (Rad2Deg(itemEndAngle - itemStartAngle - innerGapAngle * 2) > 180 ? true : false);
                edge.Point = (outerRight);
                outerArc.Point = outerLeft;
                outerArc.Size = new Size(itemOuterRadius, itemOuterRadius);
                outerArc.SweepDirection = SweepDirection.Counterclockwise;
                outerArc.IsLargeArc = (Rad2Deg(itemEndAngle - itemStartAngle - outerGapAngle * 2) > 180 ? true : false);
                figure.Segments.Add(innerArc);
                figure.Segments.Add(edge);
                figure.Segments.Add(outerArc);
                geometry.Figures.Add(figure);
                path.Data = geometry;
                path.Stroke = (item.isEnabled ? (item.borderBrush != null ? item.borderBrush : borderBrush) : (item.disabledBorderBrush != null ? item.disabledBorderBrush : disabledBorderBrush));
                path.StrokeThickness = (item.isEnabled ? (item.BorderThickness != -1 ? item.BorderThickness : BorderThickness) : (item.DisabledBorderThickness != -1 ? item.DisabledBorderThickness : DisabledBorderThickness));
                path.Fill = (item.isEnabled ? (item.backgroundBrush != null ? item.backgroundBrush : backgroundBrush) : (item.disabledBackgroundBrush != null ? item.disabledBackgroundBrush : disabledBackgroundBrush));
                itemCanvas.Children.Add(path);
                TextBlock itemLabel = new TextBlock();
                itemLabel.Text = item.labelText;
                itemLabel.TextAlignment = TextAlignment.Center;
                itemLabel.Foreground = (item.isEnabled ? (item.foregroundBrush != null ? item.foregroundBrush : foregroundBrush) : (item.disabledForegroundBrush != null ? item.disabledForegroundBrush : disabledForegroundBrush));
                itemLabel.FontFamily = (item.fontFamily != null ? item.fontFamily : fontFamily);
                itemLabel.FontSize = (item.fontSize != 0 ? item.fontSize : fontSize);
                itemLabel.FontStyle = (item.fontStyle != null ? item.fontStyle : fontStyle);
                itemLabel.TextDecorations = (item.fontTextDecorations != null ? item.fontTextDecorations : fontTextDecorations);
                itemLabel.FontWeight = (item.fontWeight != null ? item.fontWeight : fontWeight);
                itemLabel.Measure(new Size(windowSize, windowSize));
                switch (displayStyle)
                {
                    default:
                        Double itemLabelX = windowCenter - (Math.Cos((itemStartAngle + itemEndAngle) * .5) * (itemInnerRadius + itemOuterRadius) + itemLabel.DesiredSize.Width) * .5;
                        Double itemLabelY = windowCenter - (Math.Sin((itemStartAngle + itemEndAngle) * .5) * (itemInnerRadius + itemOuterRadius) + itemLabel.DesiredSize.Height) * .5;
                        itemLabel.RenderTransform = new TranslateTransform(itemLabelX, itemLabelY);
                        break;
                }
                itemCanvas.Children.Add(itemLabel);
                if (item.isEnabled)
                {
                    itemCanvas.Tag = item;
                    if (item.requiresClick)
                    {
                        itemCanvas.MouseEnter += new MouseEventHandler(itemCanvas_ClickRequired_MouseEnter);
                        itemCanvas.MouseLeave += new MouseEventHandler(itemCanvas_MouseLeave);
                        itemCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(itemCanvas_MouseLeftButtonDown);
                    }
                    else
                    {
                        itemCanvas.MouseEnter += new MouseEventHandler(itemCanvas_MouseEnter);
                    }
                }
                layoutRoot.Children.Add(itemCanvas);
            }
            pieWindow.Content = layoutRoot;

            System.Drawing.Point clickPos = System.Windows.Forms.Cursor.Position;
            clickPos.X = (int)Math.Max(Math.Min(clickPos.X, SystemParameters.VirtualScreenLeft + SystemParameters.VirtualScreenWidth - cancelCircleRadius), SystemParameters.VirtualScreenLeft + cancelCircleRadius);
            clickPos.Y = (int)Math.Max(Math.Min(clickPos.Y, SystemParameters.VirtualScreenTop + SystemParameters.VirtualScreenHeight - cancelCircleRadius), SystemParameters.VirtualScreenTop + cancelCircleRadius);
            pieWindow.Top = clickPos.Y - windowCenter;
            pieWindow.Left = clickPos.X - windowCenter;
            System.Windows.Forms.Cursor.Position = clickPos;
            DoubleAnimation fadeIn = new DoubleAnimation(0, 1, new Duration(new TimeSpan(0, 0, 0, 0, openingAnimationDuration)));
            fadeIn.AutoReverse = false;
            if (openingAnimation == CnTPieMenuOpeningAnimations.Fade)
            {
                pieWindow.Opacity = 0;
            }
            pieWindow.Show();
            pieWindow.Activate();
            if (Opened != null)
            {
                Opened(this, new EventArgs());
            }
            windowLocked = false;
            if (openingAnimation == CnTPieMenuOpeningAnimations.Fade)
            {
                pieWindow.BeginAnimation(Window.OpacityProperty, fadeIn);
            }

        }

        void itemCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (windowLocked)
            {
                return;
            }
            CnTPieMenuItem tag = ((CnTPieMenuItem)((Canvas)sender).Tag);
            foreach (Canvas canvas in ((Canvas)pieWindow.Content).Children)
            {
                if (canvas.Tag == tag)
                {
                    Path background = (Path)canvas.Children[0];
                    TextBlock label = (TextBlock)canvas.Children[1];
                    background.Fill = (tag.backgroundBrush != null ? tag.backgroundBrush : backgroundBrush);
                    background.Stroke = (tag.borderBrush != null ? tag.borderBrush : borderBrush);
                    background.StrokeThickness = (tag.BorderThickness != -1 ? tag.BorderThickness : BorderThickness);
                    label.Foreground = (tag.foregroundBrush != null ? tag.foregroundBrush : foregroundBrush);
                    break;
                }
            }
        }

        void itemCanvas_ClickRequired_MouseEnter(object sender, MouseEventArgs e)
        {
            if (windowLocked)
            {
                return;
            }
            CnTPieMenuItem tag = ((CnTPieMenuItem)((Canvas)sender).Tag);
            foreach (Canvas canvas in ((Canvas)pieWindow.Content).Children)
            {
                if (canvas.Tag == tag)
                {
                    Path background = (Path)canvas.Children[0];
                    TextBlock label = (TextBlock)canvas.Children[1];
                    background.Fill = (tag.selectedBackgroundBrush != null ? tag.selectedBackgroundBrush : selectedBackgroundBrush);
                    background.Stroke = (tag.selectedBorderBrush != null ? tag.selectedBorderBrush : selectedBorderBrush);
                    background.StrokeThickness = (tag.SelectedBorderThickness != -1 ? tag.SelectedBorderThickness : SelectedBorderThickness);
                    label.Foreground = (tag.selectedForegroundBrush != null ? tag.selectedForegroundBrush : selectedForegroundBrush);
                    break;
                }
            }
        }

        Double Deg2Rad(Double deg)
        {
            return deg * Math.PI / 180;
        }

        Double Rad2Deg(Double rad)
        {
            Double deg = 180 * rad / Math.PI;
            while (deg >= 360)
            {
                deg -= 360;
            }
            while (deg <= 0)
            {
                deg += 360;
            }
            return deg;
        }

        void itemCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (windowLocked)
            {
                return;
            }
            CnTPieMenuItem tag = ((CnTPieMenuItem)((Canvas)sender).Tag);
            FireAndClose(tag);
        }

        void itemCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (windowLocked)
            {
                return;
            }
            CnTPieMenuItem tag = ((CnTPieMenuItem)((Canvas)sender).Tag);
            FireAndClose(tag);
        }

        void FireAndClose(CnTPieMenuItem tag)
        {
            windowLocked = true;
            pieWindow.Background = new SolidColorBrush(Colors.Transparent);
            pieWindow.IsHitTestVisible = false;
            switch (closingAnimation)
            {
                case CnTPieMenuClosingAnimations.Fade:
                    Double AnimationStartOpacity = 1;
                    DoubleAnimation fadeOut = new DoubleAnimation(AnimationStartOpacity, 0, new Duration(new TimeSpan(0, 0, 0, 0, closingAnimationDuration)));
                    DoubleAnimation selectedItemFadeOut = new DoubleAnimation(AnimationStartOpacity, 0, new Duration(new TimeSpan(0, 0, 0, 0, selectedItemClosingAnimationDuration)));
                    fadeOut.AutoReverse = false;
                    selectedItemFadeOut.AutoReverse = false;
                    Boolean selectedAnimIsLonger = closingAnimationDuration <= selectedItemClosingAnimationDuration;
                    Boolean isSet = false;
                    foreach (Canvas canvas in ((Canvas)pieWindow.Content).Children)
                    {
                        if (canvas.Tag != tag)
                        {
                            if (!isSet && !selectedAnimIsLonger)
                            {
                                DoubleAnimation SpecialFadeOut = new DoubleAnimation(AnimationStartOpacity, 0, new Duration(new TimeSpan(0, 0, 0, 0, closingAnimationDuration)));
                                SpecialFadeOut.Completed += new EventHandler(selectedItemFadeOut_Completed);
                                canvas.BeginAnimation(Canvas.OpacityProperty, SpecialFadeOut);
                            }
                            else
                            {
                                canvas.BeginAnimation(Canvas.OpacityProperty, fadeOut);
                            }
                        }
                        else
                        {
                            Path background = (Path)canvas.Children[0];
                            TextBlock label = (TextBlock)canvas.Children[1];
                            background.Fill = (tag.selectedBackgroundBrush != null ? tag.selectedBackgroundBrush : selectedBackgroundBrush);
                            background.Stroke = (tag.selectedBorderBrush != null ? tag.selectedBorderBrush : selectedBorderBrush);
                            background.StrokeThickness = (tag.SelectedBorderThickness != -1 ? tag.SelectedBorderThickness : SelectedBorderThickness);
                            label.Foreground = (tag.selectedForegroundBrush != null ? tag.selectedForegroundBrush : selectedForegroundBrush);
                            if (selectedAnimIsLonger)
                            {
                                selectedItemFadeOut.Completed += new EventHandler(selectedItemFadeOut_Completed);
                            }
                            canvas.BeginAnimation(Canvas.OpacityProperty, selectedItemFadeOut);
                        }
                    }
                    break;
                default:
                    pieWindow.Close();
                    break;
            }
            tag.OnFire();
        }

        void selectedItemFadeOut_Completed(object sender, EventArgs e)
        {
            if (pieWindow.IsVisible)
            {
                pieWindow.Close();
            }
        }

        void cancelCircleGroup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CancelMenu();
        }

        int GetCommandItemCount()
        {
            int count = 0;
            foreach (CnTPieMenuItem item in items)
            {
                if (item.isVisible && !item.isSpacer)
                {
                    ++count;
                }
            }
            return count;
        }

        Double GetVisibleItemTotalRatio()
        {
            Double total = 0;
            foreach (CnTPieMenuItem item in items)
            {
                if (item.isVisible)
                {
                    total += item.Ratio;
                }
            }
            return total;
        }

        void pieWindow_SourceInitialized(object sender, EventArgs e)
        {
            WindowInteropHelper pieWindowInteropHelper = new WindowInteropHelper(pieWindow);
            pieWindowInteropHelper.EnsureHandle();
            HandleRef handleRefernce = new HandleRef(pieWindow, pieWindowInteropHelper.Handle);
            int extendedWindowStyle = User32.GetWindowLongPtr(handleRefernce, User32.GWL_EXSTYLE).ToInt32();
            extendedWindowStyle |= User32.WS_EX_TOOLWINDOW;
            User32.SetWindowLongPtr(handleRefernce, User32.GWL_EXSTYLE, new IntPtr(extendedWindowStyle));
        }

        void CancelMenu()
        {
            if (!pieWindow.IsVisible || windowLocked)
            {
                return;
            }
            windowLocked = true;
            pieWindow.Close();
            if (Cancelled != null)
            {
                Cancelled(pieWindow, new EventArgs());
            }
        }

        void cancelCircle_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!CloseOnMouseLeave)
            {
                return;
            }
            CancelMenu();
        }

        void pieWindow_Deactivated(object sender, EventArgs e)
        {
            CancelMenu();
        }

        void pieWindow_LostFocus(object sender, RoutedEventArgs e)
        {
            CancelMenu();
        }

        void pieWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!pieWindow.IsVisible)
            {
                return;
            }
            if (e.Key == Key.Escape)
            {
                CancelMenu();
                return;
            }
            foreach (CnTPieMenuItem item in items)
            {
                if (item.isVisible && item.isEnabled && item.accessKey != Key.None && item.accessKey == e.Key)
                {
                    item.OnFire();
                    return;
                }
            }
        }

        Double GetLargestOuterRadius()
        {
            Double largest = 0;
            foreach (CnTPieMenuItem item in items)
            {
                largest = Math.Max(
                    (item.OuterRadius != 0 ? item.OuterRadius : this.OuterRadius)
                    + Math.Max(
                    Math.Max(
                    (item.BorderThickness != -1 ? item.BorderThickness : BorderThickness),
                    (item.DisabledBorderThickness != -1 ? item.DisabledBorderThickness : DisabledBorderThickness)
                    ),
                    (item.SelectedBorderThickness != -1 ? item.SelectedBorderThickness : SelectedBorderThickness)
                    ),
                    largest);
            }
            return largest;
        }

        Double GetLargestInnerRadius()
        {
            Double largest = 0;
            foreach (CnTPieMenuItem item in items)
            {
                largest = Math.Max((item.InnerRadius != 0 ? item.InnerRadius : this.InnerRadius), largest);
            }
            return largest;
        }

    }
}
