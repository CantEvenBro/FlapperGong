﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    /// <summary>
    /// A collection of game functions and classes to store information allowing a simple wargame that can be controlled via the dicord bot
    /// </summary>
    public class Game
    {
        public int cellLength = 0;
        public List<Unit> units = null;
        public List<List<Cell>> cellGrid = null;
        public Game(int gridSize)
        {
            cellGrid = new List<List<Cell>>();
            units = new List<Unit>();
            cellLength = gridSize;
            for ( int y =0; y< gridSize; y++)
            {
                List<Cell> newRow = new List<Cell>();
                for (int x = 0; x < gridSize; x++)
                {
                    newRow.Add(new Cell(x, y));
                }
                cellGrid.Add(newRow);
            }
        }
        public string AddUnit(string name,int x, int y)
        {
            Unit newUnit = new Unit(name,2, 1, 0, 0, this, this.units.Count());
            if (x< this.cellLength && y < this.cellLength && cellGrid[x][y].MoveUnit(ref newUnit))
            {
                units.Add(newUnit);
                newUnit.MoveUnit(x, y);
                return "Unit Added Successfully";
            }
            else
            {

                return "Unit Could Not Be Added";
            }
        }
    }
    public class Unit
    {
        public int health;
        public string name;
        public int damage;
        public int x;
        public int y;
        public Game gameThisUnitIsPartOf;
        public int indexInGame;
        public Unit(string name, int health,int damage,int x, int y, Game game, int index)
        {
              this.health = health;
              this.name = name;
              this.damage = damage;
              this.x = x;
              this.y = y;
            this.gameThisUnitIsPartOf = game;
              this.indexInGame = index;

        }
        public bool AttackUnit( int x, int y)
        {
            if(gameThisUnitIsPartOf.cellGrid[x][y].unitOnCell!= null && gameThisUnitIsPartOf.cellGrid[x][y].unitOnCell != this)
            {
                gameThisUnitIsPartOf.cellGrid[x][y].unitOnCell.ChangeHealth(-1);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ChangeHealth(int change)
        {
            this.health = this.health += change;
            if (this.health < 1)
            {
                gameThisUnitIsPartOf.cellGrid[x][y].unitOnCell = null;
            }
        }
        public void MoveUnit(int x, int y)
        {
            if (this.health > 0)
            {

                this.x = x;
                this.y = y;
            }
        }
    }
    public class Cell
    {
        public int x;
        public int y;
        public Unit unitOnCell;
        public Cell( int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public bool MoveUnit(ref Unit unitMovingHere)
        {

            if (this.unitOnCell == null)
            {
                this.unitOnCell = unitMovingHere;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
