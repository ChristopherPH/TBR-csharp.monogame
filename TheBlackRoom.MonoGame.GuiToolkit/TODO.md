TO-DO
=====



New Elements
-----------------

#### Scrolling label
- Like a stock ticker, would use animation to scroll text


#### Textbox 
- with colors and scroll


#### Checkbox
- Checked property


#### Button
- PictureBox + Label combined


#### Line
- Horizontal or vertical seperator


#### Picture List
- Same as picturebox, but stores a list of pictures and an index
- List of pictures should be a re-usable class that can be set into any picture list
  - Could have a draw method as well, to draw the active or given index to a rectangle
  - Draw function should be static in GuiDraw


New Adornments
-----------------

### Focus
- A simple overlay that draws a rectangle around the focused element

### Scrollbar
- Adds an IScroller interface
- Stock Scrollbar can draw triangles
- Scrollbar, up/down arrows docked at the bottom..



Existing Elements
-----------------

### GuiLayout
- could set all interfaces (adornments, borders)
- could set all UI Scale
- get gui elements by key for easy access to children
  - guiLayout["foo"].Text = "bar";
  - override or catch add to store gui elements in dictionary
  - or.. just have a getchildelementbyname("...") and return that
- hold focused control
- Can also use hittest to handle focus change
- Load xml/yml layout

### Panel
- Create columns/rows via rectangle slicing



Unsorted TODOs
-----------------

- Add license to all files
- Consider adding .Tag property to elements

### Refactor
- Rename content alignment maybe to avoid clash. But rectangle?
- Spritebatchex can split whitetexture calls to extension methods (shapes)

### HitTest:
- Hittest might return adornment, or element?
  - Maybe should return element, and optional [] adornments (border, scrollbar, etc)

### Utility
- Make a utility draw class that creates the needed spritebatch for drawing and calls draw

### Animations
Need to figure out how to do animations..
- Animations chould have event fired when completed, or just automatically set visible/enabled based on animation type

Types:
- Slide in/out
- Fade in/out
- Flip in/out

### Selections
- Add IGuiSelection adornment
- Add a way to cycle through selected items in a collection
  - Something like `GuiElement SelectNextElement(GuiElement currentElement)`
  - basically indexof() + 1 % count
  - Is this useful? It would only work inside a collection. Maybe the layout needs to be able to do it through all descendants
