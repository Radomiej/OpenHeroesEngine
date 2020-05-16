﻿using System;
using System.Collections.Generic;
using OpenHeroesServer.Server.Models;

namespace OpenHeroesServer.Server
{
    public class PlayerManager
    {
        public static readonly PlayerManager Instance = new PlayerManager();

        private PlayerManager()
        {
            Players = new List<JPlayer>(10);
        }

        public List<JPlayer> Players { get; private set; }

        public JPlayer LocalPlayer { get; set; }

        public void RegisterPlayer(JPlayer playerToRegister)
        {
            Players.Add(playerToRegister);
        }

        public void Clear()
        {
            Players.Clear();
            LocalPlayer = null;
        }

        public JPlayer CreatePlayer()
        {
            JPlayer player = new JPlayer();
            player.ConnectionId = Guid.NewGuid().ToString();
            RegisterPlayer(player);
            return player;
        }

        public JPlayer FindPlayerByConnectionId(string connectionId)
        {
            JPlayer result = null;

            foreach (var player in Players)
            {
                if (player.ConnectionId.Equals(connectionId))
                {
                    return player;
                }
            }
            return null;
        }
    }
}