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


## TheBlackRoom.MonoGame.GuiFramework

A framework for creating a basic game user interface.

Features:

- Easy to subclass any control
- OwnerDraw functionality

Standard Elements:

- PictureBox
- Label
- ListBox
