using Common;
using GameStateEngine.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Xml.Schema;
using TheBlackRoom.System.Helpers.IListHelpers;

namespace CrossfireRPG.GuiElements
{
    public class geListBox : geTextElement
    {
        private ListBoxWrapper<object> _listbox = new ListBoxWrapper<object>();
        
        public geListBox()
        {
            GetItemText = GetItemTextInternal;
            
            _listbox.ListItems = _Items;
        }

        public List<object> Items
        {
            get => _Items;
            set
            {
                if (_Items == value) return;
                _Items = value;

                _listbox.ListItems = _Items;
            }
        }
        private List<object> _Items = new List<object>();

        public float ItemHeight
        { 
            get => _ItemHeight;
            set
            {
                if (_ItemHeight == value) return;

                _ItemHeight = value;
                _listbox.MaximumItemCount = ElementBounds.Height / RealItemHeight;
                _listbox.OnListItemsChanged();
            }
        }
        private float _ItemHeight = 0;


        public ContentAlignment Alignment { get; set; } = ContentAlignment.MiddleLeft;
        public string FormatString { get; set; } = string.Empty;
        public bool OwnerDraw { get; set; } = false;

        public void NotifyListItemsChanged() => _listbox.OnListItemsChanged();

        protected override void OnFontChanged()
        {
            base.OnFontChanged();

            _FontHeight = (Font == null) ? 0 : Font.MeasureString("Wy").Y;

            _listbox.MaximumItemCount = ElementBounds.Height / RealItemHeight;
        }
        
        float _FontHeight = 0;

        protected float RealItemHeight => (ItemHeight > 0) ? ItemHeight : _FontHeight;

        protected override void DrawElement(ExtendedSpriteBatch spriteBatch)
        {
            if ((spriteBatch == null) || spriteBatch.IsDisposed || ElementBounds.IsEmpty)
                return;

            if ((Font == null) || (Items == null) || (Items.Count == 0))
                return;

            var itemBounds = new Rectangle(ElementBounds.X, ElementBounds.Y, ElementBounds.Width, (int)RealItemHeight);

            if ((itemBounds.Width <= 0) || (itemBounds.Height <= 0))
                return;

            for (int ix = _listbox.TopIndex; ix < _listbox.BottomIndex; ix++, itemBounds.Y += itemBounds.Height)
            {
                var item = Items[ix];
                if (item == null)
                    continue;

                var text = GetItemText(item);

                if (string.IsNullOrEmpty(text))
                    continue;

                DrawItemInternal(spriteBatch, itemBounds, ix == _listbox.SelectedIndex, text, ForeColour, BackColour);
            }

            if (!_listbox.CanScroll)
                return;

            var scrollBtnWidth = 50;
            var scrollBtnHeight = 40;

            //rect for entire ScrollBar
            var scrollRect = new Rectangle(ElementBounds.Right - scrollBtnWidth, ElementBounds.Y, scrollBtnWidth, ElementBounds.Height);

            //rect for scroll up/down buttons
            var scrollUpRect = new Rectangle(scrollRect.X, scrollRect.Y, scrollBtnWidth, scrollBtnHeight);
            var scrollDnRect = new Rectangle(scrollRect.X, scrollRect.Bottom - scrollBtnHeight, scrollBtnWidth, scrollBtnHeight);

            //rect for ScrollBar less buttons
            var scrollBarRect = new Rectangle(scrollRect.X, scrollRect.Y + scrollBtnHeight, scrollRect.Width, scrollRect.Height - (scrollBtnHeight * 2));


            var ScrollMinimum = (float)_listbox.ScrollParameters.GetScrollBarMinimum(RealItemHeight);
            var ScrollMaximum = (float)_listbox.ScrollParameters.GetScrollBarMaximum(RealItemHeight);
            var ScrollSmallChange = (float)_listbox.ScrollParameters.GetScrollBarSmallChange(RealItemHeight);
            var ScrollLargeChange = (float)_listbox.ScrollParameters.GetScrollBarLargeChange(RealItemHeight);
            var ScrollValue = (float)_listbox.ScrollPosition.GetScrollBarValue(RealItemHeight);


            //var offset = scrollBarRect.Height * ScrollValue / (ScrollMaximum - ScrollLargeChange + ScrollSmallChange);
            //var len = scrollBarRect.Height * ScrollSmallChange / (ScrollMaximum - ScrollLargeChange + ScrollSmallChange);
            //var scrollBarVal = new Rectangle(scrollRect.X, scrollBarRect.Y + offset, scrollBtnWidth, (int)(ScrollLargeChange * scale));
            //var scrollBarVal = new Rectangle(scrollRect.X, scrollBarRect.Y + (int)offset, scrollBtnWidth, (int)len); //works, but not big enough


            //Rect for the bar
            var barHeight = scrollBarRect.Height * ScrollLargeChange / (ScrollMaximum - ScrollMinimum);
            var barOffset = (scrollBarRect.Height - barHeight) * ScrollValue / ((ScrollMaximum - ScrollMinimum) - ScrollLargeChange);
            var scrollBarVal = new Rectangle(scrollRect.X, scrollBarRect.Y + (int)barOffset, scrollBtnWidth, (int)barHeight);

            spriteBatch.FillRectangle(scrollUpRect, _listbox.CanScrollPreviousItem ? Color.SteelBlue : Color.Gray);
            spriteBatch.FillRectangle(scrollDnRect, _listbox.CanScrollNextItem ? Color.SteelBlue : Color.Gray);
            spriteBatch.FillRectangle(scrollBarRect, Color.DimGray);
            spriteBatch.FillRectangle(scrollBarVal, Color.Orange);
        }

