﻿using Microsoft.Xna.Framework;
using Monocle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Mod.CelesteNet.Server.Chat {
    public class ChatSettings {

        public int MaxChatTextLength { get; set; } = 256;

        public string CommandPrefix { get; set; } = "/";

        public Color ColorBroadcast { get; set; } = Calc.HexToColor("#00adee");
        public Color ColorServer { get; set; } = Calc.HexToColor("#9e24f5");
        public Color ColorError { get; set; } = Calc.HexToColor("#c71585");
        public Color ColorCommand { get; set; } = Calc.HexToColor("#2e31f1");
        public Color ColorCommandReply { get; set; } = Calc.HexToColor("#e39dcc");

        public string MessageGreeting { get; set; } = "Welcome {player}, to <insert server name here>!";
        public string MessageMOTD { get; set; } =
@"Don't cheat. Have fun!
Press T to talk.
Send /help for a list of all commands.";
        public string MessageLeave { get; set; } = "Cya, {player}!";

    }
}
