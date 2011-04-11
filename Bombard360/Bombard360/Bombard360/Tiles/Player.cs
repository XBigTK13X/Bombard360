using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Tiles;

namespace Bombard360
{
    class Player:GameplayObject
    {
        private float m_health = 100;
        private int m_moveCooldown = COOLDOWN_TIME;

        private List<IPowerupEffect> m_powerups = new List<IPowerupEffect>();

        private int MAX_BOMB_CACHE_SIZE = 10;
        private int m_currentBombCacheSize = 0;
        private int m_bombPower = 100;
        private int m_bombRange = 3;

        private void Setup(int gridColumn, int gridRow)
        {
            Initialize(gridColumn, gridRow,SpriteType.PLAYER_STAND);
            m_isBlocking = true;
            
            AIManager.Add(this);
        }

        public Player(int gridColumn, int gridRow)
        {
            Setup(gridColumn, gridRow);
        }
        public override void Update()
        {
            base.Update();
            Run();
            foreach (IPowerupEffect power in m_powerups)
            {
                power.Update(this);
            }
            CheckForDamage();
        }
        public virtual void Run() { }

        private void CheckForDamage()
        {
            if (BoardManager.HasTileType(m_graphic.GetPosition(), SpriteType.EXPLOSION))
            {
                m_health -= BoardManager.GetExplosionInstance(m_graphic.GetPosition()).GetPower();
            }
            if (m_health <= 0)
            {
                m_isActive = false;
            }
        }

        protected void MoveIfPossible(int xVel, int yVel)
        {
            m_moveCooldown--;
            if ((xVel != 0 || yVel != 0) && m_moveCooldown <= 0)
            {
                SetSpriteInfo(SpriteSheetManager.GetSpriteInfo(SpriteType.PLAYER_WALK));
                if (BoardManager.IsCellEmpty((int)m_graphic.GetPosition().X + xVel, (int)m_graphic.GetPosition().Y + yVel))
                {
                    Move(xVel, yVel);
                    m_moveCooldown = COOLDOWN_TIME;
                }
            }
            else
            {
                SetSpriteInfo(SpriteSheetManager.GetSpriteInfo(SpriteType.PLAYER_STAND));
            }
        }

        //Bomb management
        protected void PlaceBomb()
        {
            if (MAX_BOMB_CACHE_SIZE > m_currentBombCacheSize)
            {
                if (BoardManager.Add(new Bomb((int)m_graphic.GetPosition().X, (int)m_graphic.GetPosition().Y, this, m_bombPower, m_bombRange)))
                {
                    m_currentBombCacheSize++;
                }
            }
        }
        public void FreeBombCacheSlot()
        {
            if (m_currentBombCacheSize > 0)
            {
                m_currentBombCacheSize--;
            }
        }

        //Powerup Management
        public void AddPowerup(IPowerupEffect power)
        {
            m_powerups.Add(power);
            power.Apply(this);
        }

        public void ModifySpeed(int amount)
        {
            COOLDOWN_TIME -= amount;
            COOLDOWN_TIME = (COOLDOWN_TIME < 2) ? 2 : COOLDOWN_TIME;
        }
        public void ModifyBombCache(int amount)
        {
            MAX_BOMB_CACHE_SIZE += amount;
            MAX_BOMB_CACHE_SIZE = (MAX_BOMB_CACHE_SIZE < 1) ? 1 : MAX_BOMB_CACHE_SIZE;
        }
        public void ModifyBombPower(int amount)
        {
            m_bombPower += amount*100;
            m_bombPower = (m_bombPower < 100) ? 100 : m_bombPower;
        }
        public void ModifyHealth(int amount)
        {
            m_health += amount*100;
            m_health = (m_health < 100) ? 100 : m_health;
        }
    }
}
