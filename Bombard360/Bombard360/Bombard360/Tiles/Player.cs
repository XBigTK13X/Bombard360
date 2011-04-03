using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bombard360.Tiles;

namespace Bombard360
{
    class Player:GameplayObject
    {
        private bool m_isHuman;
        private float m_health = 100;
        private bool m_isAlive;
        private int m_playerIndex;
        private int m_moveCooldown = COOLDOWN_TIME;

        private List<IPowerupEffect> m_powerups = new List<IPowerupEffect>();

        private int MAX_BOMB_CACHE_SIZE = 10;
        private int m_currentBombCacheSize = 0;
        private int m_bombPower = 100;
        private int m_bombRange = 3;

        private int m_AI_SPEED = COOLDOWN_TIME * 2;
        private int m_aiCooldown;

        private void Setup(int gridColumn, int gridRow, int playerId, bool isHuman)
        {
            Initialize(gridColumn, gridRow,SpriteType.PLAYER_STAND);
            m_isHuman = isHuman;
            m_playerIndex = playerId;
            m_isBlocking = true;
            m_aiCooldown = m_AI_SPEED;
            AIManager.Add(this);
        }

        public Player(int gridColumn, int gridRow, int playerId, bool isHuman)
        {
            Setup(gridColumn, gridRow, playerId, isHuman);
        }
        public override void Update()
        {
            base.Update();
            if (m_isHuman)
            {
                RunAsHuman();
            }
            else
            {
                RunAsComputer();
            }
            foreach (IPowerupEffect power in m_powerups)
            {
                power.Update(this);
            }
            CheckForDamage();
        }
        private void CheckForDamage()
        {
            if (BoardManager.HasTileType(m_position, SpriteType.EXPLOSION))
            {
                m_health -= BoardManager.GetExplosionInstance(m_position).GetPower();
            }
            if (m_health <= 0)
            {
                m_isAlive = false;
                m_isActive = false;
            }
        }
        private void RunAsHuman()
        {
            int yVel = ((InputManager.IsMovingLeft(m_playerIndex)) ? -1 : 0) + ((InputManager.IsMovingRight(m_playerIndex)) ? 1 : 0);
            int xVel = ((InputManager.IsMovingDown(m_playerIndex)) ? 1 : 0) + ((InputManager.IsMovingUp(m_playerIndex)) ? -1 : 0);
            if ((xVel != 0 || yVel != 0))
            {
                SetSpriteInfo(SpriteSheetManager.GetSpriteInfo(SpriteType.PLAYER_WALK));
            }
            else
            {
                SetSpriteInfo(SpriteSheetManager.GetSpriteInfo(SpriteType.PLAYER_STAND));
            }
            MoveIfPossible(xVel, yVel);
            if (InputManager.IsPlacingBomb(m_playerIndex))
            {
                PlaceBomb();
            }
            
        }

        private void RunAsComputer()
        {
            m_aiCooldown--;
            if (m_aiCooldown <= 0)
            {
                int xVel = ((AIManager.IsClosestPlayerWest(this)) ? -1 : 0) + ((AIManager.IsClosestPlayerEast(this)) ? 1 : 0);
                int yVel = ((AIManager.IsClosestPlayerSouth(this)) ? 1 : 0) + ((AIManager.IsClosestPlayerNorth(this)) ? -1 : 0);
                MoveIfPossible(xVel, yVel);
                m_aiCooldown = m_AI_SPEED;
                PlaceBomb();
            }
        }

        private void MoveIfPossible(int xVel, int yVel)
        {
            m_moveCooldown--;
            if (m_moveCooldown <= 0&&(xVel!=0||yVel!=0))
            {
                if(BoardManager.IsCellEmpty((int)m_position.X+xVel,(int)m_position.Y+yVel))
                {
                    Move(xVel, yVel);
                    m_moveCooldown = COOLDOWN_TIME;
                }
            }
        }

        //Bomb management
        private void PlaceBomb()
        {
            if (MAX_BOMB_CACHE_SIZE > m_currentBombCacheSize)
            {
                if (BoardManager.Add(new Bomb((int)m_position.X, (int)m_position.Y, this, m_bombPower, m_bombRange)))
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
