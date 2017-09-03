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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FavEdge.CnTPieMenu
{
    public class CnTPieMenuItem
    {
        public CnTPieMenuItem()
        {
        }

        public String labelText = "";
        public Image icon = null;
        private Double ratio = 1;
        public event EventHandler Fired = null;
        public Boolean isEnabled = true;
        public FontFamily fontFamily = null;
        public Double fontSize = 0;
        public FontStyle fontStyle;
        public TextDecorationCollection fontTextDecorations;
        public FontWeight fontWeight;
        public Brush backgroundBrush = null;
        public Brush foregroundBrush = null;
        public Brush borderBrush = null;
        private Double borderThickness = -1;
        public Brush selectedBackgroundBrush = null;
        public Brush selectedForegroundBrush = null;
        public Brush selectedBorderBrush = null;
        private Double selectedBorderThickness = -1;
        public Brush disabledBackgroundBrush = null;
        public Brush disabledForegroundBrush = null;
        public Brush disabledBorderBrush = null;
        private Double disabledBorderThickness = -1;
        public Boolean isSpacer = false;
        public Boolean isVisible = true;
        public Boolean requiresClick = false;
        private Double innerRadius = 0;
        private Double outerRadius = 0;
        public Key accessKey = Key.None;
        public Object Tag = null;

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

        public void OnFire()
        {
            if (Fired != null)
            {
                Fired(this, EventArgs.Empty);
            }
        }

        public Double Ratio
        {
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Ratio must be larger than 0.", "Ratio");
                }
                else
                {
                    ratio = value;
                }
            }
            get
            {
                return ratio;
            }
        }

        public Double InnerRadius
        {
            set
            {
                if (value < CnTPieMenu.MinimumInnerRadius)
                {
                    throw new ArgumentOutOfRangeException("InnerRadius cannot be smaller than " + CnTPieMenu.MinimumInnerRadius + ".", "InnerRadius");
                }
                else if (value >= (outerRadius != 0 ? outerRadius : CnTPieMenu.DefaultOuterRadius))
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
                if (!(value > CnTPieMenu.MinimumInnerRadius))
                {
                    throw new ArgumentOutOfRangeException("OuterRadius must be greater than " + CnTPieMenu.MinimumInnerRadius + ".", "OuterRadius");
                }
                else if (value <= (innerRadius != 0 ? innerRadius : CnTPieMenu.DefaultInnerRadius))
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
    }
}
