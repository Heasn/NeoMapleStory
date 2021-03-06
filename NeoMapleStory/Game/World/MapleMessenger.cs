﻿using System.Collections.Generic;
using System.Linq;

namespace NeoMapleStory.Game.World
{
    public class MapleMessenger
    {
        
        private readonly List<MapleMessengerCharacter> m_mMembers = new List<MapleMessengerCharacter>();
        private bool m_mPos0;
        private bool m_mPos1;

        public MapleMessenger(int id, MapleMessengerCharacter chrfor)
        {
            m_mMembers.Add(chrfor);
            chrfor.Position = GetLowestPosition();
            Id = id;
        }

        public int Id { get; set; }

        public bool ContainsMembers(MapleMessengerCharacter member) => m_mMembers.Contains(member);


        public void AddMember(MapleMessengerCharacter member)
        {
            m_mMembers.Add(member);
            member.Position = GetLowestPosition();
        }

        public void RemoveMember(MapleMessengerCharacter member)
        {
            var position = member.Position;
            if (position == 0)
            {
                m_mPos0 = false;
            }
            else if (position == 1)
            {
                m_mPos1 = false;
            }
            m_mMembers.Remove(member);
        }

        public void SilentRemoveMember(MapleMessengerCharacter member) => m_mMembers.Remove(member);

        public void SilentAddMember(MapleMessengerCharacter member, int position)
        {
            m_mMembers.Add(member);
            member.Position = position;
        }

        public void UpdateMember(MapleMessengerCharacter member)
        {
            for (var i = 0; i < m_mMembers.Count; i++)
            {
                var chr = m_mMembers[i];
                if (chr == member)
                {
                    m_mMembers[i] = member;
                }
            }
        }

        public List<MapleMessengerCharacter> GetMembers() => m_mMembers;

        public int GetLowestPosition()
        {
            int position;
            if (m_mPos0)
            {
                if (m_mPos1)
                {
                    position = 2;
                }
                else
                {
                    m_mPos1 = true;
                    position = 1;
                }
            }
            else
            {
                m_mPos0 = true;
                position = 0;
            }
            return position;
        }

        public int GetPositionByName(string name)
            => m_mMembers.FirstOrDefault(x => x.CharacterName == name)?.Position ?? 4;

        public static bool operator ==(MapleMessenger left, MapleMessenger right)
        {
            return left?.Id == right?.Id;
        }

        public static bool operator !=(MapleMessenger left, MapleMessenger right)
        {
            return !(left == right);
        }

        protected bool Equals(MapleMessenger other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MapleMessenger)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}