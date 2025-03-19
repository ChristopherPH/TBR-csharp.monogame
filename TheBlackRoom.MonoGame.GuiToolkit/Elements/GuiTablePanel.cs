using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheBlackRoom.MonoGame.GuiToolkit.Elements
{
    public class GuilElementTableData
    {
        public int MinimumWidth { get; set; } = -1;
        public int MaximumWidth { get; set; } = -1;
        public int MinimumHeight { get; set; } = -1;
        public int MaximumHeight { get; set; } = -1;
        public int Column { get; set; } = -1;
        public int Row { get; set; } = -1;
        public int ColumnSpan { get; set; } = 1;
        public int RowSpan { get; set; } = 1;
    }

    public class GuiTablePanel : GuiElementCollection
    {
        private List<int> _ColumnWidths = new List<int>();
        private List<int> _RowHeights = new List<int>();
        private Dictionary<GuiElement, GuilElementTableData> _ElementLookup = new Dictionary<GuiElement, GuilElementTableData>();

        public GuiTablePanel() { }

        public GuiTablePanel(IEnumerable<GuiElementColumnStyle> ColumnStyles,
            IEnumerable<GuiElementRowStyle> RowStyles)
        {
            if ((ColumnStyles == null) || (RowStyles == null))
                return;

            this.ColumnStyles.AddRange(ColumnStyles);
            this.RowStyles.AddRange(RowStyles);

            LayoutTable();
        }

        public List<GuiElementColumnStyle> ColumnStyles { get; } = new List<GuiElementColumnStyle>();
        public List<GuiElementRowStyle> RowStyles { get; } = new List<GuiElementRowStyle>();

        public void AddColumn(GuiElementColumnStyle columnStyle)
        {
            if (columnStyle == null)
                return;

            ColumnStyles.Add(columnStyle);

            LayoutTable();
        }

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
        public void Add(GuiElement element, int column, int row)
        {
            if (AddCollectionElement(element))
            {
                _ElementLookup[element] = new GuilElementTableData()
                {
                    Column = column,
                    Row = row
                };

                LayoutElements();
            }
        }

        /// <summary>
        /// Removes the specified Gui Element from the layout
        /// </summary>
        /// <param name="element">Gui element to remove</param>
        public void Remove(GuiElement element, int column, int row)
        {
            if (RemoveCollectionElement(element))
            {
                ElementCollection.Remove(element);

                LayoutElements();
            }
        }

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
                int variableWidth = Math.Max(0, ContentBounds.Width - totalWidth);

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
                int variableHeight = Math.Max(0, ContentBounds.Height - totalHeight);

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

            LayoutElements();
        }

        protected void LayoutElements()
        {
            //Update element bounds
            for (int columnIndex = 0; columnIndex < _ColumnWidths.Count; columnIndex++)
            {
                System.Diagnostics.Debug.Print($"Column: {columnIndex} {_ColumnWidths[columnIndex]}");
            }

            //Update element bounds
            for (int rowIndex = 0; rowIndex < _RowHeights.Count; rowIndex++)
            {
                System.Diagnostics.Debug.Print($"Row: {rowIndex} {_RowHeights[rowIndex]}");
            }
        }

        protected override void DrawGuiElement(GameTime gameTime, ExtendedSpriteBatch spriteBatch,
            Rectangle drawBounds)
        {
            base.DrawGuiElement(gameTime, spriteBatch, drawBounds);

            var color = Color.Magenta;
            var thick = 2;

            int x = drawBounds.Left;
            int y = drawBounds.Top;

            //draw column lines
            for (int columnIndex = 0; columnIndex < _ColumnWidths.Count; columnIndex++)
            {
                x += _ColumnWidths[columnIndex];

                spriteBatch.DrawLine(x, drawBounds.Top, x, drawBounds.Bottom, color, thick);
            }

            //draw row lines
            for (int rowIndex = 0; rowIndex < _RowHeights.Count; rowIndex++)
            {
                y += _RowHeights[rowIndex];

                spriteBatch.DrawLine(drawBounds.Left, y, drawBounds.Right, y, color, thick);
            }
        }

        protected override void OnBoundsChanged()
        {
            base.OnBoundsChanged();

            LayoutTable();
        }
    }
}
