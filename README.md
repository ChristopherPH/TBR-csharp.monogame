# TheBlackRoom.MonoGame Libraries

A collection of utility functions, extension methods, and helpers for the c# MonoGame framework.

For more information about the MonoGame framework, visit https://monogame.net/


## Organizational Notes

- Libraries are implemented as shared projects to cleanly integrate into existing projects
- Libraries are split by namespace as much as possible to reduce dependancies


## TheBlackRoom.MonoGame

Utility functions, extension methods, helpers for MonoGame.


### TheBlackRoom.MonoGame.Drawing

Extensions for:

- Rectangle


### TheBlackRoom.MonoGame.Extensions

Extensions for:

- SpriteBatch


### TheBlackRoom.MonoGame.Interpolator


### TheBlackRoom.MonoGame.MenuSystem



## TheBlackRoom.GameFramework

A framework for creating a basic game.

Features:

- Game state system
- Game setting system
- Built-in video resolution settings
- Built-in audio resolution settings


## TheBlackRoom.MonoGame.GuiToolkit

A toolkit for creating a basic game user interface.

Provides a simple way to create and manage GUI layouts, via GUI elements. GUI
elements are self-contained objects that can be positioned, drawn and updated
independently of each other, which provide some basic functionality that is
expected from a GUI toolkit.


### GuiToolkit Features:

- GUI elements can be drawn and updated independently or as part of a GUI element collection
- GUI elements can be positioned with an abolute position, or relative to other GUI elements
- GUI elements can be easily subclassed, to use custom Update and/or Draw methods
- GUI element draw methods are implemented as static methods to allowing for custom elements to have a consistent look
- Complex GUI elements provide OwnerDraw functionality
- GUI element collections provide element anchoring functionalty for maintaining relative element positions within the collection
- GUI elements are cropped to their bounds (via ScissorTest)


### Built-In Elements:

- Element Collections:
  - Layout (Top level GUI layout)
  - Panel
  - TablePanel
- Elements:
  - Label
  - ListBox
  - PictureBox
  - ShadowLabel


### Element Adornments:
- Borders: Solid, Raised, 3D


### API Reference

#### GuiElement (base class)

Properties:
- Name
- Bounds, Size, Location
- Padding, Margin
- BackColour

Methods:
- Update()
- Draw()
- HitTest()

#### ... more to come
