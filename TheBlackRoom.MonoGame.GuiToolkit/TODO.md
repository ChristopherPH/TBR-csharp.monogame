TO-DO
=====



New Elements
-----------------

#### Scrolling label
- Like a stock ticker, would use animation to scroll text


#### Textbox 
- with colors and scroll



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

### Refactor
- Rename content alignment maybe to avoid clash. But rectangle?
- Spritebatchex can split whitetexture calls to extension methods (shapes)

### HitTest:
- Hittest might return adornment, or element?
  - Maybe should return element, and optional [] adornments (border, scrollbar, etc)

### Utility
- Make a utility draw class that creates the needed spritebatch for drawing and calls draw
