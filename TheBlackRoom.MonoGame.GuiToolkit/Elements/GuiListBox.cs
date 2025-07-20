using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TheBlackRoom.MonoGame.Drawing;
using TheBlackRoom.Core.Helpers.IListHelpers;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    /// <summary>
    /// ListBox Gui Element
    /// </summary>
    public class GuiListBox : GuiTextElement
    {
        private ListBoxWrapper<object> _listbox = new ListBoxWrapper<object>();

        public GuiListBox()
        {
            _listbox.ListItems = _Items;
        }

        /// <summary>
        /// Items contained within listbox
        /// </summary>
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

        /// <summary>
        /// Height of each item within listbox
        /// </summary>
        public float ItemHeight
        {
            get => _ItemHeight;
            set
            {
                if (_ItemHeight == value) return;

                _ItemHeight = value;
                _listbox.MaximumItemCount = ContentHeight / RealItemHeight;
                _listbox.OnListItemsChanged();
            }
        }
        private float _ItemHeight = 0;


        /// <summary>
        /// Label alignment within bounds
        /// </summary>
        public ContentAlignment Alignment
        {
            get => _Alignment;
            set
            {
                if (_Alignment == value) return;
                _Alignment = value;
                OnAlignmentChanged();
            }
        }
        private ContentAlignment _Alignment = ContentAlignment.MiddleLeft;

        /// <summary>
        /// FormatString to apply to items in list, use {0} to reference list item
        /// </summary>
        public string FormatString { get; set; } = string.Empty;

        /// <summary>
        /// Flag indicating that DrawItem event should be used to draw list item
        /// </summary>
        public bool OwnerDraw { get; set; } = false;

        /// <summary>
        /// Function to retrive string for list item, enabled when FormatString is
        /// empty, used when FormatString is not sufficient
        /// </summary>
        public Func<object, string> GetItemText { get; set; }

        /// <summary>
        /// Event raised when OwnerDraw=true to draw list item
        /// </summary>
        public event EventHandler<DrawItemEventArgs> DrawItem;

        /// <summary>
        /// Flag indicating scrollbar should be shown
        /// </summary>
        public bool ShowScrollbar { get; set; } = true;

        /// <summary>
        /// To be called when items are added or removed from the list
        /// </summary>
        public void NotifyListItemsChanged() => _listbox.OnListItemsChanged();

        protected override void OnFontChanged()
        {
            base.OnFontChanged();

            _FontHeight = (Font == null) ? 0 : Font.MeasureString("Wy").Y;

            _listbox.MaximumItemCount = ContentHeight / RealItemHeight;
        }

        float _FontHeight = 0;

        protected float RealItemHeight => (ItemHeight > 0) ? ItemHeight : _FontHeight;

        protected override void UpdateGuiElement(GameTime gameTime) { }

        protected override void DrawGuiElement(GameTime gameTime,
            ExtendedSpriteBatch spriteBatch, Rectangle drawBounds)
        {
            if ((Font == null) || (Items == null) || (Items.Count == 0))
                return;

            var itemBounds = new Rectangle(drawBounds.X, drawBounds.Y, drawBounds.Width, (int)RealItemHeight);

            if ((itemBounds.Width <= 0) || (itemBounds.Height <= 0))
                return;

            for (int ix = _listbox.TopIndex; ix < _listbox.BottomIndex; ix++, itemBounds.Y += itemBounds.Height)
            {
                var item = Items[ix];
                if (item == null)
                    continue;

                var text = GetItemText != null ? GetItemText(item) : GetItemTextInternal(item);

                if (string.IsNullOrEmpty(text))
                    continue;

                DrawItemInternal(spriteBatch, ix, itemBounds,
                    ix == _listbox.SelectedIndex, text, ForeColour, BackColour);
            }

            if (!ShowScrollbar)
                return;

            if (!_listbox.CanScroll)
                return;

            var scrollBtnWidth = 50;
            var scrollBtnHeight = 40;

            //rect for entire ScrollBar
            var scrollRect = new Rectangle(drawBounds.Right - scrollBtnWidth, drawBounds.Y, scrollBtnWidth, drawBounds.Height);

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

            //Try to format the object based on a format string
            if (!string.IsNullOrEmpty(FormatString))
            {
                try
                {
                    return string.Format(FormatString, item);
                }
                catch (FormatException) { }
            }

            //If the item is already string, then return that
            if (item is string s)
                return s;

            return item.ToString();
        }

        protected virtual void DrawItemInternal(ExtendedSpriteBatch spriteBatch,
            int itemIndex, Rectangle itemBounds, bool selected, string itemText,
            Color foreColour, Color backColor)
        {
            if (OwnerDraw)
            {
                DrawItem?.Invoke(this, new DrawItemEventArgs()
                {
                    spriteBatch = spriteBatch,
                    itemIndex = itemIndex,
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

        /// <summary>
        /// Occurs when the Gui Element Alignment property has changed
        /// </summary>
        protected virtual void OnAlignmentChanged() { }


        public class DrawItemEventArgs : EventArgs
        {
            public ExtendedSpriteBatch spriteBatch { get; set; }
            public int itemIndex { get; set; }
            public Rectangle itemBounds { get; set; }
            public bool selected { get; set; }
            public string itemText { get; set; }
            public Color foreColour { get; set; }
            public Color backColor { get; set; }
        }
    }
}
