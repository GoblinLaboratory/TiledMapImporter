# TiledMapImporter
A C# library for using custom maps made with the Tiled Map Editor (http://www.mapeditor.org/) in MonoGame/Xna.
------------

Introduction
------------
This project developed out of my own need to import maps made using Tiled into my MonoGame projects. I originally tried using some of the libraries already out there but none seem to work well enough. I therefore created my own.

The Tiled Map Editor is a great piece of free software that you can find here, http://www.mapeditor.org/.
In order to parse maps saved in JSON format, I use JSON.net which can be found here http://www.newtonsoft.com/json.
This project is written in C# and is designed to be used in MonoGame (http://www.monogame.net/). While it has been discontinued, Microsoft's XNA 4.0 should also work well with it.

Quick Start
------------
To use this library in MonoGame, add every file from a folder in this project called MapImporter (https://github.com/starfleetcadet75/TiledMapImporter/tree/master/TiledMapImporter/MapImporter) to a new folder in your project. 
  1) Add a using statement for the folder:
    using TiledMapImporter.MapImporter;
  2) Create a new Map object by calling this line:
    Map map = Importer.ImportMap(@"Content/NewBarkTown.json");

The library supports both TMX and JSON map files (TMX support will be ready soon) so make sure you specify the extension. Theoretically, you can load your Tiled map file from anywhere as long as you specify the right path. However, the images used in your tilesets must be loaded in the way usually required by MonoGame. (See this project's wiki if you don't know what that is...)

  3) Because the tileset images (which should be in XNB format somewhere inside your content folder) must be loaded as Texture2D objects, you have to call this line directly after creating a new map object:
    map.LoadTilesetTextures(Content);

  4) Finally, draw the Tiled map in your game by calling the following inside your Draw method:
    map.Draw(spriteBatch, graphics.GraphicsDevice.Viewport.Bounds, new Vector(0, 0));

Keep in mind this is the quick start. I have added tons of other useful methods and tricks which I will soon have documented in the project wiki.

Updates
------------
Updates will come as I find the time and when I have the need. Feel free to use and modify this in your own projects however you want.
