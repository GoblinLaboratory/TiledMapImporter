﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TiledMapImporter.MapImporter
{
    /// <summary>
    /// The different kinds of orientation types that Tiled supports
    /// </summary>
    public enum Orientation { Orthogonal, Isometric, Staggered }
    /// <summary>
    /// The different render orders possible
    /// </summary>
    public enum RenderOrder { RightDown, RightUp, LeftDown, LeftUp }
    /// <summary>
    /// A map object created using the Tiled Map Editor
    /// http://www.mapeditor.org/
    /// </summary>
    public class Map
    {
        /// <summary>
        /// The TMX format version, generally 1.0
        /// </summary>
        public string Version { set; get; }
        /// <summary>
        /// Map orientation. This library only supports Orthogonal.
        /// </summary>
        public Orientation Orientation { set; get; }
        /// <summary>
        /// The map width in tiles
        /// </summary>
        public int Width { set; get; }
        /// <summary>
        /// The map height in tiles
        /// </summary>
        public int Height { set; get; }
        /// <summary>
        /// The width of a tile
        /// </summary>
        public int TileWidth { set; get; }
        /// <summary>
        /// The height of a tile
        /// </summary>
        public int TileHeight { set; get; }
        /// <summary>
        /// The background color of the map (since 0.9, optional)
        /// </summary>
        public Color BackgroundColor { set; get; }
        /// <summary>
        /// The order in which tiles on tile layers are rendered
        /// </summary>
        public RenderOrder RenderOrder { set; get; }
        /// <summary>
        /// Custom properties for the map object
        /// </summary>
        public Properties Props { set; get; }
        /// <summary>
        /// List of all tilesets used in this map
        /// </summary>
        public List<Tileset> Tilesets { set; get; }
        /// <summary>
        /// List of all layers(regular tile layers) in this map
        /// </summary>
        public List<Layer> Layers { set; get; }
        /// <summary>
        /// List of all object groups in this map
        /// </summary>
        public List<ObjectGroup> ObjectGroups { set; get; }
        /// <summary>
        /// List of all image layers in this map
        /// </summary>
        public List<ImageLayer> ImageLayers { set; get; }

        /// <summary>
        /// Returns the tileset with the given name
        /// </summary>
        /// <param name="name">The name of the tileset to search for</param>
        /// <returns>The tileset with the given name</returns>
        public Tileset GetTilesetByName(string name)
        {
            foreach (Tileset t in Tilesets)
            {
                if (t.Name == name)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the tileset that contains the given global id
        /// </summary>
        /// <param name="gid">The global id to search for</param>
        /// <returns>The tileset that contains the tile with the given gid</returns>
        public Tileset GetTilesetWithGid(int gid)
        {
            foreach (Tileset t in Tilesets)
            {
                if (t.GetTileByGid(gid) != null)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the layer with the given name
        /// </summary>
        /// <param name="name">The name of the layer to search for</param>
        /// <returns>The layer with the given name</returns>
        public Layer GetLayerByName(string name)
        {
            foreach (Layer l in Layers)
            {
                if (l.Name == name)
                {
                    return l;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the object group with the given name
        /// </summary>
        /// <param name="name">The name of the object group to search for</param>
        /// <returns>The object group with the given name</returns>
        public ObjectGroup GetObjectGroupByName(string name)
        {
            foreach (ObjectGroup obj in ObjectGroups)
            {
                if (obj.Name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the image layer with the given name
        /// </summary>
        /// <param name="name">The name of the image layer to search for</param>
        /// <returns>The image layer with the given name</returns>
        public ImageLayer GetImageLayerByName(string name)
        {
            foreach (ImageLayer l in ImageLayers)
            {
                if (l.Name == name)
                {
                    return l;
                }
            }
            return null;
        }

        /// <summary>
        /// The textures need to be loaded for each tileset.
        /// THIS MUST BE CALLED BEFORE DRAWING ANYTHING OR YOU WILL GET AN EXCEPTION!
        /// </summary>
        public void LoadTilesetTextures(ContentManager content)
        {
            foreach (Tileset t in Tilesets)
            {
                t.Image.Texture = content.Load<Texture2D>(t.Image.Source);
            }
        }

        /// <summary>
        /// Draws all visible layers of the Map to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="location">The location to draw the layers</param>
        public void Draw(SpriteBatch spriteBatch, Rectangle location, Vector2 startIndex)
        {
            foreach (Layer l in Layers)
            {
                if (l.Visible)
                {
                    DrawLayer(spriteBatch, l, location, startIndex);
                }
            }
        }

        /// <summary>
        /// Draws the specified tile layer to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layerId">The id of the layer to be drawn in the Layers list</param>
        /// <param name="location">The location to draw the layer</param>
        public void DrawLayer(SpriteBatch spriteBatch, int layerId, Rectangle location, Vector2 startIndex)
        {
            DrawLayer(spriteBatch, Layers[layerId], location, startIndex);
        }

        /// <summary>
        /// Draws the specified tile layer to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layerId">The id of the layer to be drawn in the Layers list</param>
        /// <param name="location">The location to draw the layer</param>
        public void DrawLayer(SpriteBatch spriteBatch, string layerName, Rectangle location, Vector2 startIndex)
        {
            DrawLayer(spriteBatch, GetLayerByName(layerName), location, startIndex);
        }

        /// <summary>
        /// Draws the specified tile layer to the screen
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layer">The Layer object to be drawn</param>
        /// <param name="location">The location to draw the layer</param>
        public void DrawLayer(SpriteBatch spriteBatch, Layer layer, Rectangle location, Vector2 startIndex)
        {
            Draw(spriteBatch, layer, location, startIndex);
        }

        /// <summary>
        /// The only Draw function here that actually does the drawing.
        /// Use any of the above methods to use it.
        /// </summary>
        /// <param name="spriteBatch">A spritebatch object for drawing</param>
        /// <param name="layer">The layer to be drawn</param>
        /// <param name="location">The location to draw the layers</param>
        /// <param name="backgroundColor">The color to use for the background</param>
        private void Draw(SpriteBatch spriteBatch, Layer layer, Rectangle location, Vector2 startIndex)
        {
            int width = location.Width / TileWidth; //The number of tiles in the i direction that fit on the screen
            int height = location.Height / TileHeight; //The number of tiles in the j direction that fit on the screen
            int iStartIndex = (int)startIndex.X; //The i index of the first tile to draw
            int jStartIndex = (int)startIndex.Y; //The j index of the first tile to draw

            if (RenderOrder == RenderOrder.RightDown)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        //This if statement keeps the code from throwing an out of range exception depending on where the start indices are.
                        //There is an explanation along with comments on how to prevent this from happening in the documentation.
                        if (iStartIndex + i < layer.Data.TileData.GetUpperBound(0) && jStartIndex + j < layer.Data.TileData.GetUpperBound(1)
                            && iStartIndex > 0 && jStartIndex > 0)
                        {
                            int gid = layer.Data.GetTileDataAt(iStartIndex + i, jStartIndex + j); //The global id of the tile in the layer at this point

                            //If a gid of 0 occurs, it means that for this layer there is no tile placed in that location
                            if (gid != 0)
                            {
                                Tileset tileset = GetTilesetWithGid(gid); //Find the tileset with the tile that we want to draw
                                Tile tile = tileset.GetTileByGid(gid); //Find the tile in that tilset
                                spriteBatch.Draw(tileset.Image.Texture, new Rectangle(i * TileWidth, j * TileHeight, TileWidth, TileHeight),
                                    tile.Location, Color.White);
                            }
                        }
                    }
                }
            }
            // The following statements just change the order in which the tiles are rendered. Mostly the same code.
            else if (RenderOrder == RenderOrder.RightUp)
            {
                for (int j = height; 0 <= j; j--)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (iStartIndex + i < layer.Data.TileData.GetUpperBound(0) && jStartIndex + j < layer.Data.TileData.GetUpperBound(1)
                            && iStartIndex > 0 && jStartIndex > 0)
                        {
                            int gid = layer.Data.GetTileDataAt(iStartIndex + i, jStartIndex + j);

                            if (gid != 0)
                            {
                                Tileset tileset = GetTilesetWithGid(gid);
                                Tile tile = tileset.GetTileByGid(gid);
                                spriteBatch.Draw(tileset.Image.Texture, new Rectangle(i * TileWidth, j * TileHeight, TileWidth, TileHeight),
                                    tile.Location, Color.White);
                            }
                        }
                    }
                }
            }
            else if (RenderOrder == RenderOrder.LeftDown)
            {
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        if (iStartIndex + i < layer.Data.TileData.GetUpperBound(0) && jStartIndex + j < layer.Data.TileData.GetUpperBound(1)
                            && iStartIndex > 0 && jStartIndex > 0)
                        {
                            int gid = layer.Data.GetTileDataAt(iStartIndex + i, jStartIndex + j);

                            if (gid != 0)
                            {
                                Tileset tileset = GetTilesetWithGid(gid);
                                Tile tile = tileset.GetTileByGid(gid);
                                spriteBatch.Draw(tileset.Image.Texture, new Rectangle(i * TileWidth, j * TileHeight, TileWidth, TileHeight),
                                    tile.Location, Color.White);
                            }
                        }
                    }
                }
            }
            else if (RenderOrder == RenderOrder.LeftUp)
            {
                for (int j = height; 0 <= j; j--)
                {
                    for (int i = width; 0 <= i; i--)
                    {
                        if (iStartIndex + i < layer.Data.TileData.GetUpperBound(0) && jStartIndex + j < layer.Data.TileData.GetUpperBound(1)
                            && iStartIndex > 0 && jStartIndex > 0)
                        {
                            int gid = layer.Data.GetTileDataAt(iStartIndex + i, jStartIndex + j);

                            if (gid != 0)
                            {
                                Tileset tileset = GetTilesetWithGid(gid);
                                Tile tile = tileset.GetTileByGid(gid);
                                spriteBatch.Draw(tileset.Image.Texture, new Rectangle(i * TileWidth, j * TileHeight, TileWidth, TileHeight),
                                    tile.Location, Color.White);
                            }
                        }
                    }
                }
            }
        }
    }
}
