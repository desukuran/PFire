﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PFire.Core.Models;
using PFire.Core.Session;

namespace PFire.Core.Protocol.Messages.Outbound
{
    internal sealed class FriendsGamesInfo : XFireMessage
    {
        private readonly UserModel _ownerUser;

        public FriendsGamesInfo(UserModel owner) : base(XFireMessageType.FriendsGameInfo)
        {
            _ownerUser = owner;
            SessionIds = new List<Guid>();
            GameID = new List<uint>();
            GameIP = new List<uint>();
            GamePort = new List<uint>();
        }

        [XMessageField("sid")]
        public List<Guid> SessionIds { get; set; }

        [XMessageField("gameid")]
        public List<uint> GameID { get; set; }

        [XMessageField("gip")]
        public List<uint> GameIP { get; set; }

        [XMessageField("gport")]
        public List<uint> GamePort { get; set; }

        public override Task Process(IXFireClient client)
        {
            SessionIds.Add(client.Server.GetSession(_ownerUser).SessionId);
            GameID.Add(_ownerUser.Game.Id);
            GameIP.Add(_ownerUser.Game.Ip);
            GamePort.Add(_ownerUser.Game.Port);
            return Task.CompletedTask;
        }

    }
}
