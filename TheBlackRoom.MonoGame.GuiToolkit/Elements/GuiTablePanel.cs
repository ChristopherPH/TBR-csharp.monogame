using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    public class GuiTablePanel : GuiElementCollection
    {
        private List<int> _ColumnWidths = new List<int>();
        private List<int> _RowHeights = new List<int>();
        private List<int> _ColumnOffsets = new List<int>();
        private List<int> _RowOffsets = new List<int>();
        private Dictionary<GuiElement, GuiElementCollectionMetaData> _ElementMetaData
            = new Dictionary<GuiElement, GuiElementCollectionMetaData>();
        private List<GuiElementColumnStyle> ColumnStyles { get; } = new List<GuiElementColumnStyle>();
        private List<GuiElementRowStyle> RowStyles { get; } = new List<GuiElementRowStyle>();

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GuiTablePanel() { }

        /// <summary>
        /// Constructor with Column and Row Styles
        /// </summary>
        /// <param name="ColumnStyles">Column styles</param>
        /// <param name="RowStyles">Row styles</param>
        public GuiTablePanel(IEnumerable<GuiElementColumnStyle> ColumnStyles,
            IEnumerable<GuiElementRowStyle> RowStyles)
        {
            if ((ColumnStyles == null) || (RowStyles == null))
                return;

            this.ColumnStyles.AddRange(ColumnStyles);
            this.RowStyles.AddRange(RowStyles);

            LayoutTable();
        }

        /// <summary>
        /// Colour of Grid Lines
        /// </summary>
        public Color GridLineColour
        {
            get => _GridLineColour;
            set
            {
                if (_GridLineColour == value) return;
                _GridLineColour = value;
            }
        }
        private Color _GridLineColour = Color.Transparent;

        /// <summary>
        /// Thickness of Grid Lines
        /// </summary>
        public int GridLineThickness
        {
            get => _GridLineThickness;
            set
            {
                if (_GridLineThickness == value) return;
                _GridLineThickness = value;
            }
        }
        private int _GridLineThickness = 1;

        /// <summary>
        /// Returns the number of columns in the table panel
        /// </summary>
        public int ColumnCount => _ColumnWidths.Count;

        /// <summary>
        /// Returns the number of rows in the table panel
        /// </summary>
        public int RowCount => _RowHeights.Count;

        /// <summary>
        /// Adds a column to the table panel
        /// </summary>
        /// <param name="columnStyle">Style of column to add</param>
        public void AddColumn(GuiElementColumnStyle columnStyle)
        {
            if (columnStyle == null)
                return;

            ColumnStyles.Add(columnStyle);

            LayoutTable();
        }

        /// <summary>
        /// Adds a row to the table panel
        /// </summary>
        /// <param name="rowStyle">Style of row to add</param>
        public void AddRow(GuiElementRowStyle rowStyle)
        {
            if (rowStyle == null)
                return;

            RowStyles.Add(rowStyle);

            LayoutTable();
        }

        /// <summary>
        /// Adds the specified Gui Element to the layout
        /// </summary>
        /// <param name="element">Gui element to add</param>
        /// <param name="column">Cell column</param>
        /// <param name="row">Cell row</param>
        /// <param name="columnSpan">Cell column span</param>
        /// <param name="rowSpan">Cell row span</param>
        public void Add(GuiElement element, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            if (AddCollectionElement(element))
            {
                _ElementMetaData[element] = new GuiElementCollectionMetaData()
                {
                    Column = column,
                    Row = row,
                    ColumnSpan = columnSpan,
                    RowSpan = rowSpan,
                };

                LayoutElements();
            }
        }

        /// <summary>
        /// Removes the specified Gui Element from the layout
        /// </summary>
        /// <param name="element">Gui element to remove</param>
        public void Remove(GuiElement element)
        {
            if (RemoveCollectionElement(element))
            {
                ElementCollection.Remove(element);

                LayoutElements();
            }
        }

        /// <summary>
        /// Removes the specified Gui Element from the layout
        /// </summary>
        /// <param name="column">Cell column</param>
        /// <param name="row">Cell row</param>
        public GuiElement Remove(int column, int row)
        {
            var element = GetElement(column, row);

            if (element != null)
                Remove(element);

            return element;
        }

        /// <summary>
        /// Gets an element from the specified column and row
        /// </summary>
        /// <param name="column">Cell column</param>
        /// <param name="row">Cell row</param>
        /// <returns>Element</returns>
        public GuiElement GetElement(int column, int row)
        {
            return _ElementMetaData.FirstOrDefault(x => x.Value.Column == column && x.Value.Row == row).Key;
        }

        /// <summary>
        /// Sets an element to the specified column and row
        /// </summary>
        /// <param name="element">Gui element to set</param>
        /// <param name="column">Cell column</param>
        /// <param name="row">Cell row</param>
        /// <param name="columnSpan">Cell column span</param>
        /// <param name="rowSpan">Cell row span</param>
        public void SetCell(GuiElement element, int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            if (_ElementMetaData.TryGetValue(element, out var elementInfo))
            {
                elementInfo.Column = column;
                elementInfo.Row = row;
                elementInfo.ColumnSpan = columnSpan;
                elementInfo.RowSpan = rowSpan;

                LayoutElements();
            }
        }

        /// <summary>
        /// Sets an element to the specified column and row span
        /// </summary>
        /// <param name="element">Gui element to set</param>
        /// <param name="columnSpan">Cell column span</param>
        /// <param name="rowSpan">Cell row span</param>
        public void SetCellSpan(GuiElement element, int columnSpan, int rowSpan)
        {
            if (_ElementMetaData.TryGetValue(element, out var elementInfo))
            {
                elementInfo.ColumnSpan = columnSpan;
                elementInfo.RowSpan = rowSpan;

                LayoutElements();
            }
        }

        /// <summary>
        /// Sets an element to the specified column span
        /// </summary>
        /// <param name="element">Gui element to set</param>
        /// <param name="columnSpan">Cell column span</param>
        public void SetColumnSpan(GuiElement element, int columnSpan)
        {
            if (_ElementMetaData.TryGetValue(element, out var elementInfo))
            {
                elementInfo.ColumnSpan = columnSpan;

                LayoutElements();
            }
        }

        /// <summary>
        /// Sets an element to the specified  row span
        /// </summary>
        /// <param name="element">Gui element to set</param>
        /// <param name="rowSpan">Cell row span</param>
        public void SetRowSpan(GuiElement element, int rowSpan)
        {
            if (_ElementMetaData.TryGetValue(element, out var elementInfo))
            {
                elementInfo.RowSpan = rowSpan;

                LayoutElements();
            }
        }

        /// <summary>
        /// Gets the bounds of the specified cell
        /// </summary>
        /// <param name="column">Cell column</param>
        /// <param name="row">Cell row</param>
        /// <returns>Cell bounds, or Rectangle.Empty on invalid cell</returns>
        protected Rectangle GetCellBounds(int column, int row, int columnSpan = 1, int rowSpan = 1)
        {
            //Check for valid column and row
            if ((column < 0) || (row < 0) ||
                (column >= _ColumnWidths.Count) ||
                (row >= _RowHeights.Count))
            {
                return Rectangle.Empty;
            }

            //adjust column and row spans to be within range
            columnSpan = MathHelper.Min(MathHelper.Max(1, columnSpan), _ColumnWidths.Count - column);
            rowSpan = MathHelper.Min(MathHelper.Max(1, rowSpan), _RowHeights.Count - row);

            //calculate spanned column and row sizes
            int width = 0, height = 0;

            for (int c = 0; c < columnSpan; c++)
                width += _ColumnWidths[column + c];

            for (int r = 0; r < rowSpan; r++)
                height += _RowHeights[row + r];

            return new Rectangle(_ColumnOffsets[column], _RowOffsets[row], width, height);
        }

        /// <summary>
        /// Updates the layout of the table panel, determines column and row widths
        /// </summary>
        protected void LayoutTable()
        {
            int totalWidth = 0;
            float totalWidthPercent = 0f;
            var columnWidths = new List<int>();
            var columnPercents = new List<float>();

            int totalHeight = 0;
            float totalHeightPercent = 0f;
            var rowHeights = new List<int>();
            var rowPercents = new List<float>();

            //Determine column totals
            for (int columnIndex = 0; columnIndex < ColumnStyles.Count; columnIndex++)
            {
                //Default current column to no width (and no percent)
                columnWidths.Add(0);
                columnPercents.Add(0f);

                //Add up total width of absolute columns
                if (ColumnStyles[columnIndex] is GuiElementColumnStyleAbsolute columnStyleAbsolute)
                {
                    totalWidth += columnStyleAbsolute.Width;
                    columnWidths[columnIndex] = columnStyleAbsolute.Width;
                }

                //Add up total percent of any percent and variable percent columns
                if (ColumnStyles[columnIndex] is GuiElementColumnStylePercent columnStylePercent)
                {
                    totalWidthPercent += columnStylePercent.Percent;
                    columnPercents[columnIndex] = columnStylePercent.Percent;
                }
            }

            //If there are any percent columns, then adjust widths of columns
            if (totalWidthPercent > 0f)
            {
                //Determine amount of width leftover for percent columns
                int variableWidth = Math.Max(0, ContentWidth - totalWidth);

                int adjustWidth = 0;
                float adjustWidthPercent = 0;

                //Change any variable percent columns to absolute columns if they exceed min/max
                for (int columnIndex = 0; columnIndex < ColumnStyles.Count; columnIndex++)
                {
                    if (!(ColumnStyles[columnIndex] is GuiElementColumnStyleVariablePercent columnStyleVariablePercent))
                        continue;

                    //Determine the absolute width of the column from the percent
                    var width = (int)Math.Round(variableWidth * columnStyleVariablePercent.Percent / totalWidthPercent);

                    //Convert column to absolute
                    if ((columnStyleVariablePercent.MinimumWidth >= 0) && (width < columnStyleVariablePercent.MinimumWidth))
                    {
                        adjustWidthPercent += columnStyleVariablePercent.Percent;
                        adjustWidth += columnStyleVariablePercent.MinimumWidth;

                        columnWidths[columnIndex] = columnStyleVariablePercent.MinimumWidth;
                        columnPercents[columnIndex] = 0;
                    }
                    else if ((columnStyleVariablePercent.MaximumWidth >= 0) && (width > columnStyleVariablePercent.MaximumWidth))
                    {
                        adjustWidthPercent += columnStyleVariablePercent.Percent;
                        adjustWidth += columnStyleVariablePercent.MaximumWidth;

                        columnWidths[columnIndex] = columnStyleVariablePercent.MaximumWidth;
                        columnPercents[columnIndex] = 0;
                    }
                }

                //Update the variable width and total percent width after adjusting columns
                variableWidth = Math.Max(0, variableWidth - adjustWidth);
                totalWidthPercent = Math.Max(0, totalWidthPercent - adjustWidthPercent);

                //Determine column widths from remaining percent columns
                for (int columnIndex = 0; columnIndex < ColumnStyles.Count; columnIndex++)
                {
                    if (columnPercents[columnIndex] > 0)
                    {
                        //Determine the absolute width of the column from the percent if possible
                        if ((variableWidth == 0) || (totalWidthPercent == 0))
                            columnWidths[columnIndex] = 0;
                        else
                            columnWidths[columnIndex] = (int)Math.Round(variableWidth * columnPercents[columnIndex] / totalWidthPercent);
                    }
                }
            }

            //Determine row totals
            for (int rowIndex = 0; rowIndex < RowStyles.Count; rowIndex++)
            {
                //Default current row to no height (and no percent)
                rowHeights.Add(0);
                rowPercents.Add(0f);

                //Add up total height of absolute rows
                if (RowStyles[rowIndex] is GuiElementRowStyleAbsolute rowStyleAbsolute)
                {
                    totalHeight += rowStyleAbsolute.Height;
                    rowHeights[rowIndex] = rowStyleAbsolute.Height;
                }

                //Add up total percent of any percent and variable percent rows
                if (RowStyles[rowIndex] is GuiElementRowStylePercent rowStylePercent)
                {
                    totalHeightPercent += rowStylePercent.Percent;
                    rowPercents[rowIndex] = rowStylePercent.Percent;
                }
            }

            //If there are any percent rows, then adjust heights of rows
            if (totalHeightPercent > 0f)
            {
                //Determine amount of height leftover for percent rows
                int variableHeight = Math.Max(0, ContentHeight - totalHeight);

                int adjustHeight = 0;
                float adjustHeightPercent = 0;

                //Change any variable percent rows to absolute rows if they exceed min/max
                for (int rowIndex = 0; rowIndex < RowStyles.Count; rowIndex++)
                {
                    if (!(RowStyles[rowIndex] is GuiElementRowStyleVariablePercent rowStyleVariablePercent))
                        continue;

                    //Determine the absolute height of the row from the percent
                    var height = (int)Math.Round(variableHeight * rowStyleVariablePercent.Percent / totalHeightPercent);

                    //Convert row to absolute
                    if ((rowStyleVariablePercent.MinimumHeight >= 0) && (height < rowStyleVariablePercent.MinimumHeight))
                    {
                        adjustHeightPercent += rowStyleVariablePercent.Percent;
                        adjustHeight += rowStyleVariablePercent.MinimumHeight;

                        rowHeights[rowIndex] = rowStyleVariablePercent.MinimumHeight;
                        rowPercents[rowIndex] = 0;
                    }
                    else if ((rowStyleVariablePercent.MaximumHeight >= 0) && (height > rowStyleVariablePercent.MaximumHeight))
                    {
                        adjustHeightPercent += rowStyleVariablePercent.Percent;
                        adjustHeight += rowStyleVariablePercent.MaximumHeight;

                        rowHeights[rowIndex] = rowStyleVariablePercent.MaximumHeight;
                        rowPercents[rowIndex] = 0;
                    }
                }

                //Update the variable height and total percent height after adjusting rows
                variableHeight = Math.Max(0, variableHeight - adjustHeight);
                totalHeightPercent = Math.Max(0, totalHeightPercent - adjustHeightPercent);

                //Determine row heights from remaining percent rows
                for (int rowIndex = 0; rowIndex < RowStyles.Count; rowIndex++)
                {
                    if (rowPercents[rowIndex] > 0)
                    {
                        //Determine the absolute height of the row from the percent if possible
                        if ((variableHeight == 0) || (totalHeightPercent == 0))
                            rowHeights[rowIndex] = 0;
                        else
                            rowHeights[rowIndex] = (int)Math.Round(variableHeight * rowPercents[rowIndex] / totalWidthPercent);
                    }
                }
            }


            //Check if the column widths or row heights have changed
            if ((columnWidths.Count == _ColumnWidths.Count) &&
                (rowHeights.Count == _RowHeights.Count) &&
                columnWidths.SequenceEqual(_ColumnWidths) &&
                rowHeights.SequenceEqual(_RowHeights))
            {
                return;
            }

            _ColumnWidths = columnWidths;
            _RowHeights = rowHeights;

            //Rebuild column and row offsets
            _ColumnOffsets.Clear();
            _RowOffsets.Clear();

            int x = 0, y = 0;

            for (int columnIndex = 0; columnIndex < _ColumnWidths.Count; x += _ColumnWidths[columnIndex], columnIndex++)
                _ColumnOffsets.Add(x);
            _ColumnOffsets.Add(x);

            for (int rowIndex = 0; rowIndex < _RowHeights.Count; y += _RowHeights[rowIndex], rowIndex++)
                _RowOffsets.Add(y);
            _RowOffsets.Add(y);

            LayoutElements();
        }

        /// <summary>
        /// Updates the layout of the table panel, determines element bounds
        /// </summary>
        protected void LayoutElements()
        {
            //Change element bounds based on column and row
            foreach (var element in ElementCollection)
            {
                if (_ElementMetaData.TryGetValue(element, out var elementInfo))
                {
                    var cellBounds = GetCellBounds(elementInfo.Column, elementInfo.Row,
                        elementInfo.ColumnSpan, elementInfo.RowSpan);

                    //TODO: Add anchor edges / alignment
                    //TODO: Add cell padding

                    element.Bounds = cellBounds;
                }
            }
        }

        protected override void DrawGuiElement(GameTime gameTime, ExtendedSpriteBatch spriteBatch,
            Rectangle drawBounds)
        {
            base.DrawGuiElement(gameTime, spriteBatch, drawBounds);

            //Determine if grid lines should be drawn
            if ((GridLineColour == Color.Transparent) || (GridLineThickness <= 0))
                return;

            //draw column lines
            for (int columnIndex = 1; columnIndex <= _ColumnWidths.Count; columnIndex++)
            {
                var x = drawBounds.Left + _ColumnOffsets[columnIndex];

                spriteBatch.DrawLine(x, drawBounds.Top, x, drawBounds.Bottom, GridLineColour, GridLineThickness);
            }

            //draw row lines
            for (int rowIndex = 1; rowIndex <= _RowHeights.Count; rowIndex++)
            {
                var y = drawBounds.Top + _RowOffsets[rowIndex];

                spriteBatch.DrawLine(drawBounds.Left, y, drawBounds.Right, y, GridLineColour, GridLineThickness);
            }
        }

        protected override void OnSizeChanged(Point oldSize)
        {
            base.OnSizeChanged(oldSize);

            LayoutTable();
        }

        protected override void OnMarginChanged()
        {
            base.OnMarginChanged();

            LayoutTable();
        }

        private class GuiElementCollectionMetaData
        {
            public int Column { get; set; } = -1;
            public int Row { get; set; } = -1;

            public int ColumnSpan { get; set; } = 1;
            public int RowSpan { get; set; } = 1;

            //TODO: Add anchor edges / alignment
            //TODO: Add cell padding
        }
    }
}