        public int SelectedIndex
        {
            get => _listbox.SelectedIndex;
            set => _listbox.SelectedIndex = value;
        }

        public void SelectionUp() => _listbox.SelectPreviousItem();
        public void SelectionDown() => _listbox.SelectNextItem();
        public void SelectionTop() => _listbox.SelectFirstItem();
        public void SelectionBottom() => _listbox.SelectLastItem();
        public void SelectionPageUp() => _listbox.SelectPreviousPageItem();
        public void SelectionPageDown() => _listbox.SelectNextPageItem();
        public void ScrollUp() => _listbox.ScrollPreviousItem();
        public void ScrollDown() => _listbox.ScrollNextItem();
        public void ScrollTop() => _listbox.ScrollFirstItem();
        public void ScrollBottom() => _listbox.ScrollLastItem();
        public void ScrollPageUp() => _listbox.ScrollPreviousPageItem();
        public void ScrollPageDown() => _listbox.ScrollNextPageItem();

        protected virtual string GetItemTextInternal(object item)
        {
            if (item == null)
                return null;

            if (!string.IsNullOrEmpty(FormatString))
                return string.Format(FormatString, item);

            return item.ToString();
        }

        protected virtual void DrawItemInternal(ExtendedSpriteBatch spriteBatch, Rectangle itemBounds,
            bool selected, string itemText, Color foreColour, Color backColor)
        {
            if (OwnerDraw)
            {
                DrawItem?.Invoke(this, new DrawItemEventArgs()
                {
                    spriteBatch = spriteBatch,
                    itemBounds = itemBounds,
                    selected = selected,
                    itemText = itemText,
                    foreColour = foreColour,
                    backColor = backColor,
                });
            }
            else
            {
                if (selected)
                    spriteBatch.FillRectangle(itemBounds, Color.SteelBlue);

                var size = Font.MeasureString(itemText);
                var textRect = new Rectangle(Point.Zero, size.ToPoint());

                var textBounds = textRect.AlignInside(itemBounds, Alignment);

                spriteBatch.DrawString(Font, itemText, textBounds.Location.ToVector2(), ForeColour,
                    0, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }

        public Func<object, string> GetItemText { get; set; }

        public event EventHandler<DrawItemEventArgs> DrawItem;

        public class DrawItemEventArgs : EventArgs
        {
            public ExtendedSpriteBatch spriteBatch { get; set; }
            public Rectangle itemBounds { get; set; }
            public bool selected { get; set; }
            public string itemText { get; set; }
            public Color foreColour { get; set; }
            public Color backColor { get; set; }
        }
    }
}
